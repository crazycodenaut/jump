using System;

namespace JumpTool
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class JumpToAttribute : Attribute
    {
        public string TargetMethodName { get; set; }

        public JumpToAttribute(string targetName)
        {
            TargetMethodName = targetName;
        }
    }
}
