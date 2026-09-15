using Assets.Game.Scripts.Services.Net;

namespace Assets.Game.Scripts.Arena.UI.GameInfo
{
    public class GameInfoPresenter
    {
        private readonly IGameInfoView _gameInfoView;
        private readonly INetworkService _networkService;

        public GameInfoPresenter(IGameInfoView gameInfoView, INetworkService networkService)
        {
            _gameInfoView = gameInfoView;
            _networkService = networkService;
        }

        public void Init() => _gameInfoView.SetId(_networkService.RoomId);
    }
}