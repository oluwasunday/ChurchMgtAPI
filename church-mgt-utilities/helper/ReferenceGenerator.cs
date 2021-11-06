using System;

namespace hotel_booking_utilities
{
    public static class ReferenceGenerator
    {
        /// <summary>
        /// Generates a randowm number
        /// </summary>
        /// <returns>integer of randowm number</returns>
        public static int Generate()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            return rand.Next(100000000, 999999999);
        }

        public static string GetInitials(string word = "RCCGSldRck")
        {
            return word;
        }
    }
}
