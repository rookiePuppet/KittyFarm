using UnityEngine;

namespace KittyFarm.Data
{
    public abstract class ScriptableData: ScriptableObject
    {
        public abstract string DataFileName { get; }
        public abstract void LoadData(string fileName);
        public abstract void SaveData();
    }
}