using Sirenix.OdinInspector;
using UnityEngine;

namespace Hotiovip.YAFPSController.Weapon
{
    /// <summary>
    /// Holds all the projectile-related data.
    /// </summary>
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "YAFPSController/ProjectileData", order = 2)]
    public class ProjectileData : ScriptableObject
    {
        [Title("General Settings")]
        [Tooltip("How much seconds the bullet stays before it gets deactivated, if it hits nothing.")]
        public float deactivationTime = 3f;

        [Title("Projectile Settings")]
        [Tooltip("Mass [kg]")]
        public float mass = 0.2f;
        [Tooltip("Radius [m]")]
        public float radius = 0.05f;
        [Tooltip("Coefficients, which is a value you can't calculate - you have to simulate it in a wind tunnel " +
            "and they also depends on the speed, so we pick some average value." +
            " Drag coefficient (Tesla Model S has the drag coefficient 0.24)")]
        public float dragCoefficient = 0.5f;
        [Tooltip("Lift coefficient")]
        public float liftCoefficient = 0.0f;


        // TODO: MOVE SOMEWHERE ELSE (WIND CLASS SINGLETON)
        [Tooltip("Wind speed [m/s]")]
        public Vector3 windSpeedVector = new Vector3(0f, 0f, 0f);

        // TODO: MOVE SOMEWHERE ELSE (WIND CLASS SINGLETON)
        [Tooltip("The density of the medium the bullet is travelling in, which in this case is air at 15 degrees [kg/m^3].")]
        public float airDensity = 1.225f;
    }
}
