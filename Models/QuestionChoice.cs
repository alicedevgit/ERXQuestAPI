using System;
using System.Collections.Generic;

#nullable disable

namespace QuestionnaireServices.Models
{
    public partial class QuestionChoice
    {
        public int QuestionId { get; set; }
        public int Sequence { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public string SourceUri { get; set; }
    }
}
