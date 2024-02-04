using UnityEngine;

namespace Hotiovip.YAFPSController
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "YAFPSController/WeaponData", order = 1)]
    public class WeaponData : ItemData
    {
        [Space]
        public int magSize = 30;
    }
}
