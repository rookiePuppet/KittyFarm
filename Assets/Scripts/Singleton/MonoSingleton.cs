using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 继承MonoBehaviour的单例基类
    /// </summary>
    /// <remarks>无需手动挂载脚本，场景切换时不会被移除</remarks>
    /// <typeparam name="T">派生类类型</typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance is null)
                {
                    SetUpSingleton();
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            RemoveDuplicates();
        }

        /// <summary>
        /// 初始化单例
        /// <remarks>寻找场景中是否已有对象挂在该单例脚本，若有则不会继续创建单例，若无则自动创建游戏对象并挂载单例脚本。</remarks>
        /// </summary>
        private static void SetUpSingleton()
        {
            _instance = FindObjectOfType<T>();
            if (_instance is null)
            {
                var gameObject = new GameObject(typeof(T).Name);
                gameObject.AddComponent<T>();
            }
        }

        /// <summary>
        /// 移除重复创建的单例
        /// </summary>
        private void RemoveDuplicates()
        {
            // 加入 _instance.gameObject == gameObject 判断的原因：
            // 若在Awake之前已经使用过该单例，_instance不为空并且是当前的GameObject，说明不是重复创建的，不需要移除
            if (_instance == null || _instance.gameObject == gameObject)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                Debug.LogWarning(
                    $"{gameObject.name} has been destroyed because singleton {typeof(T).Name} already exists.");
            }
        }
    }
}