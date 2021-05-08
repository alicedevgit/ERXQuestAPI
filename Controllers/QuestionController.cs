using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionnaireServices.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using QuestionnaireServices.Models.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Cors;

namespace QuestionnaireServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ERXQNContext _dbContext;
        private readonly IWebHostEnvironment _env;
        public QuestionController(ERXQNContext dbDbContext, IWebHostEnvironment env)
        {
            _dbContext = dbDbContext;
            _env = env;
        }

        /// <summary>
        /// GET (Read all question categories) api/question/categories
        /// </summary>
        /// <returns></returns>
        [HttpGet("categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<IEnumerator<QuestionCategory>>> GetAllQuestionCategories()
        {
            var items = await _dbContext.QuestionCategories.OrderBy(qc => qc.Sequence).ToArrayAsync();
            if (items == null)
                return NotFound();

            return Ok(items);
        }

        /// <summary>
        /// GET (Read all questions with in the specified category) api/question/{categoryId}
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("cat/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<IEnumerator<Question>>> GetAllQuestionsInCategory(int categoryId)
        {
            var items = await _dbContext.Questions
                        .Where(quest => quest.CategoryId == categoryId)
                        .OrderBy(quest => quest.Sequence)
                        .ToArrayAsync();
            if (items == null)
                return NotFound();

            return Ok(items);
        }

        /// <summary>
        /// GET (Read all question's choices with in the specified question id) api/question/choices/{questionId}
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        [HttpGet("choices/{questionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<IEnumerator<ItemList>>> GetAllQuestionsChoices(int questionId)
        {
            var items = await _dbContext.QuestionChoices
                        .Where(quest => quest.QuestionId == questionId)
                        .OrderBy(quest => quest.Sequence)
                        .ToArrayAsync();
            if (items == null || items.Length <= 0)
                return NotFound();

            List<ItemList> choices = new List<ItemList>();
            if (items.Length > 0)  
                if (!string.IsNullOrEmpty(items[0].SourceUri))
                    choices = items[0].GetQuestionChoice(Path.Combine(_env.ContentRootPath, "Sources"));
                else
                    foreach (var item in items)  
                        choices.Add(new ItemList() { value = item.Value, text = item.Text }); 

            return Ok(choices);
        }

        /// <summary>
        /// GET (Read all restrictions with in the specified question id) api/restriction/{questionId}
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        [HttpGet("restriction/{questionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<IEnumerator<Question>>> GetRestrictions(int questionId)
        {
            var items = await _dbContext.AnswerRestrictions
                        .Join(
                            _dbContext.Restrictions,
                            ar => ar.RestrictionId,
                            r => r.Id,
                            (ar, r) => new {
                                questionId = ar.QuestionId,
                                restriction = r 
                            }
                        )
                        .Where(tbl => tbl.questionId == questionId) 
                        .ToArrayAsync();
            if (items == null)
                return NotFound();

            return Ok(items);
        }
    }

    public static class QuestionExtensions
    {
        public static List<ItemList> GetQuestionChoice(this QuestionChoice questionChoice, string root)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var jsonString = System.IO.File.ReadAllText(Path.Combine(root, questionChoice.SourceUri));
            var jsonModel = JsonSerializer.Deserialize<List<ItemList>>(jsonString, options).OrderBy(item => item.text).ToList();

            return jsonModel; 
        }
    }
}
