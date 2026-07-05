using System.Linq;
using Firebase.Analytics;

namespace Assets.Game.Scripts.Services.Analytics
{
    public class FirebaseAnalyticsProvider : IAnalyticsProvider
    {
        public void LogEvent(string eventName, params AnalyticsParameter[] parameters)
        {
            FirebaseAnalytics.LogEvent(
                eventName,
                parameters.Select(CreateParameter).ToArray());
        }

        private Parameter CreateParameter(AnalyticsParameter parameter)
        {
            return parameter.Value switch
            {
                int value => new Parameter(parameter.Name, value),
                long value => new Parameter(parameter.Name, value),
                float value => new Parameter(parameter.Name, value),
                double value => new Parameter(parameter.Name, value),
                string value => new Parameter(parameter.Name, value),
                _ => new Parameter(parameter.Name, parameter.Value?.ToString() ?? string.Empty)
            };
        }
    }

}