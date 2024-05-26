using KittyFarm.Data;
using KittyFarm.Time;
using KittyFarm.UI;
using UnityEngine;

namespace KittyFarm.InteractiveObject
{
    public class Resource : MonoBehaviour, ICollectable
    {
        [SerializeField] private ResourceDataSO data;
        [SerializeField] private GameObject[] stageObjects;

        public ResourceDataSO Data => data;
        private ResourceGrowthDetails growthDetails;
        public bool CanBeCollected { get; private set; }

        private const string GROWTH_DETAILS_KEY = "ResourceGrowthDetails";

        private void Awake()
        {
            var dataJson = PlayerPrefs.GetString($"{GROWTH_DETAILS_KEY}{transform.position}", "{}");
            growthDetails = JsonUtility.FromJson<ResourceGrowthDetails>(dataJson);
        }

        private void Start()
        {
            Refresh();
        }

        private void OnEnable()
        {
            GameManager.BeforeGameExit += OnBeforeGameExit;
            TimeManager.SecondPassed += Refresh;
        }

        private void OnDisable()
        {
            GameManager.BeforeGameExit -= OnBeforeGameExit;
            TimeManager.SecondPassed -= Refresh;
        }

        public void Collect()
        {
            var productDetails = data.ProductDetails;
            var addSucceed = GameDataCenter.Instance.PlayerInventory.AddItem(productDetails.ProductData, productDetails.Quantity);
            if (!addSucceed)
            {
                UIManager.Instance.ShowMessage("背包没有空位了");
                return;
            }
            
            UIManager.Instance.ShowUI<GetItemView>().Initialize(productDetails.ProductData, productDetails.Quantity);
            AudioManager.Instance.PlaySoundEffect(GameSoundEffect.HarvestCrop);

            growthDetails.LastCollectTime = TimeManager.CurrentTime;
            Refresh();
        }

        private void Refresh()
        {
            var sinceLastCollectTime = TimeManager.GetTimeSpanFrom(growthDetails.LastCollectTime);
            CanBeCollected = sinceLastCollectTime.TotalHours >= data.RegenerationTime;
            
            stageObjects[0].SetActive(!CanBeCollected);
            stageObjects[1].SetActive(CanBeCollected);
        }
        
        private void OnBeforeGameExit()
        {
            var json = JsonUtility.ToJson(growthDetails);
            PlayerPrefs.SetString($"{GROWTH_DETAILS_KEY}{transform.position}", json);
        }
    }
}