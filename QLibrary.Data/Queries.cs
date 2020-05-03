using System;
using System.Collections.Generic;

namespace QLibrary.Data
{
    public partial class Queries
    {
        public int QueryId { get; set; }
        public int? SectionId { get; set; }
        public string QueryName { get; set; }
        public string Description { get; set; }
        public DateTime? ScreatedDate { get; set; }
    }
}
