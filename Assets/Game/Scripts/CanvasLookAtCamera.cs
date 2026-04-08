using UnityEngine;

namespace Assets.Game.Scripts
{
    public class CanvasLookAtCamera : MonoBehaviour
    {
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void LateUpdate()
        {
            if (mainCamera == null) return;

            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                 mainCamera.transform.rotation * Vector3.up);
        }
    }
}