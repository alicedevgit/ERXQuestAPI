using System;
using System.Collections.Generic;

#nullable disable

namespace QuestionnaireServices.Models
{
    public partial class Restriction
    {
        public int Id { get; set; }
        public string NotAllowedValue { get; set; }
        public string WarningMessage { get; set; }
        public string Operation { get; set; }
    }
}
