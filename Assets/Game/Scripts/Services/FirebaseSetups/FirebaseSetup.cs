using Firebase;
using Firebase.Extensions;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Services.FirebaseSetups
{
    public class FirebaseSetup : IInitializable
    {
        public void Initialize()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                var dependencyStatus = task.Result;
                
                if (dependencyStatus == DependencyStatus.Available)
                {
                    Debug.Log("Firebase is ready");
                }
                else
                {
                    Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                }
            });
        }
    }
}