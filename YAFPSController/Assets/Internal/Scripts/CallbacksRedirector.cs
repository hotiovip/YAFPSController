using UnityEngine;
using UnityEngine.Events;

namespace Hotiovip.YAFPSController
{
    public class CallbacksRedirector : MonoBehaviour
    {
        public UnityEvent onActionEnded;

        public void OnActionEnded() 
        {
            onActionEnded?.Invoke();
        }
    }
}
