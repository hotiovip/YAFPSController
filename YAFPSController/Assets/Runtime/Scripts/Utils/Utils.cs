using UnityEngine;

namespace Hotiovip.YAFPSController.Utils
{
    public class FloatLerp
    {
        private float currentVelocity;

        public FloatLerp()
        {
            currentVelocity = 0;
        }

        public float SmoothDamp(float currentFloat, float targetFloat, float smoothTime)
        {
            return Mathf.SmoothDamp(currentFloat, targetFloat, ref currentVelocity, smoothTime);
        }
    }

    public class Vector2Lerp
    {
        public Vector2 currentVelocity;

        public Vector2Lerp()
        {
            currentVelocity = Vector3.zero;
        }

        public Vector2 SmoothDamp(Vector2 currentVector, Vector2 targetVector, float smoothTime)
        {
            return Vector2.SmoothDamp(currentVector, targetVector, ref currentVelocity, smoothTime);
        }
    }

    public class Vector3Lerp
    {
        public Vector3 currentVelocity;

        public Vector3Lerp()
        {
            currentVelocity = Vector3.zero;
        }

        public Vector3 SmoothDamp(Vector3 currentVector, Vector3 targetVector, float smoothTime)
        {
            return Vector3.SmoothDamp(currentVector, targetVector, ref currentVelocity, smoothTime);
        }
    }

    public class TransformLerp
    {

    }
}