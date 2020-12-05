using System;
using System.Collections.Generic;
using System.Text;

namespace WeGrow
{
    class ReferenceItems
    {
        public static List<string> PlantImageSources = new List<string> { "starter_cactus.png", "starter_fern.png", "starter_purple.png" };
        public const int MIN_WEIGHT_KG = 30;
        public const int MAX_WEIGHT_KG = 200;
        public const int MIN_WEIGHT_LBS = 60;
        public const int MAX_WEIGHT_LBS = 400;

        public static bool isInConstraints(int weight, string unit)
        {
            switch (unit)
            {
                case "lbs":
                    return (weight >= MIN_WEIGHT_LBS && weight <= MAX_WEIGHT_LBS);
                case "kg":
                    return (weight >= MIN_WEIGHT_KG && weight <= MAX_WEIGHT_KG);
                default:
                    return false; 
            }
        }
    }
}
