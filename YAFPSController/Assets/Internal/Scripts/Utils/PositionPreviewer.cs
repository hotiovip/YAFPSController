using Hotiovip.YAFPSController.Attributes;
using UnityEngine;

namespace Hotiovip.YAFPSController.Utils
{
    public class PositionPreviewer : MonoBehaviour
    {
        [SerializeField]
        [Required]
        private ItemData itemData;

        [Button(nameof(StartPreviewPosition), "Start Preview Positon")]
        public bool startPreviewPositionButton;
        [Button(nameof(StopPreviewPosition), "Stop Preview Position")]
        public bool stopPreviewPositionButton;


        public void StartPreviewPosition()
        {
            if (itemData == null) return;

            //transform.localPosition = itemData.itemPosition;
            //transform.localRotation = itemData.itemRotation;
            transform.localPosition = itemData.defaultPosRot.position;
            transform.localRotation = itemData.defaultPosRot.rotation;
        }
        public void StopPreviewPosition()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }


    }
}
