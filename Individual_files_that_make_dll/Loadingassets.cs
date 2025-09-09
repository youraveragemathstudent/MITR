// ✅ Something.cs
using System.IO;
using TMPro; // if you include TMP assets in your bundle
using UnityEngine;
using UnityEngine.UI;

namespace MITR_main
{
    public static class LoadingAssets
    {
        public static AssetBundle Assets;

        // Example: expose your prefabs
        public static GameObject PushbuttonTest79;
        public static GameObject Panel1;

        // Example: expose fonts, sprites, audio if you bundle them later
        public static TMP_FontAsset GameFont;
        public static Sprite UISprite;

        public static void LoadAssets(BepInEx.Logging.ManualLogSource logger)
        {
            // Path to your bundle file
            string path = Path.Combine(Paths.PluginPath, "MITR", "mitr_ui_new");
            Assets = AssetBundle.LoadFromFile(path);

            if (Assets == null)
            {
                Plugin.Logger.LogError($"❌ Could not load AssetBundle at {path}");
                return;
            }

            // Load prefabs by their exported names (make sure you set these names in Unity)
            PushbuttonTest79 = Assets.LoadAsset<GameObject>("ui_pushbutton_test_79");
            Panel1 = Assets.LoadAsset<GameObject>("Panel_1");

            // Optional extras
            GameFont = Assets.LoadAsset<TMP_FontAsset>("VCR_OSD_MONO_EXTENDED_TMP");
            UISprite = Assets.LoadAsset<Sprite>("UISprite");

            Plugin.Logger.LogInfo("✅ MITR assets loaded.");
        }
    }
}
