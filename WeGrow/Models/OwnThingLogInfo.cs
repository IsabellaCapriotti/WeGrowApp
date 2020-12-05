using System;
using System.Collections.Generic;
using System.Text;
using SQLite; 

namespace WeGrow.Models
{
    public class OwnThingLogInfo
    {
        [PrimaryKey, AutoIncrement]
        public int log_id { get; set; }
        [NotNull]
        public string date { get; set; }
        public bool isInitialized { get; set; } = false;
        public string breakfastNotes { get; set; }
        public string lunchNotes { get; set; }
        public string dinnerNotes { get; set; }
        public string otherNotes { get; set; }
        public string dayState { get; set; }
    }
}
