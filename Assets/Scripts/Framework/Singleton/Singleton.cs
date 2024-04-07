using System;

namespace Framework
{
    /// <summary>
    /// 普通单例基类
    /// </summary>
    /// <remarks>利用反射创建单例对象，可将派生类构造函数私有化，并且多线程安全</remarks>
    /// <typeparam name="T">派生类类型</typeparam>
    public abstract class Singleton<T> where T : class
    {
        private static T _instance;
        protected static readonly object lockObj = new();
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

        /// <summary>
        /// 初始化单例
        /// </summary>
        private static void SetUpSingleton()
        {
            lock (lockObj)
            {
                if (_instance is null)
                {
                    _instance = Activator.CreateInstance(typeof(T), true) as T;
                }
            }
        }
    }
}