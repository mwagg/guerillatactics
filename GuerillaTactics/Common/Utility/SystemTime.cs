using System;

namespace GuerillaTactics.Common.Utility
{
    public static class SystemTime
    {
        static SystemTime()
        {
            ResetDelegate();
        }

        public static void ResetDelegate()
        {
            Now = () => DateTime.Now;
        }

        public static Func<DateTime> Now;
    }
}