using UnityEngine;

using Hotiovip.YAFPSController.Wind;

namespace Hotiovip.YAFPSController.Weapon
{
    public static class BallisticMethods
    {
        private static Vector3 gravityVector = new Vector3(0f, -9.81f, 0f);

        /// <summary>
        /// Calculates bullet trajectory using backward euler method. It tends to undershoot the target.
        /// It works yelds similiar results as the unity physics engine.
        /// </summary>
        /// <param name="h"></param>
        /// <param name="currentPosition"></param>
        /// <param name="currentVelocity"></param>
        /// <param name="newPosition"></param>
        /// <param name="newVelocity"></param>
        public static void BackwardEuler(float h, Vector3 currentPosition, Vector3 currentVelocity, out Vector3 newPosition, out Vector3 newVelocity)
        {
            // Init acceleration
            // Gravity
            Vector3 accelerationFactor = Physics.gravity;

            // Main algorithm
            newVelocity = currentVelocity + h * accelerationFactor;

            newPosition = currentPosition + h * newVelocity;
        }

        /// <summary>
        /// Calculates bullet trajectory using forward euler method. It works the other way as backward euler. It tends to overshoot the target.
        /// It works yelds similiar results as the unity physics engine.
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="currentPosition"></param>
        /// <param name="currentVelocity"></param>
        /// <param name="newPosition"></param>
        /// <param name="newVelocity"></param>
        public static void ForwardEuler(float timeStep, Vector3 currentPosition, Vector3 currentVelocity, out Vector3 newPosition, out Vector3 newVelocity)
        {
            // Add all factors that affects the acceleration
            // Gravity
            Vector3 accelerationFactor = gravityVector;


            // Calculate the new velocity and position
            // y_k+1 = y_k + timeStep * f(t_k, y_k)

            newVelocity = currentVelocity + timeStep * accelerationFactor;

            newPosition = currentPosition + timeStep * currentVelocity;
        }

        /// <summary>
        /// Calculates bullet trajectory using heuns method. It doe
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="currentPosition"></param>
        /// <param name="currentVelocity"></param>
        /// <param name="upVec">It is a vector perpendicular (in the upwards direction) to the direction the bullet is travelling in.
        /// Only needed to calculate lift force</param>
        /// <param name="projectileData"></param>
        /// <param name="newPosition"></param>
        /// <param name="newVelocity"></param>
        public static void Heuns(float timeStep, Vector3 currentPosition, Vector3 currentVelocity, Vector3 upVector, ProjectileData projectileData, out Vector3 newPosition, out Vector3 newVelocity)
        {
            //Add all factors that affects the acceleration
            //Gravity
            Vector3 accFactorEuler = gravityVector;
            //Drag
            accFactorEuler += CalculateBulletDragAcc(currentVelocity, projectileData);
            //Lift 
            accFactorEuler += CalculateBulletLiftAcc(currentVelocity, projectileData, upVector);


            //Calculate the new velocity and position
            //y_k+1 = y_k + timeStep * 0.5 * (f(t_k, y_k) + f(t_k+1, y_k+1))
            //Where f(t_k+1, y_k+1) is calculated with Forward Euler: y_k+1 = y_k + timeStep * f(t_k, y_k)

            //Step 1. Find new pos and new vel with Forward Euler
            Vector3 newVelEuler = currentVelocity + timeStep * accFactorEuler;

            //Step 2. Heuns method's final step
            //If we take drag into account, then acceleration is not constant - it also depends on the velocity
            //So we have to calculate another acceleration factor
            //Gravity
            Vector3 accFactorHeuns = gravityVector;
            //Drag
            //This assumes that windspeed is constant between the steps, which it should be because wind doesnt change that often
            accFactorHeuns += CalculateBulletDragAcc(newVelEuler, projectileData);
            //Lift 
            accFactorHeuns += CalculateBulletLiftAcc(newVelEuler, projectileData, upVector);

            newVelocity = currentVelocity + timeStep * 0.5f * (accFactorEuler + accFactorHeuns);

            newPosition = currentPosition + timeStep * 0.5f * (currentVelocity + newVelEuler);
        }



        /// <summary>
        /// Calculate the bullet's drag acceleration.
        /// </summary>
        /// <param name="projectileVelocity"></param>
        /// <param name="projectileData"></param>
        /// <returns></returns>
        public static Vector3 CalculateBulletDragAcc(Vector3 projectileVelocity, ProjectileData projectileData)
        {
            //If you have a wind speed in your game, you can take that into account here:
            //https://www.youtube.com/watch?v=lGg7wNf1w-k
            Vector3 bulletVelRelativeToWindVel = projectileVelocity - WindManager.Instance.GetWindSpeedVector();

            //Step 1. Calculate the bullet's drag force [N]
            //https://en.wikipedia.org/wiki/Drag_equation
            //F_drag = 0.5 * rho * v^2 * C_d * A 

            //The velocity of the bullet [m/s]
            float v = bulletVelRelativeToWindVel.magnitude;
            //The bullet's cross section area [m^2]
            float A = Mathf.PI * projectileData.radius * projectileData.radius;

            float dragForce = 0.5f * WindManager.Instance.GetAirDensity() * v * v * projectileData.dragCoefficient * A;


            //Step 2. We need to add an acceleration, not a force, in the integration method [m/s^2]
            //Drag acceleration F = m * a -> a = F / m
            float dragAcc = dragForce / projectileData.mass;

            //SHould be in a direction opposite of the bullet's velocity vector
            Vector3 dragVec = dragAcc * bulletVelRelativeToWindVel.normalized * -1f;


            return dragVec;
        }

        /// <summary>
        /// Calculate the bullet's lift acceleration.
        /// </summary>
        /// <param name="projectileVelocity"></param>
        /// <param name="projectileData"></param>
        /// <param name="projectileUpDirection#"></param>
        /// <returns></returns>
        public static Vector3 CalculateBulletLiftAcc(Vector3 projectileVelocity, ProjectileData projectileData, Vector3 projectileUpDirection)
        {
            //If you have a wind speed in your game, you can take that into account here:
            //https://www.youtube.com/watch?v=lGg7wNf1w-k
            Vector3 bulletVelRelativeToWindVel = projectileVelocity - WindManager.Instance.GetWindSpeedVector();

            //Step 1. Calculate the bullet's lift force [N]
            //https://en.wikipedia.org/wiki/Lift_(force)
            //F_lift = 0.5 * rho * v^2 * S * C_l 

            //The velocity of the bullet [m/s]
            float v = bulletVelRelativeToWindVel.magnitude;
            //Planform (projected) wing area, which is assumed to be the same as the cross section area [m^2]
            float S = Mathf.PI * projectileData.radius * projectileData.radius;

            float liftForce = 0.5f * WindManager.Instance.GetAirDensity() * v * v * S * projectileData.liftCoefficient;

            //Step 2. We need to add an acceleration, not a force, in the integration method [m/s^2]
            //Drag acceleration F = m * a -> a = F / m
            float liftAcc = liftForce / projectileData.mass;

            //The lift force acts in the up-direction = perpendicular to the velocity direction it travels in
            Vector3 liftVec = liftAcc * projectileUpDirection;


            return liftVec;
        }
    }
}
