using UnityEngine;
using System.Collections;

namespace MITR_main
{
    public class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner _instance;

        public static void Run(IEnumerator coroutine)
        {
            if (_instance == null)
            {
                GameObject runner = new GameObject("MITR_CoroutineRunner");
                Object.DontDestroyOnLoad(runner);
                _instance = runner.AddComponent<CoroutineRunner>();
            }

            _instance.StartCoroutine(coroutine);
        }
    }
}
