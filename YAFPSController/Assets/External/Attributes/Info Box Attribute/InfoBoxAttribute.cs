using System;
using UnityEngine;

namespace Hotiovip.YAFPSController.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class InfoBoxAttribute : PropertyAttribute
    {
        public string text { get; }

        public InfoBoxAttribute(string text) 
        {
            this.text = text;
        }
    }
}
