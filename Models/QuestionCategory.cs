using System;
using System.Collections.Generic;

#nullable disable

namespace QuestionnaireServices.Models
{
    public partial class QuestionCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
        public string Remark { get; set; }
    }
}
