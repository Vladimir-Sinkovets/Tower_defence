using UnityEngine;

namespace Assets.Game.Scripts.Buildings
{
    public class Building : MonoBehaviour
    {
        private BuildingConfig _config;

        public float RadiusOfOccupiedSpace => _config.RadiusOfOccupiedSpace;

        public void Init(BuildingConfig config)
        {
            _config = config;
        }

        private void OnDrawGizmos()
        {
            if (_config != null)
                Gizmos.DrawWireSphere(transform.position, RadiusOfOccupiedSpace);
        }
    }
}