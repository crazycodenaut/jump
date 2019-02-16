using System;

namespace JumpTool
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class JumpToAttribute : Attribute
    {
        public string TargetName { get; set; }
        public string ShortDescription { get; set; }

        public JumpToAttribute(string targetName, string shortDescription)
        {
            TargetName = targetName;
            ShortDescription = shortDescription;
        }
    }
}
