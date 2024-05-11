using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "ResourceDatabase", menuName = "Database/Resource Database")]
    public class ResourceDatabaseSO: DatabaseSO<ResourceDataSO>
    {
        public ResourceDataSO GetResourceData(int id) => dataList.Find(resourceData => resourceData.Id == id);
    }
}