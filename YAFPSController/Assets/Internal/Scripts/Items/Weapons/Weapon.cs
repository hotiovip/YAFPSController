using UnityEngine;

using Hotiovip.YAFPSController.Utils;

namespace Hotiovip.YAFPSController.Items.Weapons
{
    /// <summary>
    /// Base class for managing weapons, primarily used for shooting items.
    /// This class provides functionality to create custom weapons and manages aspects such as firing behavior, reloading, and aiming.
    /// Derived classes can implement specific weapon types by overriding or extending the provided functionality.
    /// </summary>
    public class Weapon : Item
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

        // Aim variables
        protected bool aimingFinished;

        // Animations hash values
        protected readonly int actionHash = Animator.StringToHash("Action");
        #endregion

        protected override void Start()
        {
            // Get weapon data
            weaponData = itemData as WeaponData;
            if (weaponData == null) Debug.LogError($"The assigned Data for the weapon '{itemData.name}' is not a WeaponData!");

            // Initialize values that need a default value
            currentFireMode = weaponData.fireModes[0];
            currentMagSize = weaponData.magSize;
            currentSpareAmmoSize = weaponData.spareAmmoSize;
        }
        protected override void Update()
        {
            UpdatePrimaryUse();
            UpdateSecondaryUse();

            // Remember to call base Update()
            base.Update();
        }

        #region PRIMARY USE
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
            isUsingPrimary = true;

            PrimaryUse();

            // Do weapon specific primary use start logic
            fireTimer = 0;
        }
        protected override void PrimaryUse()
        {
            projectilePool.GetPool().Get().SetStartValues(muzzle.position, muzzle.forward, weaponData.muzzleVelocity);
        }
        protected override void StopPrimaryUse()
        {
            isUsingPrimary = false;
        }
        #endregion

        #region SECONDARY USE
        protected override void UpdateSecondaryUse()
        {
            // Aim in
            if (isUsingSecondary && CanSecondaryUse())
            {
                //positionHolderInterp.RotationSmoothDamp(weaponData.posRotData.aimRotation, 6f, InterpolationSpace.Local);
                //positionHolderInterp.PositionSmoothDamp(weaponData.posRotData.aimPosition, 6f, InterpolationSpace.Local);
            }
            else if (!isUsingSecondary || !CanSecondaryUse())
            {
                StopSecondaryUse();

                //positionHolderInterp.RotationSmoothDamp(weaponData.posRotData.defaultRotation, 10f, InterpolationSpace.Local);
                //positionHolderInterp.PositionSmoothDamp(weaponData.posRotData.defaultPosition, 10f, InterpolationSpace.Local);
            }
        }
        protected override void StartSecondaryUse()
        {
            if (!CanSecondaryUse()) return;

            isUsingSecondary = true;
        }
        protected override void StopSecondaryUse()
        {
            isUsingSecondary = false;
        }
        #endregion

        /// <summary>
        /// Reload logic.
        /// </summary>
        protected override void Action()
        {
            if (!CanPerformAction()) return;

            isPerformingAction = true;

            if (animator) animator.SetBool(actionHash, true);

            // Use spare ammo to reload
            if (weaponData.shouldUseSpareAmmo)
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
            if (animator) animator.SetBool(actionHash, false);
        }

        // TODO: Fix This. Weapon move strange when aim multiplier is applied.
        /// <summary>
        /// Custom sway logic. Works like normal sway but takes aiming in consideration.
        /// </summary>
        protected override Quaternion CalculateLookSway()
        {
            Quaternion lookSwayTarget = base.CalculateLookSway();

            if (isUsingSecondary)
            {
                lookSwayTarget.eulerAngles = new Vector3(lookSwayTarget.eulerAngles.x * weaponData.aimSwayMultiplier, 
                    lookSwayTarget.eulerAngles.y * weaponData.aimSwayMultiplier, 
                    lookSwayTarget.eulerAngles.z * weaponData.aimSwayMultiplier);

                Debug.Log($"Changed | {weaponData.aimSwayMultiplier}");
            }

            return lookSwayTarget;
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
