using System.Collections.Generic;

namespace KittyFarm.Service
{
    public static class ServiceCenter
    {
        private static readonly Dictionary<string, IService> services = new();

        public static void Register<T>(T service) where T : IService
        {
            services[typeof(T).Name] = service;
        }

        public static T Get<T>() where T : IService
        {
            if (services.TryGetValue(typeof(T).Name, out var service))
            {
                return (T)service;
            }
        
            return default(T);
        }
    }
    
    public interface IService
    {
    }
}