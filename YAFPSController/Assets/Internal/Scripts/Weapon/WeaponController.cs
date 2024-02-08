using UnityEngine;

namespace Hotiovip.YAFPSController
{
    /// <summary>
    /// Used for weapons (shooting items). Can be used as a superclass to make new custom weapons.
    /// </summary>
    public class WeaponController : Item
    {
        /// <summary>
        /// Used for weapon-specific data. Use this.itemData for item-specific data.
        /// </summary>
        protected WeaponData weaponData;

        protected override void Start()
        {
            base.Start();

            weaponData = itemData as WeaponData;
            if (weaponData == null) Debug.LogError($"The assigned Data for the weapon '{itemData.name}' is not a WeaponData!");
        }

        /// <summary>
        /// called for shooting.
        /// </summary>
        protected override void StartPrimaryUse()
        {
            base.StartPrimaryUse();
        }

        /// <summary>
        /// Called for aiming.
        /// </summary>
        protected override void StopPrimaryUse()
        {
            base.StopPrimaryUse();
        }
    }
}
