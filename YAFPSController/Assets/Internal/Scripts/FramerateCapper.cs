using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hotiovip.YAFPSController
{
    public class FramerateCapper : MonoBehaviour
    {
        public int maxFramerate = 60;

        // Start is called before the first frame update
        void Start()
        {
            Application.targetFrameRate = maxFramerate;
        }
    }
}
