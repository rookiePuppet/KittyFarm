using System.Collections.Generic;
using UnityEngine;

namespace KittyFarm.Data
{
    public abstract class DatabaseSO<T> : ScriptableObject
    {
        [SerializeField] protected List<T> dataList;
    }
}