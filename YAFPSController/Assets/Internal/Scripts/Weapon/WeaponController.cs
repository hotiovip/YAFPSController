using Sirenix.OdinInspector;
using UnityEngine;

namespace Hotiovip.YAFPSController.Weapon
{
    /// <summary>
    /// Used for weapons (shooting items). Can be used as a superclass to make new custom weapons.
    /// </summary>
    public class WeaponController : Item
    {
        #region VARIABLES
        [SerializeField]
        [Required]
        protected Transform muzzle;
        [SerializeField]
        [Required]
        protected ProjectilePool projectilePool;

        /// <summary>
        /// Used for weapon-specific data. Use this.itemData for item-specific data.
        /// </summary>
        protected WeaponData weaponData;

        protected int currentMagSize;
        protected int currentSpareAmmoSize;
        protected float fireTimer;
        protected int bursts;
        protected FireMode currentFireMode;
        #endregion

        protected override void Start()
        {
            base.Start();

            weaponData = itemData as WeaponData;
            if (weaponData == null) Debug.LogError($"The assigned Data for the weapon '{itemData.name}' is not a WeaponData!");

            currentFireMode = weaponData.fireModes[0];
            currentMagSize = weaponData.magSize;
            currentSpareAmmoSize = weaponData.spareAmmoSize;
        }


        /// <summary>
        /// Calls PrimaryUse() if all conditions are met.
        /// </summary>
        protected override void UpdatePrimaryUse()
        {
            if (!CanPrimaryUse() || !isUsingPrimary)
            {
                if (isUsingPrimary) StopPrimaryUse();
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

            projectilePool.GetPool().Get().SetStartValues(muzzle.position, muzzle.forward, weaponData.muzzleVelocity);
        }
        /// <summary>
        /// Stops the fire process.
        /// </summary>
        protected override void StopPrimaryUse()
        {
            base.StopPrimaryUse();
        }

        /// <summary>
        /// Reload logic.
        /// </summary>
        protected override void Action()
        {
            if (!CanPerformAction()) return;

            //currentMagSize
        }

        public override bool CanPrimaryUse() => currentMagSize > 0 && !isPerformingAction;
        public override bool CanPerformAction() => !isUsingPrimary;

        public WeaponData GetWeaponData() => weaponData;
        public Transform GetMuzzle() => muzzle;
    }
}
