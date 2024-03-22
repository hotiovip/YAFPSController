using Hotiovip.YAFPSController.Attributes;
using UnityEngine;

namespace Hotiovip.YAFPSController.Weapon
{
    /// <summary>
    /// Used for weapon-specific data.
    /// </summary>
    [CreateAssetMenu(fileName = "WeaponData", menuName = "YAFPSController/WeaponData", order = 1)]
    public class WeaponData : ItemData
    {
        [Space]
        [Title("General Settings")]
        /// <summary>
        /// Weapon's fire rate in RPM (round per minute)
        /// </summary>
        public float fireRate = 650;
        /// <summary>
        /// Weapon's muzzle velocity in m/s (meters per second)
        /// </summary>
        public float muzzleVelocity = 900;
        /// <summary>
        /// Firemodes available for this weapon
        /// </summary>
        public FireMode[] fireModes = { FireMode.Semi };
        [Space]
        [Title("Ammo Settings")]
        public ProjectileController projectilePrefab;
        [Space]
        public int magSize = 30;
        public bool hasSpareAmmo = true;
        [ShowIf("hasSpareAmmo")]
        public int spareAmmoSize = 300;
        [Title("Aim Settings")]
        public bool canAim = true;
    }
}
