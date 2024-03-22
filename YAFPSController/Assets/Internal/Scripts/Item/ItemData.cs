using Hotiovip.YAFPSController.Attributes;
using UnityEngine;

namespace Hotiovip.YAFPSController
{
    /// <summary>
    /// Contains item-specific data.
    /// </summary>
    [CreateAssetMenu(fileName = "ItemData", menuName = "YAFPSController/ItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        [Title("General Settings")]
        public string itemName;
        [Space]
        public PosRotData posRotData;
        /*
        [Tooltip("X: how much to the right | Y: how much down | Z: how far from the camera")]
        public Vector3 itemPosition;
        [Tooltip("X: on itself | Y: left-right | Z: down-up")]
        public Quaternion itemRotation;
        */

        [Title("Sway Settings")]
        [Tooltip("If this variable is false, then no sway will be applied to the item.")]
        public bool canSway = true;

        [ShowIf("canSway")]
        [Tooltip("X: on itself | Y: left-right | Z: down-up")]
        public Vector3 swayVector = new Vector3(5, 3, 2);
        [ShowIf("canSway")]
        [Tooltip("1 = positive direction (up, forward, right) and 0 = negative direction (down, back, left)")]
        public Vector3 swayVectorDirection = new Vector3(-1, 1, 1);
        [ShowIf("canSway")]
        [Tooltip("Max values that can be reached on each axis. (Maximum)")]
        public Vector3 maxSwayVector = new Vector3(10, 6, 4);
        [ShowIf("canSway")]
        [Tooltip("Min values that can be reached on each axis. (Least)")]
        public Vector3 minSwayVector = new Vector3(-10, -6, -4);
        [ShowIf("canSway")]
        [Tooltip("The time that the weapons need to return to their original axis. More is smoother, less is snappier.")]
        public float swaySmoothTime = 10;
    }
}
