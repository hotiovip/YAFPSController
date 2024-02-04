using UnityEngine;

namespace Hotiovip.YAFPSController
{
    public class WeaponController : Item
    {
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
