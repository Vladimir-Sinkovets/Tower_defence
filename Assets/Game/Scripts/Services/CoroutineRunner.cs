using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Services
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        public Coroutine Run(IEnumerator coroutine) => StartCoroutine(coroutine);
        
        public void Stop(Coroutine coroutine) => StopCoroutine(coroutine);
        
        public void StopAll() => StopAllCoroutines();

        private void OnDestroy() => StopAllCoroutines();
    }

    public interface ICoroutineRunner
    {
        Coroutine Run(IEnumerator coroutine);
        void Stop(Coroutine coroutine);
        void StopAll();
    }
}