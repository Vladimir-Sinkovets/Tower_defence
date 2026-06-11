namespace Assets.Game.Scripts.Services.Analytics
{
    public readonly struct AnalyticsParameter
    {
        public string Name { get; }
        public object Value { get; }

        public AnalyticsParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}