using System;
using System.Collections.Generic;

#nullable disable

namespace QuestionnaireServices.Models
{
    public partial class Question
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int AnswerTypeId { get; set; }
        public int? Sequence { get; set; }
    }
}
