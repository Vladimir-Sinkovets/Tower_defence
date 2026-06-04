using System;

namespace Assets.Game.Scripts.Saves
{
    public interface ISaveService
    {
        event Action OnSaved;
        void Save(SaveData saveData);
        SaveData GetSaveData();
    }
}