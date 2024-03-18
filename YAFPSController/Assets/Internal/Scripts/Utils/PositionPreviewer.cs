using Hotiovip.YAFPSController.Attributes;
using UnityEngine;

namespace Hotiovip.YAFPSController.Utils
{
    public class PositionPreviewer : MonoBehaviour
    {
        [SerializeField]
        [Required]
        private ItemData itemData;

        [Title("PosRotData Previewing")]
        [Button(nameof(StartPreviewPosition), "Start Preview Positon")]
        public bool startPreviewPositionButton;
        [Button(nameof(StopPreviewPosition), "Stop Preview Position")]
        public bool stopPreviewPositionButton;

        [Title("PosRotData Creation")]
        [Tooltip("If left empty the item's name from the given itemData will be used. It will look like this 'itemName_PosRotData'. ")]
        public string posRotDataName = "PosRotData";
        public string savePath = "Assets/";
        [Button(nameof(SavePosRotData), "Save PosRot Data")]
        public bool savePosRotData;

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

        public void SavePosRotData()
        {
            PosRotData posRotData = new PosRotData();
            posRotData.position = transform.localPosition;
            posRotData.rotation = transform.localRotation;
        }
    }
}
