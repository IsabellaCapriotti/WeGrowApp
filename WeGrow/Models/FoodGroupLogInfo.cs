using System;
using System.Collections.Generic;
using System.Text;
using SQLite; 

namespace WeGrow.Models
{
    public class FoodGroupLogInfo
    {
        [PrimaryKey, AutoIncrement]
        public int log_id { get; set; }
        [NotNull]
        public string date { get; set; }
        public bool isInitialized { get; set; } = false;
        [MaxLength(5)]
        public string breakfastVals { get; set; } = "00000";
        [MaxLength(5)]
        public string lunchVals { get; set; } = "00000";
        [MaxLength(5)]
        public string dinnerVals { get; set; } = "00000";
        [MaxLength(5)]
        public string otherVals { get; set; } = "00000";
    
        public string notes { get; set; }
    }
}
