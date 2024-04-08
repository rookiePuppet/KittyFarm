using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    [CreateAssetMenu(fileName = "CropDatabase", menuName = "Kitty Farm/Crop Database")]
    public class CropDatabaseSO: ScriptableObject
    {
        [field: SerializeField] private List<CropDataSO> CropDataList { get; set; }
        
        public CropDataSO GetCropData(int id) => CropDataList.Find(x => x.Id == id);
    }
}