using System;
using UnityEngine;

namespace Hotiovip.YAFPSController.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class RequiredAttribute : PropertyAttribute
    {

    }
}
