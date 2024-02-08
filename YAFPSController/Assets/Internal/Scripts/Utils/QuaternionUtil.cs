using UnityEngine;

namespace Hotiovip.YAFPSController.Utils
{
    /*
    Copyright 2016 Max Kaufmann
    Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
    The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
    */

    public static class QuaternionUtil
    {
        public static Quaternion AngVelToDeriv(Quaternion Current, Vector3 AngVel)
        {
            var Spin = new Quaternion(AngVel.x, AngVel.y, AngVel.z, 0f);
            var Result = Spin * Current;
            return new Quaternion(0.5f * Result.x, 0.5f * Result.y, 0.5f * Result.z, 0.5f * Result.w);
        }

        public static Vector3 DerivToAngVel(Quaternion Current, Quaternion Deriv)
        {
            var Result = Deriv * Quaternion.Inverse(Current);
            return new Vector3(2f * Result.x, 2f * Result.y, 2f * Result.z);
        }

        public static Quaternion IntegrateRotation(Quaternion Rotation, Vector3 AngularVelocity, float DeltaTime)
        {
            if (DeltaTime < Mathf.Epsilon) return Rotation;
            var Deriv = AngVelToDeriv(Rotation, AngularVelocity);
            var Pred = new Vector4(
                    Rotation.x + Deriv.x * DeltaTime,
                    Rotation.y + Deriv.y * DeltaTime,
                    Rotation.z + Deriv.z * DeltaTime,
                    Rotation.w + Deriv.w * DeltaTime
            ).normalized;
            return new Quaternion(Pred.x, Pred.y, Pred.z, Pred.w);
        }

        public static Quaternion SmoothDamp(Quaternion currentRotation, Quaternion targetRotation, ref Quaternion velocity, float smoothTime)
        {
            if (Time.deltaTime < Mathf.Epsilon) return currentRotation;
            // account for double-cover
            var Dot = Quaternion.Dot(currentRotation, targetRotation);
            var Multi = Dot > 0f ? 1f : -1f;
            targetRotation.x *= Multi;
            targetRotation.y *= Multi;
            targetRotation.z *= Multi;
            targetRotation.w *= Multi;
            // smooth damp (nlerp approx)
            var Result = new Vector4(
                Mathf.SmoothDamp(currentRotation.x, targetRotation.x, ref velocity.x, smoothTime),
                Mathf.SmoothDamp(currentRotation.y, targetRotation.y, ref velocity.y, smoothTime),
                Mathf.SmoothDamp(currentRotation.z, targetRotation.z, ref velocity.z, smoothTime),
                Mathf.SmoothDamp(currentRotation.w, targetRotation.w, ref velocity.w, smoothTime)
            ).normalized;

            // ensure deriv is tangent
            var derivError = Vector4.Project(new Vector4(velocity.x, velocity.y, velocity.z, velocity.w), Result);
            velocity.x -= derivError.x;
            velocity.y -= derivError.y;
            velocity.z -= derivError.z;
            velocity.w -= derivError.w;

            return new Quaternion(Result.x, Result.y, Result.z, Result.w);
        }
    }
}
