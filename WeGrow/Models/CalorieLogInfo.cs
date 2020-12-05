using System;
using System.Collections.Generic;
using System.Text;
using SQLite; 

namespace WeGrow.Models
{
    public class CalorieLogInfo
    {
        [PrimaryKey, AutoIncrement]
        public int log_id { get; set; }

        [NotNull, Unique]
        public string date { get; set; }
        public bool isInitialized { get; set; } = false;

        public int breakfastCals { get; set; }
        public int lunchCals { get; set; }
        public int dinnerCals { get; set; }
        public int otherCals { get; set; }
        public string notes { get; set; }

    }
}
