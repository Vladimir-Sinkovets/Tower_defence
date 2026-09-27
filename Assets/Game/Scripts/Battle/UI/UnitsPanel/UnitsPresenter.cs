using System;
using System.Linq;
using Assets.Game.Scripts.Battle.Configs;
using Assets.Game.Scripts.Battle.Ecs.Spawn;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.UI.UnitsPanel
{
    public class UnitsPresenter : IDisposable
    {
        private readonly IUnitsView _unitsView;
        private readonly UnitsConfig _unitsConfig;
        private readonly World _world;

        private Entity _eventEntity;

        private Stash<UnitChosenEvent> _eventStash;

        public UnitsPresenter(IUnitsView unitsView, UnitsConfig unitsConfig, World world)
        {
            _unitsView = unitsView;
            _unitsConfig = unitsConfig;
            _world = world;
        }

        public void Init()
        {
            var units = _unitsConfig.Units
                .Select(x =>
                    new UnitOption()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Icon = x.Icon,
                    });
            
            _unitsView.SetOptions(units);
            _unitsView.SetDefaultOption();

            _unitsView.OnOptionChosen += OnOptionChosenHandler;
            
            _eventEntity = _world.CreateEntity();
            _eventStash = _world.GetStash<UnitChosenEvent>();
        }

        private void OnOptionChosenHandler(string id)
        {
            var unit = _unitsConfig.Units.FirstOrDefault(x => x.Id == id);

            if (unit == null)
                return;
            
            _eventStash.Set(_eventEntity, new() { Config = unit });
        }

        public void Dispose() => _unitsView.OnOptionChosen -= OnOptionChosenHandler;
    }
}