using Hotiovip.YAFPSController.Attributes;
using UnityEngine;

namespace Hotiovip.YAFPSController.Items
{
    /// <summary>
    /// Contains item-specific data.
    /// </summary>
    [CreateAssetMenu(fileName = "ItemData", menuName = "YAFPSController/ItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        [Title("General Settings")]
        public string itemName;

        [Title("Sway Settings")]
        [Tooltip("If this variable is false, then no sway will be applied to the item.")]
        public bool canLookSway = true;

        [ShowIf("canLookSway")]
        [Tooltip("X: on itself | Y: left-right | Z: down-up")]
        public Vector3 lookSwayVector = new Vector3(5, 3, 2);
        [ShowIf("canLookSway")]
        [Tooltip("1 = positive direction (up, forward, right) and -1 = negative direction (down, back, left)")]
        public Vector3 lookSwayVectorDirection = new Vector3(-1, 1, 1);
        [ShowIf("canLookSway")]
        [Tooltip("Max values that can be reached on each axis. (Maximum)")]
        public Vector3 maxLookSwayVector = new Vector3(10, 6, 4);
        [ShowIf("canLookSway")]
        [Tooltip("Min values that can be reached on each axis. (Least)")]
        public Vector3 minLookSwayVector = new Vector3(-10, -6, -4);
        [ShowIf("canLookSway")]
        [Tooltip("The time that the weapons need to return to their original axis. More is smoother, less is snappier.")]
        public float lookSwaySmoothTime = 10;



        [Title("Movement Settings")]
        public bool canMovementSway = true;

        [ShowIf("canMovementSway")]
        public AnimationCurve movementSwayAnimationCurve;
    }
}
