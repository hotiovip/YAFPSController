using Sirenix.OdinInspector;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace Hotiovip.YAFPSController.Weapon
{
    /// <summary>
    /// Used for weapons (shooting items). Can be used as a superclass to make new custom weapons.
    /// </summary>
    public class WeaponController : Item
    {
        [SerializeField]
        [Required]
        private Transform muzzle;

        /// <summary>
        /// Used for weapon-specific data. Use this.itemData for item-specific data.
        /// </summary>
        protected WeaponData weaponData;

        protected int currentMagSize;
        protected float fireTimer;
        protected int bursts;
        protected FireMode currentFireMode;
        protected override void Start()
        {
            base.Start();

            weaponData = itemData as WeaponData;
            if (weaponData == null) Debug.LogError($"The assigned Data for the weapon '{itemData.name}' is not a WeaponData!");

            currentFireMode = weaponData.fireModes[0];
            currentMagSize = weaponData.magSize;
        }


        /// <summary>
        /// Calls PrimaryUse() if all conditions are met.
        /// </summary>
        protected override void UpdatePrimaryUse()
        {
            if (!CanFire() || !isPrimaryUsing)
            {
                if (isPrimaryUsing) StopPrimaryUse();
                return;
            }

            if (currentFireMode != FireMode.Semi && fireTimer >= 60f / weaponData.fireRate)
            {
                PrimaryUse();

                if (currentFireMode == FireMode.Burst)
                {
                    bursts--;

                    if (bursts == 0)
                    {
                        fireTimer = -1f;
                        StopPrimaryUse();
                    }
                    else
                    {
                        fireTimer = 0f;
                    }
                }
                else
                {
                    fireTimer = 0f;
                }
            }

            if (fireTimer >= 0f)
            {
                fireTimer += Time.deltaTime;
            }
        }
        /// <summary>
        /// Start the fire process.
        /// </summary>
        protected override void StartPrimaryUse()
        {
            base.StartPrimaryUse();

            fireTimer = 0;
        }
        /// <summary>
        /// Fire logic.
        /// </summary>
        protected override void PrimaryUse()
        {
            base.PrimaryUse();

            GameObject bullet = Instantiate(weaponData.bulletPrefab, transform.position, transform.rotation);
            bullet.transform.parent = inventoryController.GetBulletsHolder();
        }
        /// <summary>
        /// Stops the fire process.
        /// </summary>
        protected override void StopPrimaryUse()
        {
            base.StopPrimaryUse();
        }


        public bool CanFire() => currentMagSize > 0;
    }
}
