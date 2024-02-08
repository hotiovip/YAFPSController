using UnityEngine;

namespace Hotiovip.YAFPSController
{
    /// <summary>
    /// Used for weapon-specific data.
    /// </summary>
    [CreateAssetMenu(fileName = "WeaponData", menuName = "YAFPSController/WeaponData", order = 1)]
    public class WeaponData : ItemData
    {
        [Space]
        public int magSize = 30;
    }
}
