using System;
using System.Collections.Generic;

namespace QLibrary.Data
{
    public partial class ActivityLog
    {
        public int Id { get; set; }
        public string ActivityType { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public AspNetUsers User { get; set; }
    }
}
