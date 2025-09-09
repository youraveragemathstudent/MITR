// LoadingAssets.cs
using System.IO;
using BepInEx;
using BepInEx.Logging;
using UnityEngine;

namespace MITR_main
{
    public static class LoadingAssets
    {
        public static AssetBundle Bundle { get; private set; }

        // Prefabs you want to use
        public static GameObject Pushbutton79;
        public static GameObject Panel1;

        private const string BundleFile = "mitr_ui_new"; // change if needed

        private const string ButtonShortName = "ui_pushbutton_test_79";
        private const string PanelShortName = "Panel_1";

        private const string ButtonFullPathLower =
            "assets/_project_ui_1/ui_first/prefabs_first/ui_pushbutton_test_79.prefab";
        private const string PanelFullPathLower =
            "assets/_project_ui_1/ui_first/prefabs_first/panel_1.prefab";

        public static void Load(ManualLogSource logger)
        {
            if (Bundle != null)
            {
                return;
            }

            string path = Path.Combine(Paths.PluginPath, "MITR", BundleFile);
            logger.LogInfo($"📦 LoadingAssets: loading {path}");

            if (!File.Exists(path))
            {
                logger.LogError($"❌ No bundle at {path}");
                return;
            }

            Bundle = AssetBundle.LoadFromFile(path);
            if (Bundle == null)
            {
                logger.LogError("❌ AssetBundle.LoadFromFile returned null");
                return;
            }

            Pushbutton79 = LoadGO(logger, ButtonShortName, ButtonFullPathLower);
            Panel1       = LoadGO(logger, PanelShortName,  PanelFullPathLower);
        }

        private static GameObject LoadGO(ManualLogSource logger, string shortName, string fullLowerPath)
        {
            GameObject go = Bundle.LoadAsset<GameObject>(shortName);
            if (go != null)
            {
                return go;
            }

            go = Bundle.LoadAsset<GameObject>(fullLowerPath);
            if (go != null)
            {
                return go;
            }

            logger.LogError($"❌ Could not find prefab {shortName} ({fullLowerPath})");
            return null;
        }
    }
}

