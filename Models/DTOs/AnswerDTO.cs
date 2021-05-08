using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireServices.Models.DTOs
{
    public class AnswerDTO
    {
        public ItemDictionary[] headers { get; set; }
        public JObject[] data { get; set; }
    }
}
