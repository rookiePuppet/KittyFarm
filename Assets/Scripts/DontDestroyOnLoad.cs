using UnityEngine;
using UnityEngine.SceneManagement;

namespace KittyFarm
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private static DontDestroyOnLoad instance;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}