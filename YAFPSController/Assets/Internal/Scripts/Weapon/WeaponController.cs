using Hotiovip.YAFPSController.Utils;
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
        protected Transform muzzle;
        [SerializeField]
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

            // Get weapon data
            weaponData = itemData as WeaponData;
            if (weaponData == null) Debug.LogError($"The assigned Data for the weapon '{itemData.name}' is not a WeaponData!");

            // Initialize values that need a default value
            currentFireMode = weaponData.fireModes[0];
            currentMagSize = weaponData.magSize;
            currentSpareAmmoSize = weaponData.spareAmmoSize;
        }

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
        protected override void StartPrimaryUse()
        {
            base.StartPrimaryUse();

            fireTimer = 0;
        }
        protected override void PrimaryUse()
        {
            base.PrimaryUse();

            projectilePool.GetPool().Get().SetStartValues(muzzle.position, muzzle.forward, weaponData.muzzleVelocity);
        }
        protected override void StopPrimaryUse()
        {
            base.StopPrimaryUse();
        }

        protected override void UpdateSecondaryUse()
        {
            // Return if it cannot secondary use
            if (!CanSecondaryUse()) return;

            StartSecondaryUse();
        }
        protected override void StartSecondaryUse()
        {
            // Aim
            positionHolder.localRotation = QuaternionUtil.SmoothDamp(positionHolder.localRotation.)
        }
        protected override void SecondaryUse()
        {
            
        }

        protected override void StopSecondaryUse()
        {
            
        }

        /// <summary>
        /// Reload logic.
        /// </summary>
        protected override void Action()
        {
            if (!CanPerformAction()) return;

            isPerformingAction = true;

            // Use spare ammo to reload
            if (weaponData.hasSpareAmmo)
            {
                // Calculate needed ammo amount
                int neededAmmo = weaponData.magSize - currentMagSize;

                // We should always have enough spare ammo because this check is also in CanPerformAction()
                // Enough spare ammo
                if (neededAmmo <= currentSpareAmmoSize)
                {
                    currentSpareAmmoSize -= neededAmmo;
                    currentMagSize += neededAmmo;
                }
                // Not enough spare ammo
                else
                {
                    // Cannot reload
                }
            }
            // No spare ammo needed
            else
            {
                currentMagSize = weaponData.magSize;
            }
        }
        /// <summary>
        /// Reload end logic.
        /// </summary>
        protected override void ActionEnded()
        {
            base.ActionEnded();
        }

        #region GETTERS
        public override bool CanPrimaryUse() => currentMagSize > 0 && !isPerformingAction;
        public override bool CanSecondaryUse() => weaponData.canAim && !isPerformingAction;
        public override bool CanPerformAction() => !isUsingPrimary && currentMagSize < weaponData.magSize && (weaponData.magSize - currentMagSize) <= currentSpareAmmoSize;

        public WeaponData GetWeaponData() => weaponData;
        public Transform GetMuzzle() => muzzle;
        #endregion
    }
}
