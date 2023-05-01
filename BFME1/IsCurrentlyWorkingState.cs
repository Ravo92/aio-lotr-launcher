namespace PatchLauncher
{
    static class IsCurrentlyWorkingState
    {
        private static bool _IsLauncherCurrentlyWorking = false;

        public static bool IsLauncherCurrentlyWorking
        {
            get { return _IsLauncherCurrentlyWorking; }
            set { _IsLauncherCurrentlyWorking = value; }
        }
    }
}
