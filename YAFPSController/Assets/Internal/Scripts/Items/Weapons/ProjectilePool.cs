using UnityEngine;
using UnityEngine.Pool;

namespace Hotiovip.YAFPSController.Items.Weapons
{
    public class ProjectilePool : MonoBehaviour
    {
        [SerializeField]
        private Weapon weapon;

        private WeaponData weaponData;
        private Transform muzzle;

        private Transform poolHolder;
        private ObjectPool<Projectile> pool;

        private void Start()
        {
            weaponData = weapon.GetWeaponData();
            muzzle = weapon.GetMuzzle();

            poolHolder = new GameObject($"{weaponData.itemName}_ProjectilePool").transform;
            pool = new ObjectPool<Projectile>(CreateProjectile, OnTakeProjectileFromPool, OnReturnProjectileToPool, OnDestroyProjectile,
                true, weaponData.magSize, weaponData.magSize + weaponData.magSize / 2);
        }

        private Projectile CreateProjectile()
        {
            // Spawn projectile's instance
            Projectile projectile = Instantiate(weaponData.projectilePrefab, muzzle.position, muzzle.rotation, poolHolder);
            projectile.gameObject.SetActive(false);

            // Set projectile's pool
            projectile.SetPool(pool);

            return projectile;
        }

        /// <summary>
        /// Called when a projectile is taken from the pool.
        /// </summary>
        private void OnTakeProjectileFromPool(Projectile projectile)
        {
            projectile.transform.position = muzzle.position;
            projectile.transform.forward = muzzle.forward;

            projectile.gameObject.SetActive(true);
        }

        /// <summary>
        /// Called when a projectile is returned to the pool.
        /// </summary>
        /// <param name="projectile"></param>
        private void OnReturnProjectileToPool(Projectile projectile)
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
        private void OnDestroyProjectile(Projectile projectile)
        {
            Destroy(projectile.gameObject);
        }

        public ObjectPool<Projectile> GetPool() => pool;
    }
}
