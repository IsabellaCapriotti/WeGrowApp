using System;
using System.Collections.Generic;
using System.Text;
using SQLite; 

namespace WeGrow.Models
{
    public class UserInfo
    {
        [PrimaryKey]
        public int userId { get; set; }
        public string Name { get; set; }
        public string StarterPlant { get; set; }
        public string GrowthStage { get; set; }
        public string TrackingPreference { get; set; }
        public int CurrentWeight { get; set; }
        public int GoalWeight { get; set; }
        public int StartWeight { get; set; }
        public string MeasurementUnit { get; set; }
    }
}
