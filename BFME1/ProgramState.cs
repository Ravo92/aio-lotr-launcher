using System;

namespace PatchLauncher
{
    public static class ProgramState
    {
        public enum ProgramStates{ normal, repair, install };

        private static ProgramStates _programStates;

        public static ProgramStates CurrentProgramState
        {
            get { return _programStates; }
            set { _programStates = value; }
        }

        /// <summary>Indicates whether the specified array is null or has a length of zero.</summary>
        /// <param name="array">The array to test.</param>
        /// <returns>true if the array parameter is null or has a length of zero; otherwise, false.</returns>
        public static bool IsArrayNullOrEmpty(this Array array)
        {
            return (array == null || array.Length == 0);
        }
    }
}
