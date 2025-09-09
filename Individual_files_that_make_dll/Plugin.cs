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

        private void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo("[MITR_Main] Plugin loaded.");

            // Call your renamed static loader
            LoadingAssets.Load(Logger);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Logger.LogInfo($"🧩 Scene loaded: {scene.name}");

            if (LoadingAssets.Bundle == null)
            {
                Logger.LogError("❌ LoadingAssets.Bundle is null in OnSceneLoaded.");
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
            // Let the scene finish building its UI this frame
            yield return null;

            // Use prefab exposed by LoadingAssets
            var prefab = LoadingAssets.Pushbutton79;
            if (prefab == null)
            {
                Logger.LogError("❌ LoadingAssets.Pushbutton79 is null (check bundle/asset names).");
                yield break;
            }

            // Find intended parent; fall back to any active Canvas
            Transform parent = Resources.FindObjectsOfTypeAll<Transform>()
                .FirstOrDefault(t => t.name == "Difficulty Select (1)");

            if (parent == null)
            {
                var canvas = Resources.FindObjectsOfTypeAll<Canvas>()
                    .FirstOrDefault(c => c.isActiveAndEnabled);
                if (canvas != null) parent = canvas.transform;
                else { Logger.LogError("❌ No Canvas found to parent under."); yield break; }

                Logger.LogWarning("⚠️ 'Difficulty Select (1)' not found; parenting under first active Canvas instead.");
            }

            // Instantiate + parent in UI space
            var go = Instantiate(prefab);
            go.name = "MITR_Button";
            go.transform.SetParent(parent, worldPositionStays: false);
            go.SetActive(true);

            // Make it definitely visible: bottom-left, fixed size
            var rt = go.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchorMin = new Vector2(0f, 0f);
                rt.anchorMax = new Vector2(0f, 0f);
                rt.pivot = new Vector2(0f, 0f);
                rt.anchoredPosition = new Vector2(40f, 40f);   // 40 px from left/bottom
                rt.sizeDelta = new Vector2(160f, 30f);  // default prefab size
                rt.localScale = Vector3.one;
            }
            else
            {
                Logger.LogWarning("⚠️ Instantiated object had no RectTransform.");
            }

            // Ensure something renders even if your prefab had no sprite
            var img = go.GetComponent<Image>();
            if (img != null)
            {
                if (img.sprite == null)
                {
                    var builtin = Resources.GetBuiltinResource<Sprite>("UI/Skin/UISprite.psd");
                    img.sprite = builtin;
                    img.type = Image.Type.Sliced;
                }
                var c = img.color; img.color = new Color(c.r, c.g, c.b, 1f);
            }

            go.transform.SetAsLastSibling();

            Logger.LogInfo("✅ Instantiated and parented UI prefab.");
        }
    }
}


