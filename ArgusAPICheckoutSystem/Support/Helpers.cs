using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgusAPICheckoutSystem.Support
{
    public static class Helpers
    {
        public static int ConvertTimetoFloat(string time)
        {
            string[] parts = time.Split(':');
            int? hour = (int.TryParse(parts[0], out int result) ? result : (int?)null);
            return (int)hour;

        }

        public static float ConvertPoundToFloat(string pound)
        {
            string cleanedPrice = pound.Replace("£", "");

            // Convert to float
            if (float.TryParse(cleanedPrice, NumberStyles.Any, CultureInfo.InvariantCulture, out float price))
            {
                return price; 
            }
            else
            {
                return 0;
            }
        }
    }
}
