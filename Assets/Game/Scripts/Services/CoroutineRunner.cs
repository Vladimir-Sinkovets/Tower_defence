using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Services
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        public Coroutine Run(IEnumerator coroutine) => base.StartCoroutine(coroutine);
        
        public void Stop(Coroutine coroutine) => base.StopCoroutine(coroutine);
        
        public void StopAll() => base.StopAllCoroutines();

        private void OnDestroy() => base.StopAllCoroutines();
    }

    public interface ICoroutineRunner
    {
        Coroutine Run(IEnumerator coroutine);
        void Stop(Coroutine coroutine);
        void StopAll();
    }
}