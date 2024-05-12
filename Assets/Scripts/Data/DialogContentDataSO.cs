using System.Collections.Generic;
using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "DialogContentData", menuName = "Data/Dialog Content")]
    public class DialogContentDataSO: ScriptableObject
    {
        [SerializeField] private List<string> contents;

        public List<string> Content => contents;
    }
}