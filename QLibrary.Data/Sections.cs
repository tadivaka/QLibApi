using System;
using System.Collections.Generic;

namespace QLibrary.Data
{
    public partial class Sections
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? RequiredCount { get; set; }
    }
}
