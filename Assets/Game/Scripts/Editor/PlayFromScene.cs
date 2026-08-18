using Assets.Game.Scripts.Shared;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Assets.Game.Scripts.Editor
{
    [InitializeOnLoad]
    public static class PlayFromScene
    {
        private const string PrefKey = "PlayFromScene_Enabled";
        private const string MenuPath = "Tools/Start Scene/Use Start Scene";

        private static bool Enabled
        {
            get => EditorPrefs.GetBool(PrefKey, true);
            set => EditorPrefs.SetBool(PrefKey, value);
        }

        static PlayFromScene()
        {
            ApplyStartScene();
        }

        [MenuItem(MenuPath)]
        private static void Toggle()
        {
            Enabled = !Enabled;
            ApplyStartScene();
        }

        [MenuItem(MenuPath, true)]
        private static bool ToggleValidate()
        {
            Menu.SetChecked(MenuPath, Enabled);
            return true;
        }

        private static void ApplyStartScene()
        {
            Menu.SetChecked(MenuPath, Enabled);

            if (!Enabled)
            {
                EditorSceneManager.playModeStartScene = null;
                return;
            }

            var bootstrapScene = GetBootstrapScene();

            if (bootstrapScene == null)
            {
                EditorSceneManager.playModeStartScene = null;
                Debug.LogError($"Bootstrap scene not found at path: {SceneNames.Bootstrap}");
                return;
            }

            EditorSceneManager.playModeStartScene = bootstrapScene;
        }

        private static SceneAsset GetBootstrapScene()
        {
            var guids = AssetDatabase.FindAssets($"t:Scene {SceneNames.Bootstrap}");

            if (guids.Length == 0)
            {
                Debug.LogError($"Bootstrap scene not found: {SceneNames.Bootstrap}");

                EditorSceneManager.playModeStartScene = null;
                return null;
            }

            if (guids.Length > 1)
            {
                Debug.LogError($"Multiple scenes found with name: {SceneNames.Bootstrap}");

                EditorSceneManager.playModeStartScene = null;
                return null;
            }

            var scenePath = AssetDatabase.GUIDToAssetPath(guids[0]);
            
            return AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
        }
    }
}