using UnityEngine;

namespace Assets.Game.Scripts.UI
{
    public class CanvasLookAtCamera : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void LateUpdate()
        {
            if (!_mainCamera) return;

            transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward,
                 _mainCamera.transform.rotation * Vector3.up);
        }
    }
}