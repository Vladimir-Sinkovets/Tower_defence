using System;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Battle.UI.UnitsPanel
{
    public interface IUnitsView
    {
        event Action<string> OnOptionChosen;
        void SetOptions(IEnumerable<UnitOption> units);
        void SetDefaultOption();
    }
}