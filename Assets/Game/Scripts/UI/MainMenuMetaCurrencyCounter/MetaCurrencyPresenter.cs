using Assets.Game.Scripts.Services;

namespace Assets.Game.Scripts.UI
{
    public class MetaCurrencyPresenter
    {
        public MetaCurrencyPresenter(IMetaCurrencyView view, MetaCurrencyService metaCurrencyService)
        {
            view.SetText(metaCurrencyService.Total.ToString());
        }
    }
}