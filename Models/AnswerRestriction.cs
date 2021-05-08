using System;
using System.Collections.Generic;

#nullable disable

namespace QuestionnaireServices.Models
{
    public partial class AnswerRestriction
    {
        public int QuestionId { get; set; }
        public int RestrictionId { get; set; }
    }
}
