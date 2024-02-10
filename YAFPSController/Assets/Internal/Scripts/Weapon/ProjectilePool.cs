using UnityEngine;
using UnityEngine.Pool;

namespace Hotiovip.YAFPSController.Weapon
{
    public class ProjectilePool : MonoBehaviour
    {
        [SerializeField]
        private WeaponController weaponController;

        private WeaponData weaponData;
        private Transform muzzle;

        private Transform poolHolder;
        private ObjectPool<ProjectileController> pool;

        private void Start()
        {
            weaponData = weaponController.GetWeaponData();
            muzzle = weaponController.GetMuzzle();

            poolHolder = new GameObject($"{weaponData.itemName}_ProjectilePool").transform;
            pool = new ObjectPool<ProjectileController>(CreateProjectile, OnTakeProjectileFromPool, OnReturnProjectileToPool, OnDestroyProjectile,
                true, weaponData.magSize, weaponData.magSize + weaponData.magSize / 2);
        }

        private ProjectileController CreateProjectile()
        {
            // Spawn projectile's instance
            ProjectileController projectile = Instantiate(weaponData.projectilePrefab, muzzle.position, muzzle.rotation, poolHolder);
            projectile.gameObject.SetActive(false);

            // Set projectile's pool
            projectile.SetPool(pool);

            return projectile;
        }

        /// <summary>
        /// Called when a projectile is taken from the pool.
        /// </summary>
        private void OnTakeProjectileFromPool(ProjectileController projectile)
        {
            projectile.transform.position = muzzle.position;
            projectile.transform.forward = muzzle.forward;

            projectile.gameObject.SetActive(true);
        }

        /// <summary>
        /// Called when a projectile is returned to the pool.
        /// </summary>
        /// <param name="projectile"></param>
        private void OnReturnProjectileToPool(ProjectileController projectile)
        {
            projectile.gameObject.SetActive(false);

            projectile.transform.position = muzzle.position;
            projectile.transform.forward = muzzle.forward;
        }

        /// <summary>
        /// Called when a projectile has to be destroyed instead of returned to the pool.
        /// Maybe because pool is already full.
        /// </summary>
        /// <param name="projectile"></param>
        private void OnDestroyProjectile(ProjectileController projectile)
        {
            Destroy(projectile.gameObject);
        }

        public ObjectPool<ProjectileController> GetPool() => pool;
    }
}
