using Sirenix.OdinInspector;
using UnityEngine;

namespace Hotiovip.YAFPSController.Utils
{
    public class PositionPreviewer : MonoBehaviour
    {
        [SerializeField]
        private ItemData itemData;

        [Button("Start Preview Position")]
        private void StartPreviewPosition()
        {
            if (itemData == null) return;

            transform.localPosition = itemData.itemPosition;
            transform.localRotation = itemData.itemRotation;            
        }

        [Button("Stop Preview Position")]
        private void StopPreviewPosition()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }


    }
}
