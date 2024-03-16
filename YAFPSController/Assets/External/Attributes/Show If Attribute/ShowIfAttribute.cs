using System;
using UnityEngine;

namespace Hotiovip.YAFPSController.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ShowIfAttribute : PropertyAttribute
    {
        public string variableName { get; }

        public ShowIfAttribute(string variableName)
        {
            this.variableName = variableName;
        }   
    }
}
