namespace Powell
{
    static class Debug
    {
        private static bool debugOn = false;
        public static bool IsDebugOn => debugOn;

        public static void DebugOn() => debugOn = true;
        public static void DebugOff() => debugOn = false;
    }
}
