// ✅ Plugin.cs
using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

namespace MITR_main
{
    [BepInPlugin("youraveragemathstudent.ultrakill.mitr", "MITR_Main", "0.1.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;
        private AssetBundle _bundle;

        private void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo("[MITR_Main] Plugin loaded.");

            string assetBundlePath = System.IO.Path.Combine(Paths.PluginPath, "MITR", "ui_pushbutton_test_79");
            Logger.LogInfo($"📦 Trying to load AssetBundle from: {assetBundlePath}");

            if (System.IO.File.Exists(assetBundlePath))
            {
                _bundle = AssetBundle.LoadFromFile(assetBundlePath);
                Logger.LogInfo("✅ AssetBundle loaded successfully.");
            }
            else
            {
                Logger.LogError($"❌ AssetBundle not found at: {assetBundlePath}");
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Logger.LogInfo($"🧩 Scene loaded: {scene.name}");

            if (_bundle == null)
            {
                Logger.LogError("❌ AssetBundle was null in OnSceneLoaded.");
                return;
            }

            try
            {
                CoroutineRunner.Run(InjectCustomUI());
            }
            catch (System.Exception ex)
            {
                Logger.LogError($"❌ Exception in CoroutineRunner.Run: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private IEnumerator InjectCustomUI()
        {
            yield return new WaitForSeconds(1f);

            GameObject prefab = _bundle.LoadAsset<GameObject>("ui_pushbutton_test_79");
            if (prefab == null)
            {
                Logger.LogError("❌ Could not load prefab from AssetBundle.");
                yield break;
            }

            Transform parent = Resources.FindObjectsOfTypeAll<Transform>()
                .FirstOrDefault(t => t.name == "Difficulty Select (1)");

            if (parent == null)
            {
                Logger.LogError("❌ Could not find 'Difficulty Select (1)' to parent UI.");
                yield break;
            }

            GameObject uiObject = GameObject.Instantiate(prefab, parent);
            uiObject.name = "MITR_Button";

            RectTransform rect = uiObject.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.localScale = Vector3.one;
                rect.anchoredPosition = new Vector2(0, -100); // Adjust as needed
                rect.localPosition = Vector3.zero;
            }
            else
            {
                Logger.LogWarning("⚠️ Instantiated object had no RectTransform.");
            }

            Logger.LogInfo("✅ Instantiated and parented UI prefab.");
        }
    }
}
