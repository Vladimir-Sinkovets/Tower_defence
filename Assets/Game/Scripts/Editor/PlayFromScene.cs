using Assets.Game.Scripts.Shared;
using UnityEditor;
using UnityEditor.SceneManagement;

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
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        [MenuItem(MenuPath)]
        private static void Toggle()
        {
            Enabled = !Enabled;
            Menu.SetChecked(MenuPath, Enabled);
        }

        [MenuItem(MenuPath, true)]
        private static bool ToggleValidate()
        {
            Menu.SetChecked(MenuPath, Enabled);
            return true;
        }

        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (!Enabled)
                return;

            if (state != PlayModeStateChange.ExitingEditMode)
                return;

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(SceneNames.Bootstrap);
            }
            else
            {
                EditorApplication.isPlaying = false;
            }
        }
    }
}
