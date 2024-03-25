using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Hotiovip.YAFPSController.Items.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private ProjectileData projectileData;

        private ObjectPool<Projectile> pool;
        private Coroutine deactivateAfterTimeCoroutine;

        private Vector3 currentPosition;
        private Vector3 currentVelocity;
        private Vector3 newPosition;
        private Vector3 newVelocity;

        private void OnEnable()
        {
            deactivateAfterTimeCoroutine = StartCoroutine(DeactivateBulletAfterTime());
        }
        private void OnDisable()
        {
            StopCoroutine(deactivateAfterTimeCoroutine);
        }
        private void FixedUpdate()
        {
            MoveProjectileOneStep();
        }
        private void OnTriggerEnter(Collider other)
        {
            // Return to the pool
            pool.Release(this);
        }

        private void MoveProjectileOneStep()
        {
            //Use an integration method to calculate the new position of the bullet
            float timeStep = Time.fixedDeltaTime;

            BallisticMethods.Heuns(timeStep, currentPosition, currentVelocity, transform.up, projectileData, out newPosition, out newVelocity);

            //Debug.DrawRay(transform.position, transform.up * 5f);

            //Set the new values to the old values for next update
            currentPosition = newPosition;
            currentVelocity = newVelocity;

            //Add the new position to the bullet
            transform.position = currentPosition;

            //Change so the bullet points in the velocity direction
            transform.forward = currentVelocity.normalized;
        }
        private IEnumerator DeactivateBulletAfterTime()
        {
            float elapsedTime = 0f;

            while (elapsedTime < projectileData.deactivationTime)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Return to the pool
            pool.Release(this);
        }
        /// <summary>
        /// Set starts values so that the projectile can move.
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="startRotation"></param>
        /// <param name="muzzleVelocity"></param>
        public void SetStartValues(Vector3 startPosition, Vector3 startRotation, float muzzleVelocity)
        {
            this.currentPosition = startPosition;
            this.currentVelocity = muzzleVelocity * startRotation;

            transform.position = startPosition;
            transform.forward = startRotation;
        }
        /// <summary>
        /// Sets the pool this projectile belongs to.
        /// </summary>
        /// <param name="pool">Pool</param>
        public void SetPool(ObjectPool<Projectile> pool) 
        {
            this.pool = pool;
        }

    }
}
