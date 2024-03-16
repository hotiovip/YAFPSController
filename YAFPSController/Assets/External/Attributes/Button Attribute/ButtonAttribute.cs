using System;
using UnityEngine;

namespace Hotiovip.YAFPSController.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ButtonAttribute : PropertyAttribute
    {
        public string methodName { get; }
        public string buttonName { get; }

        public ButtonAttribute(string methodName, string buttonName) 
        {
            this.methodName = methodName;
            this.buttonName = buttonName;
        }
    }
}
