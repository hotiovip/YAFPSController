using Hotiovip.YAFPSController.Attributes;
using UnityEngine;

namespace Hotiovip.YAFPSController.Items.Weapons
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
        public Projectile projectilePrefab;
        [Space]
        public int magSize = 30;
        [Tooltip("If set to true the weapon will use and have limited ammo (spare ammo).")]
        public bool shouldUseSpareAmmo = true;
        [ShowIf("shouldUseSpareAmmo")]
        public int spareAmmoSize = 300;


        [Title("Aim Settings")]
        public bool canAim = true;
        [ShowIf("canAim")]
        public float aimSwayMultiplier = 0.5f;
    }
}
