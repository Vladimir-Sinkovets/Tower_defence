using Assets.Game.Scripts.Arena.Player;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Game.Scripts.Arena.Services.EnemyDroppers
{
    public abstract class Drop : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] protected PhotonView PhotonView;
        
        [Header("Appearance Animation")]
        [SerializeField] private float _flyDistance = 1f;
        [SerializeField] private float _flyDuration = 0.6f;
        [SerializeField] private float _jumpHeight = 1f;
        [SerializeField] private Ease _flyEase = Ease.OutCubic;
        [SerializeField] private float _speed = 0.5f;

        private bool _taken;
        
        protected void PlayAppearanceAnimation()
        {
            var randomCircle = Random.insideUnitCircle.normalized;
            var direction = new Vector3(randomCircle.x, 0f, randomCircle.y);
            
            var startPosition = transform.position;
            var targetPosition = startPosition + direction * _flyDistance;

            transform.DOJump(targetPosition, _jumpHeight, 1, _flyDuration)
                .SetEase(_flyEase)
                .SetLink(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<ArenaPlayer>();

            if (player == null)
                return;

            var playerView = player.GetComponent<PhotonView>();

            if (playerView == null)
                return;

            if (!playerView.IsMine)
                return;

            PhotonView.RPC(
                nameof(RequestTake),
                RpcTarget.MasterClient,
                playerView.ViewID);

            TakeAsync(playerView.ViewID).Forget();
        }

        [PunRPC]
        public void RequestTake(int playerViewId)
        {
            if (!PhotonNetwork.IsMasterClient)
                return;

            if (_taken)
                return;

            var playerView = PhotonView.Find(playerViewId);

            if (playerView == null)
                return;

            var player = playerView.GetComponent<ArenaPlayer>();

            if (player == null)
                return;

            _taken = true;
        }

        private async UniTaskVoid TakeAsync(int playerViewId)
        {
            var playerView = PhotonView.Find(playerViewId);
            
            if (playerView == null)
                return;
            
            if (!playerView.IsMine)
                return;

            var player = playerView.GetComponent<ArenaPlayer>();
            
            if (player == null)
                return;

            _collider.enabled = false;

            await PlayTakeAnimation(player.transform);

            ApplyBonus();
             
            PhotonView.RPC(nameof(RequestDestroy), RpcTarget.MasterClient);
        }

        [PunRPC]
        public void RequestDestroy()
        {
            transform.DOKill(true); 
            
            if (!PhotonNetwork.IsMasterClient)
                return;
            
            PhotonNetwork.Destroy(gameObject);
        }
        
        protected abstract void ApplyBonus();
        
        private async UniTask PlayTakeAnimation(Transform taker)
        {
            transform.DOKill();

            if (taker == null)
                return;

            while (taker != null &&
                   Vector3.SqrMagnitude(transform.position - taker.position) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    taker.position,
                    _speed * Time.deltaTime);

                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            if (taker != null)
                transform.position = taker.position;
        }
    }
}