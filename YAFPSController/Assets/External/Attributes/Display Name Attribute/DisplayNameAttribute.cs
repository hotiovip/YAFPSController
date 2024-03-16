using System;
using UnityEngine;

namespace Hotiovip.YAFPSController.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class DisplayNameAttribute : PropertyAttribute
    {
        public string displayName { get; }

        public DisplayNameAttribute(string displayName)
        {
            this.displayName = displayName;
        }
    }
}
