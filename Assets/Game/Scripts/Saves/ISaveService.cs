namespace Assets.Game.Scripts.Saves
{
    public interface ISaveService
    {
        void Save(SaveData saveData);
        SaveData GetSaveData();
    }
}