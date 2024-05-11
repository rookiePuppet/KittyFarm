using UnityEngine;
using UnityEngine.Pool;

namespace KittyFarm.UI
{
    public class GameMessagePool : MonoBehaviour
    {
        private GameObject messagePrefab;

        private IObjectPool<GameMessage> messages;

        public void Initialize(GameObject prefab, Transform parent)
        {
            messagePrefab = prefab;

            messages = new ObjectPool<GameMessage>(CreateMessage, OnGetMessage, OnReleaseMessage);
        }

        public GameMessage Get() => messages.Get();

        public void Clear() => messages.Clear();

        private GameMessage CreateMessage()
        {
            var messageObj = Instantiate(messagePrefab, transform);
            var message = messageObj.GetComponent<GameMessage>();
            message.Pool = messages;
            return message;
        }

        private void OnGetMessage(GameMessage message)
        {
            message.gameObject.SetActive(true);
        }

        private void OnReleaseMessage(GameMessage message)
        {
            message.gameObject.SetActive(false);
        }
    }
}