using System.Threading.Tasks;
using KittyFarm.Service;
using KittyFarm.Time;
using UnityEngine;

namespace KittyFarm.InteractiveObject
{
    public class FruitTree : MonoBehaviour, IChopped
    {
        [SerializeField] protected FruitTreeDataSO treeData;
        [SerializeField] protected GameObject[] stageObjects;
        [SerializeField] private int fruitStageIndex = 2;

        private IItemService ItemService => ServiceCenter.Get<IItemService>();
        private GameObject CurrentStageObject => stageObjects[growthDetails.CurrentStageIndex];
        private Animator currentStageAnimator;

        private TreeGrowthDetails growthDetails;
        private Damageable damageable;
        private bool isShaking;

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
            GameManager.BeforeGameExit += OnBeforeGameExit;
            TimeManager.SecondPassed += Refresh;
            damageable.Dead += OnDead;
        }

        private void OnDisable()
        {
            GameManager.BeforeGameExit -= OnBeforeGameExit;
            TimeManager.SecondPassed -= Refresh;
            damageable.Dead -= OnDead;
        }

        private void Refresh()
        {
            growthDetails.CurrentStageIndex = growthDetails.CalculateCurrentStage(treeData);
            for (var i = 0; i < stageObjects.Length; i++)
            {
                stageObjects[i].SetActive(i == growthDetails.CurrentStageIndex);
            }

            currentStageAnimator = CurrentStageObject.GetComponent<Animator>();
        }

        public async void OnOtherClicked()
        {
            if (isShaking || currentStageAnimator == null)
            {
                return;
            }
    
            currentStageAnimator.SetTrigger(ANIMATOR_SHAKE);
            isShaking = true;
            AudioManager.Instance.PlaySoundEffect(GameSoundEffect.TreeShake);
            await Task.Delay(800);
            isShaking = false;

            // 掉落树枝
            ItemService.SpawnItemAt(transform.position + (Vector3)treeData.RandomProductPosition,
                treeData.BranchDetails.ProductData);
            // 树上有水果，则生成水果
            if (growthDetails.CurrentStageIndex == fruitStageIndex)
            {
                SpawnFruit();
                growthDetails.ChangePlantedTime(treeData, fruitStageIndex - 1);
                Refresh();
            }
        }

        private void OnDead()
        {
            for (var i = 0; i < treeData.WoodDetails.Quantity; i++)
            {
                ItemService.SpawnItemAt(transform.position + (Vector3)treeData.RandomProductPosition,
                    treeData.WoodDetails.ProductData);
            }

            // 树上有水果，则生成水果
            if (growthDetails.CurrentStageIndex == fruitStageIndex)
            {
                SpawnFruit();
            }

            growthDetails.ChangePlantedTime(treeData, 0);
            Refresh();
            damageable.Recover();
        }

        private void SpawnFruit()
        {
            foreach (var position in treeData.FixedProductPositions)
            {
                var worldPosition = transform.position + (Vector3)position;
                ItemService.SpawnItemAt(worldPosition, treeData.FruitDetails.ProductData);
            }
        }

        public async void OnChopped()
        {
            if (currentStageAnimator != null)
            {
                var left = GameManager.Player.transform.position.x > transform.position.x;
                currentStageAnimator.SetTrigger(left ? ANIMATOR_SHOCK_LEFT : ANIMATOR_SHOCK_RIGHT);
            }

            await Task.Delay(100);
            damageable.TakeDamage(34);
        }
        
        public bool CanBeChopped => growthDetails.CurrentStageIndex > 0;

        private void OnBeforeGameExit()
        {
            var json = JsonUtility.ToJson(growthDetails);
            PlayerPrefs.SetString($"{GROWTH_DETAILS_KEY}{transform.position}", json);
        }
    }
}