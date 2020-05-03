using System;
using System.Collections.Generic;
using System.Text;

namespace QLibrary.Dto
{
   public class QueryDto
    {  
        public int QueryId { get; set; }

        public int SectionId { get; set; }

        public string SectionName { get; set; }

        public string QueryName { get; set; }

        public string Description { get; set; }

        public DateTime SCreatedDate { get; set; }
        public List<SectionDto> SectionDetails { get; set; }
    }
}
