using System;
using System.Threading.Tasks;
using KittyFarm.Service;
using KittyFarm.Time;
using UnityEngine;

namespace KittyFarm.InteractiveObject
{
    public class FruitTree : MonoBehaviour, IChopped
    {
        [SerializeField] protected FruitTreeDataSO treeData;
        [SerializeField] protected GameObject[] StageObjects;
        [SerializeField] private int fruitStageIndex = 2;

        private IItemService ItemService => ServiceCenter.Get<IItemService>();
        private GameObject CurrentStageObject => StageObjects[growthDetails.CurrentStageIndex];
        private Animator currentStageAnimator;

        private TreeGrowthDetails growthDetails;
        private Damageable damageable;

        private const string GROWTH_DETAILS_KEY = "TreeGrowthDetails";
        
        private readonly int ANIMATOR_SHAKE = Animator.StringToHash("Shake");
        private readonly int ANIMATOR_SHOCK_LEFT = Animator.StringToHash("ShockLeft");
        private readonly int ANIMATOR_SHOCK_RIGHT = Animator.StringToHash("ShockRight");
        
        private void Awake()
        {
            damageable = GetComponent<Damageable>();
            
            var dataJson = PlayerPrefs.GetString($"{GROWTH_DETAILS_KEY}{transform.position}", "{}");
            growthDetails = JsonUtility.FromJson<TreeGrowthDetails>(dataJson);
        }

        private void Start()
        {
            Refresh();
        }

        private void OnEnable()
        {
            GameManager.BeforeGameExit += () =>
            {
                var json = JsonUtility.ToJson(growthDetails);
                PlayerPrefs.SetString($"{GROWTH_DETAILS_KEY}{transform.position}", json);
            };
            TimeManager.SecondPassed += Refresh;
            damageable.Dead += OnDead;
        }

        private void Refresh()
        {
            growthDetails.CurrentStageIndex = growthDetails.CalculateCurrentStage(treeData);
            for (var i = 0; i < StageObjects.Length; i++)
            {
                StageObjects[i].SetActive(i == growthDetails.CurrentStageIndex);
            }
            
            currentStageAnimator = CurrentStageObject.GetComponent<Animator>();
        }
        
        public async void OnOtherClicked()
        {
            if (currentStageAnimator != null)
            {
                currentStageAnimator.SetTrigger(ANIMATOR_SHAKE);
            }
            AudioManager.Instance.PlaySoundEffect(GameSoundEffect.TreeShake);
            await Task.Delay(800);
            
            // 掉落树枝
            ItemService.SpawnItemAt(transform, treeData.RandomProductPosition, treeData.BranchDetails.ProductData);
            // 树上有水果，则生成水果
            if (growthDetails.CurrentStageIndex == fruitStageIndex)
            {
                SpawnFruit();
            }
        }
        
        private void OnDead()
        {
            ItemService.SpawnItemAt(transform,treeData.RandomProductPosition, treeData.WoodDetails.ProductData);
            
            growthDetails.ChangePlantedTime(treeData, 0);
            Refresh();
            
            damageable.Recover();
        }

        private void SpawnFruit()
        {
            foreach (var position in treeData.FixedProductPositions)
            {
                ItemService.SpawnItemAt(transform, position, treeData.FruitDetails.ProductData);
                print($"fruit spawned at {position}");
            }
            
            growthDetails.ChangePlantedTime(treeData, fruitStageIndex - 1);
            Refresh();
        }

        public async void OnChopped()
        {
            if (currentStageAnimator != null)
            {
                var left = GameManager.Player.transform.position.x > transform.position.x;
                currentStageAnimator.SetTrigger(left? ANIMATOR_SHOCK_LEFT : ANIMATOR_SHOCK_RIGHT);
            }
            await Task.Delay(100);
            damageable.TakeDamage(34);
            
            // 树上有水果，则生成水果
            if (growthDetails.CurrentStageIndex == fruitStageIndex)
            {
                print($"{growthDetails.CurrentStageIndex} tree has fruit");
                SpawnFruit();
            }
        }
    }
}