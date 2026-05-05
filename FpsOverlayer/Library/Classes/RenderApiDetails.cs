using System;

namespace LibraryShared
{
    public partial class Classes
    {
        [Serializable]
        public class RenderApiDetails()
        {
            public bool RenderingUI { get; set; } = false;
            public string ApiName3D { get; set; } = string.Empty;
        }
    }
}