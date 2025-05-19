using UnityEngine;
using UnityEditor;
using ProjectMM.Scope.Gameplay.Level;
using System.IO;
using UnityEditor.AddressableAssets;

namespace ProjectMM.Editor
{
    public class LevelEditor : EditorWindow
    {
        private string _levelName = "LevelData";
        private LevelData _selectedLevelData;

        [MenuItem("ProjectMM/Level Editor")]
        public static void ShowWindow()
        {
            GetWindow<LevelEditor>("Level Editor");
        }

        private void OnGUI()
        {
            GUILayout.Label("Create New LevelData", EditorStyles.boldLabel);
            _levelName = EditorGUILayout.TextField("Level Name", _levelName);

            if (GUILayout.Button("Create New LevelData"))
            {
                CreateNewLevelData();
            }

            GUILayout.Space(20);
            GUILayout.Label("Edit Existing LevelData", EditorStyles.boldLabel);

            _selectedLevelData = (LevelData)EditorGUILayout.ObjectField("Selected LevelData", _selectedLevelData, typeof(LevelData), false);

            if (_selectedLevelData != null)
            {
                var so = new SerializedObject(_selectedLevelData);
                var dataProp = so.FindProperty("_Data");

                EditorGUILayout.PropertyField(dataProp, true);

                so.ApplyModifiedProperties();

                if (GUILayout.Button("Save Changes"))
                {
                    EditorUtility.SetDirty(_selectedLevelData);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
        }

        private void CreateNewLevelData()
        {
            if (string.IsNullOrEmpty(_levelName))
            {
                return;
            }

            const string folderPath = "Assets/Data/Levels";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                AssetDatabase.Refresh();
            }

            var assetPath = $"{folderPath}/{_levelName}.asset";

            if (File.Exists(assetPath))
            {
                return;
            }

            var newLevelData = CreateInstance<LevelData>();
            AssetDatabase.CreateAsset(newLevelData, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            AddToAddressable(assetPath, _levelName);

            _selectedLevelData = newLevelData;
        }

        private static void AddToAddressable(string assetPath, string address)
        {
            var settings = AddressableAssetSettingsDefaultObject.Settings;

            if (settings == null)
            {
                return;
            }

            var guid = AssetDatabase.AssetPathToGUID(assetPath);
            if (string.IsNullOrEmpty(guid))
            {
                return;
            }

            var defaultGroup = settings.DefaultGroup;
            if (defaultGroup == null)
            {
                return;
            }

            var entry = settings.CreateOrMoveEntry(guid, defaultGroup);
            entry.address = address;

            AssetDatabase.SaveAssets();
        }
    }
}