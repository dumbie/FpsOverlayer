using System;

namespace LibraryShared
{
    public partial class Classes
    {
        [Serializable]
        public class StatsOrderDetails()
        {
            public int Index { get; set; } = -1;
            public string Identifier { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
        }
    }
}