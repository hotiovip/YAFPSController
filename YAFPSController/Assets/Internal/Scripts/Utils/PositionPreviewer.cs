using UnityEngine;

using Hotiovip.YAFPSController.Attributes;
using Hotiovip.YAFPSController.Items;

namespace Hotiovip.YAFPSController.Utils
{
    public class PositionPreviewer : MonoBehaviour
    {
        [SerializeField]
        private PosRotData posRotData;

        [Title("PosRotData Previewing")]
        [Button(nameof(StartPreviewDefaultPosRot), "Start Preview DefaultPosRot")]
        public bool startPreviewPosRot;
        [Button(nameof(StartPreviewAimingPosRot), "Start Preview AimingPosRot")]
        public bool startPreviewAimingPosRot;
        [Space]
        [Button(nameof(StopPreview), "Stop Preview")]
        public bool stopPreview;

        [Title("PosRotData Creation")]
        [Tooltip("If left empty, 'NoName_' with a random number will be used as name. The script will automatically add '_PosRotData' at the end.")]
        public string scriptableObjectName = "";
        public string savePath = "Assets/";
        [Button(nameof(SaveAsDefaultPosRotData), "Save As DefaultPosRotData")]
        public bool saveDefaultPosRotData;
        [Button(nameof(SaveAsAimingPosRotData), "Save As AimingPosRotData")]
        public bool saveAimingPosRotData;

#if UNITY_EDITOR
        // TODO: Change path to file's path when a posRotData SO is selected

        /// <summary>
        /// Builds the asset name, for example "ItemName_PosRotData.asset".
        /// </summary>
        private void BuildScriptableObjectName()
        {
            // Set ScriptableObject asset name
            if (scriptableObjectName == "" || scriptableObjectName == null)
            {
                scriptableObjectName = $"NoName_{Random.Range(0, 10000)}.asset";
            }

            if (!scriptableObjectName.Contains("_PosRotData.asset")) scriptableObjectName += "_PosRotData.asset";
        }
        private void SaveScriptableObject(PosRotData posRotData)
        {
            PosRotData existingPosRotData = GetScriptableObject(savePath + scriptableObjectName);

            if (existingPosRotData != null)
            {
                // Mark the existing ScriptableObject as dirty
                UnityEditor.EditorUtility.SetDirty(existingPosRotData);
            }
            else
            {
                // Save as new ScriptableObject
                UnityEditor.AssetDatabase.CreateAsset(posRotData, savePath + scriptableObjectName);
            }
            
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();

        }
        private PosRotData GetScriptableObject(string path)
        {
            return UnityEditor.AssetDatabase.LoadAssetAtPath<PosRotData>(path);
        }

        public void StartPreviewDefaultPosRot()
        {
            if (posRotData == null) return;

            transform.localPosition = posRotData.defaultPosition;
            transform.localRotation = posRotData.defaultRotation;
        }
        public void StartPreviewAimingPosRot()
        {
            if (posRotData == null) return;

            transform.localPosition = posRotData.aimPosition;
            transform.localRotation = posRotData.aimRotation;
        }
        public void StopPreview()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        public void SaveAsDefaultPosRotData()
        {
            BuildScriptableObjectName();

            // Load existing ScriptableObject
            PosRotData posRotData = GetScriptableObject(savePath + scriptableObjectName);
            // Instantiate new ScriptableObject if no one was found
            if (posRotData == null) posRotData = ScriptableObject.CreateInstance<PosRotData>();

            // Set properties
            posRotData.defaultPosition = transform.localPosition;
            posRotData.defaultRotation = transform.localRotation;

            SaveScriptableObject(posRotData);
        }
        public void SaveAsAimingPosRotData()
        {
            BuildScriptableObjectName();

            // Load existing ScriptableObject
            PosRotData posRotData = GetScriptableObject(savePath + scriptableObjectName);
            // Instantiate new ScriptableObject if no one was found
            if (posRotData == null) posRotData = ScriptableObject.CreateInstance<PosRotData>();

            // Set properties
            posRotData.aimPosition = transform.localPosition;
            posRotData.aimRotation = transform.localRotation;

            SaveScriptableObject(posRotData);
        }  
#endif
    }
}
