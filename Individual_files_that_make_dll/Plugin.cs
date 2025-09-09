// Plugin.cs
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

        private void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo("[MITR_Main] Plugin loaded.");

            // Load all prefabs once at startup
            LoadingAssets.Load(Logger);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Logger.LogInfo($"🧩 Scene loaded: {scene.name}");

            if (LoadingAssets.Bundle == null)
            {
                Logger.LogError("❌ AssetBundle was null in OnSceneLoaded.");
                return;
            }

            CoroutineRunner.Run(InjectCustomUI());
        }

        private IEnumerator InjectCustomUI()
        {
            // wait a frame for UI to finish building
            yield return null;

            var prefab = LoadingAssets.Pushbutton79;
            if (prefab == null)
            {
                Logger.LogError("❌ Pushbutton79 prefab is null. Check bundle export.");
                yield break;
            }

            // Find intended parent; fallback to any active canvas
            Transform parent = Resources.FindObjectsOfTypeAll<Transform>()
                .FirstOrDefault(t => t.name == "Difficulty Select (1)");

            if (parent == null)
            {
                var canvas = Resources.FindObjectsOfTypeAll<Canvas>()
                    .FirstOrDefault(c => c.isActiveAndEnabled);
                if (canvas != null)
                {
                    parent = canvas.transform;
                }
                else
                {
                    Logger.LogError("❌ No Canvas found.");
                    yield break;
                }

                Logger.LogWarning("⚠️ 'Difficulty Select (1)' not found. Using fallback canvas.");
            }

            var go = Instantiate(prefab);
            go.name = "MITR_Button";
            go.transform.SetParent(parent, worldPositionStays: false);
            go.SetActive(true);

            var rt = go.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchorMin = new Vector2(0f, 0f);
                rt.anchorMax = new Vector2(0f, 0f);
                rt.pivot = new Vector2(0f, 0f);
                rt.anchoredPosition = new Vector2(40f, 40f);
                rt.sizeDelta = new Vector2(160f, 30f);
                rt.localScale = Vector3.one;
            }

            var img = go.GetComponent<Image>();
            if (img != null && img.sprite == null)
            {
                var builtin = Resources.GetBuiltinResource<Sprite>("UI/Skin/UISprite.psd");
                img.sprite = builtin;
                img.type = Image.Type.Sliced;
            }

            Logger.LogInfo("✅ MITR button instantiated.");
        }
    }
}

