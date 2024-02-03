using UnityEngine;

namespace Hotiovip.YAFPSController
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "YAFPSController/WeaponData", order = 0)]
    public class WeaponData : ScriptableObject
    {
        public string weaponName = "no name weapon";

        public int magSize = 30;

        public bool aimable = false;
    }
}
