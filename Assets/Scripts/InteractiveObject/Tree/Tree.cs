using System.Threading.Tasks;
using KittyFarm.Service;
using KittyFarm.Time;
using UnityEngine;

namespace KittyFarm.InteractiveObject
{
    public class Tree : MonoBehaviour, IChopped
    {
        [SerializeField] private TreeDataSO treeData;
        [SerializeField] private GameObject[] StageObjects;
        [SerializeField] private ParticleSystem leavesParticle;

        private IItemService ItemService => ServiceCenter.Get<IItemService>();
        private GameObject CurrentStageObject => StageObjects[growthDetails.CurrentStageIndex];
        private TreeGrowthDetails growthDetails;
        private Damageable damageable;
        private Animator currentStageAnimator;
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
            for (var i = 0; i < StageObjects.Length; i++)
            {
                StageObjects[i].SetActive(i == growthDetails.CurrentStageIndex);
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
            AudioManager.Instance.PlaySoundEffect(GameSoundEffect.TreeShake);
            isShaking = true;
            leavesParticle.Play();
            await Task.Delay(800);
            isShaking = false;
            ItemService.SpawnItemAt(transform.position + (Vector3)treeData.RandomProductPosition,
                treeData.BranchDetails.ProductData);
        }

        private void OnDead()
        {
            for (var i = 0; i < treeData.WoodDetails.Quantity; i++)
            {
                ItemService.SpawnItemAt(transform.position + (Vector3)treeData.RandomProductPosition,
                    treeData.WoodDetails.ProductData);
            }

            growthDetails.ChangePlantedTime(treeData, 0);
            Refresh();

            damageable.Recover();
        }

        public async void OnChopped()
        {
            if (currentStageAnimator == null)
            {
                return;
            }
            
            var left = GameManager.Player.transform.position.x > transform.position.x;
            currentStageAnimator.SetTrigger(left ? ANIMATOR_SHOCK_LEFT : ANIMATOR_SHOCK_RIGHT);
            leavesParticle.Play();
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