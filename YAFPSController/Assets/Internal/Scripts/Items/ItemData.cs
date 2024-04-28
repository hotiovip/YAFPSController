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

        [Title("Procedural Animations")]
        public ProceduralAnimationsData proceduralAnimationsData;
    }
}
