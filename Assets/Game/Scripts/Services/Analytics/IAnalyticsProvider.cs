namespace Assets.Game.Scripts.Services.Analytics
{
    public interface IAnalyticsProvider
    {
        void LogEvent(string eventName, params AnalyticsParameter[] parameters);
    }
}