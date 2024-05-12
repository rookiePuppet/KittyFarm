using KittyFarm.Data;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.Playables;

namespace KittyFarm
{
    public class FirstPlayChecker : MonoBehaviour
    {
        [SerializeField] private PlayableDirector director;
        [SerializeField] private DialogContentDataSO openingDialogData;
        
        private void Start()
        {
            if (GameDataCenter.Instance.SettingsData.IsNewPlayer)
            {
                director.Play();
                director.stopped += _ =>
                {
                   InputReader.EnableInput();
                   UIManager.Instance.SetAllCanvasVisible(true);
                   GameManager.IsPlayerEnabled = true;
                   Destroy(director.gameObject);
                   
                   var dialogBoard = UIManager.Instance.ShowUI<DialogBoard>();
                   dialogBoard.ContentData = openingDialogData;
                   dialogBoard.BeginDialog();
                };

                InputReader.DisableInput();
                GameDataCenter.Instance.SettingsData.IsNewPlayer = false;
                UIManager.Instance.SetAllCanvasVisible(false);
                GameManager.IsPlayerEnabled = false;
                
                return;
            }
            
            Destroy(director.gameObject);
        }
    }
}