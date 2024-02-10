using UnityEngine;

namespace Hotiovip.YAFPSController.Wind
{
    public class WindManager : MonoBehaviour
    {
        public static WindManager Instance { get; private set; }

        [SerializeField]
        [Tooltip("Wind speed [m/s]")]
        private Vector3 windSpeedVector = new Vector3(0f, 0f, 0f);
        [SerializeField]
        [Tooltip("The density of the medium the bullet is travelling in, which in this case is air at 15 degrees [kg/m^3].")]
        private float airDensity = 1.225f;

        private void Awake()
        {
            // Making sure there is always one and only one of this class
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                // Settings the WindManager instance to this script
                Instance = this;
            }
        }

        public Vector3 GetWindSpeedVector() => windSpeedVector;
        public float GetAirDensity() => airDensity;
    }
}
