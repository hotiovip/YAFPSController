using System;
using UnityEngine;

namespace Hotiovip.YAFPSController.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class TitleAttribute : PropertyAttribute
    {
        public string title { get; }
        public TitleAttribute(string title) 
        {
            this.title = title;
        }
    }
}
