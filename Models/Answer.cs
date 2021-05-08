using System;
using System.Collections.Generic;

#nullable disable

namespace QuestionnaireServices.Models
{
    public partial class Answer
    {
        public string Token { get; set; }
        public int QuestionId { get; set; }
        public string Value { get; set; }

        public string Text { get; set; }
    }
}
