using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Game.Scripts.Enemies
{
    [CreateAssetMenu(fileName = "Enemy_config", menuName = "Configs/Enemy config")]
    public class EnemyConfig : ScriptableObject
    {
        public string Id;
        public AssetReference Prefab;
    }
}