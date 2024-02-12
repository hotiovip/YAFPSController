using Sirenix.OdinInspector;
using UnityEngine;

namespace Hotiovip.YAFPSController
{
    /// <summary>
    /// ScriptableObject class used to store item-specific data.
    /// </summary>
    [CreateAssetMenu(fileName = "ItemData", menuName = "YAFPSController/ItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        [Title("General Settings")]
        public string itemName;
        [Space]
        public Vector3 itemPosition;
        [Space]
        public Quaternion itemRotation;

        [Title("Sway Settings")]
        public bool canSway = true;

        [ShowIf("canSway")]
        public Vector3 swayVector = new Vector3(5, 3, 2);
        [ShowIf("canSway")]
        public Vector3 maxSwayVector = new Vector3(10, 6, 4);
        [ShowIf("canSway")]
        public Vector3 minSwayVector = new Vector3(-10, -6, -4);
        [ShowIf("canSway")]
        public float swaySmoothTime = 6;
    }
}
