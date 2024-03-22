using Hotiovip.YAFPSController.Attributes;
using UnityEngine;

namespace Hotiovip.YAFPSController
{
    public class PosRotData : ScriptableObject
    {
        [Title("Default")]
        [Tooltip("X: how much to the right | Y: how much down | Z: how far from the camera")]
        public Vector3 defaultPosition;
        [Tooltip("X: on itself | Y: left-right | Z: down-up")]
        public Quaternion defaultRotation;

        [Title("Aiming")]
        [Tooltip("X: how much to the right | Y: how much down | Z: how far from the camera")]
        public Vector3 aimPosition;
        [Tooltip("X: on itself | Y: left-right | Z: down-up")]
        public Quaternion aimRotation;
    }
}
