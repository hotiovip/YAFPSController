using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hotiovip.YAFPSController
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "YAFPSController/ItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        [Title("General Settings")]
        public string itemName;
        [Space]
        public bool canPrimaryUse = true;
        public bool canSecondaryUse = false;

        [Title("Sway Settings")]
        public bool canSway = true;

        [ShowIf("canSway")]
        public Vector3 swayAmount;
    }
}
