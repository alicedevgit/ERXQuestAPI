using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using QuestionnaireServices.Models;
using QuestionnaireServices.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly ERXQNContext _dbContext;
        private readonly IWebHostEnvironment _env;
        public AnswerController(ERXQNContext dbDbContext, IWebHostEnvironment env)
        {
            _dbContext = dbDbContext;
            _env = env;
        }

        /// <summary>
        /// GET (Read all answer for exporting CSV) /Answer/{inToken}
        /// </summary>
        /// <param name="inToken"></param>
        /// <returns></returns>
        [HttpGet("{inToken}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<AnswerDTO>> GetAnswers(string inToken)
        {
            if (string.IsNullOrEmpty(inToken))
                return BadRequest();

            var answers = await _dbContext.Answers.ToArrayAsync();
            if(inToken != "all")
                answers = await _dbContext.Answers.Where(a => a.Token == inToken).ToArrayAsync();

            if (answers.Length <= 0)
                return NotFound();

            var headers = await _dbContext.QuestionCategories.OrderBy(qc => qc.Sequence)
                .Join(
                    _dbContext.Questions.OrderBy(q => q.Sequence),
                    qc => qc.Id,
                    q => q.CategoryId,
                    (qc, q) => new
                    {
                        categoryName = qc.Name,
                        questionId = q.Id,
                        questionName = q.Name,
                    }
                ).Select(q => new ItemDictionary { 
                    key = q.questionId.ToString(),
                    label = string.Format("{0}: {1}", q.categoryName, q.questionName)
                }).ToArrayAsync();
             
            
            object[] tmpItems = null;
            string tmpValueStr = "";
            int headerIndex = 0, answerIndex = 0;

            var tokenList = answers.GroupBy(a => a.Token).Select(a => a.FirstOrDefault().Token).ToArray();
            JObject[] data = new JObject[tokenList.Length];
            foreach (var token in tokenList)
            {  
                headerIndex = 0;
                tmpItems = new object[headers.Length];
                foreach (ItemDictionary header in headers)
                {
                    tmpValueStr = "";
                    var value = answers.Where(a => a.QuestionId.ToString() == header.key && a.Token == token);
                    if (value.Any())
                        tmpValueStr = value.SingleOrDefault().Value;

                    tmpItems[headerIndex++] = string.Format("\"{0}\": \"{1}\"", header.key, tmpValueStr);
                }

                string jsonObjStr = "{" + string.Join(",", tmpItems) + "}";
                JObject json = JObject.Parse(jsonObjStr);
                data[answerIndex++] = json;
            }

            return Ok(new AnswerDTO { headers = headers, data = data });
        }

        /// <summary>
        /// POST (Create or Update) /Answer
        /// </summary>
        /// <param name="answers"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<bool>> UpdateAnswers([FromBody] List<Answer> answers)
        {
            bool success = false;
            if (answers == null || answers.Count <= 0)
                return BadRequest();

            try
            {
                Answer updatedClass = null, newAnswer = null;
                foreach (var answer in answers)
                {
                    updatedClass = answer;
                    newAnswer = answer.ToModel();
                    var answerToUpdate = await _dbContext.Answers.FindAsync(answer.Token, answer.QuestionId);
                    if (answerToUpdate != null)
                    {
                        answerToUpdate.Update(newAnswer);
                        updatedClass = answerToUpdate;
                    }
                    else
                    {
                        _dbContext.Answers.Add(newAnswer);
                        updatedClass = newAnswer;
                    }

                    await _dbContext.SaveChangesAsync();
                }
                success = true;
            }
            catch { }
            return success;
        }
    }
    public static class AnswerExtensions
    {
        public static Answer ToModel(this Answer item)
        {
            return new Answer
            {
                QuestionId = item.QuestionId,
                Token = item.Token,
                Value = item.Text 
            };
        } 

        public static void Update(this Answer itemToUpdate, Answer newItem)
        {
            if (!newItem.Token.Equals(itemToUpdate.Token) && !newItem.QuestionId.Equals(itemToUpdate.QuestionId)) 
                throw new NotSupportedException();

            itemToUpdate.Value = newItem.Text; 
        }
    }
}
