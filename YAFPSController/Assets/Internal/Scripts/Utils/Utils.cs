using UnityEngine;

namespace Hotiovip.YAFPSController.Utils
{
    public class TransformInterp
    {
        private Transform transform;

        private Vector3 positionVelocity;
        private Quaternion rotationVelocity;
        
        public TransformInterp(Transform targetTransform)
        {
            transform = targetTransform;
            positionVelocity = Vector3.zero;
            rotationVelocity = Quaternion.identity;
        }

        public void Reset()
        {
            positionVelocity = Vector3.zero;
            rotationVelocity = Quaternion.identity;
        }
        public void ResetPosition()
        {
            positionVelocity = Vector3.zero;
        }
        public void ResetRotation()
        {
            rotationVelocity = Quaternion.identity;
        }

        public void PositionSmoothDamp(Vector3 targetPosition, float smoothTime, InterpolationSpace interpolationSpace = InterpolationSpace.Local) 
        {
            if (interpolationSpace.Equals(InterpolationSpace.Local))
            {
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref positionVelocity, smoothTime * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref positionVelocity, smoothTime * Time.deltaTime);

            }
        }
        public void RotationSmoothDamp(Quaternion targetRotation, float smoothTime, InterpolationSpace interpolationSpace = InterpolationSpace.Local)
        {
            if (interpolationSpace.Equals(InterpolationSpace.Local))
            {
                transform.localRotation = QuaternionUtil.SmoothDamp(transform.localRotation, targetRotation, ref rotationVelocity, smoothTime * Time.deltaTime);
            }
            else
            {
                transform.rotation = QuaternionUtil.SmoothDamp(transform.rotation, targetRotation, ref rotationVelocity, smoothTime * Time.deltaTime);

            }
        }
    }

    public enum InterpolationSpace
    {
        Local,
        Global
    }
}