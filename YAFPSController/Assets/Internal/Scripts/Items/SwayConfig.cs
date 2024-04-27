using Hotiovip.YAFPSController.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hotiovip.YAFPSController.Items
{
    [System.Serializable]
    public class SwayConfig
    {
        [Title("Look Sway Settings")]
        [Tooltip("If this variable is false, then no sway will be applied to the item.")]
        public bool canLookSway = true;

        [Tooltip("X: on itself | Y: left-right | Z: down-up")]
        public Vector3 lookSwayForce = new Vector3(1, 1, 1);
        [Tooltip("1 = positive direction (up, forward, right) and -1 = negative direction (down, back, left)")]
        public Vector3 lookSwayDirection = new Vector3(-1, -1, 1);
        [Tooltip("Max values that can be reached on each axis. (Maximum)")]
        public Vector3 maxLookSwayVector = new Vector3(5, 3, 3);
        [Tooltip("Min values that can be reached on each axis. (Least)")]
        public Vector3 minLookSwayVector = new Vector3(-5, -3, -3);
        [Tooltip("The time that the weapons need to return to their original axis. More is smoother, less is snappier.")]
        public float lookSwaySmoothTime = 5;


        [Title("Movement Sway Settings")]
        public bool canMovementSway = true;

        [Tooltip("X: on itself | Y: left-right | Z: down-up")]
        public Vector3 movementSwayForce = new Vector3(1, 1, 1);
        [Tooltip("1 = positive direction (up, forward, right) and -1 = negative direction (down, back, left)")]
        public Vector3 movementSwayDirection = new Vector3(-1, -1, 1);
        [Tooltip("Max values that can be reached on each axis. (Maximum)")]
        public Vector3 maxMovementSwayVector = new Vector3(5, 3, 3);
        [Tooltip("Min values that can be reached on each axis. (Least)")]
        public Vector3 minMovementSwayVector = new Vector3(-5, -3, -3);
        [Tooltip("The time that the weapons need to return to their original axis. More is smoother, less is snappier.")]
        public float movementSwaySmoothTime = 5;
    }
}
