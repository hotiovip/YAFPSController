using UnityEngine;

namespace Hotiovip.YAFPSController
{
    public class PosRotData : ScriptableObject
    {
        [Tooltip("X: how much to the right | Y: how much down | Z: how far from the camera")]
        public Vector3 position;
        [Tooltip("X: on itself | Y: left-right | Z: down-up")]
        public Quaternion rotation;
    }
}
