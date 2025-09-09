using UnityEngine;
using System.Collections;

namespace MITR_main
{
    public class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner _instance;

        public static void Run(IEnumerator routine)
        {
            if (_instance == null)
            {
                var go = new GameObject("MITR_CoroutineRunner");
                Object.DontDestroyOnLoad(go);
                _instance = go.AddComponent<CoroutineRunner>();
            }

            _instance.StartCoroutine(routine);
        }
    }
}
