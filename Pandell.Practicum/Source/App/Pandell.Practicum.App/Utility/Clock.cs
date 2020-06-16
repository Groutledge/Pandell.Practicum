using System;

namespace Pandell.Practicum.App.Utility
{
    public static class Clock
    {
        private static bool isClockFrozen;
        
        static Clock()
        {
            Now = DateTime.Now;
            UnFreeze();
        }

        public static DateTime UtcNow()
        {
            if (isClockFrozen) return Now;
            Now = DateTime.UtcNow;
            return Now;
        }

        public static DateTime UtcToday()
        {
            return UtcNow().Date;
        }

        public static void Freeze()
        {
            isClockFrozen = true;
        }

        public static void UnFreeze()
        {
            isClockFrozen = false;
        }
        
        #region Class Members
        
        public static DateTime Now { get; private set; }

        public static DateTime Today => Now.Date;

        #endregion
    }
}