using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "CropDatabase", menuName = "Database/Crop Database")]
    public class CropDatabaseSO : DatabaseSO<CropDataSO>
    {
        public CropDataSO GetCropData(int id) => dataList.Find(cropData => cropData.Id == id);
    }
}