using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Services.Core
{
    public interface IServiceLocator
    {
        List<INativeService> ServiceCollection { get; }
        List<IManagedService> ManagedServiceCollection { get; }

        TService GetNativeService<TService>() where TService: INativeService, new()
        {
            try
            {
                return (TService)ServiceCollection.First(service => service is TService);
            }
            catch (InvalidOperationException)
            {
                TService t = new TService();
                t.InitializeService();
                ServiceCollection.Add(t);
                return t;
            }
        }

        TManagedService GetManagedService<TManagedService>(bool createIfNotPresent = true) where TManagedService : Component, IManagedService
        {
            try
            {
                return (TManagedService)ManagedServiceCollection.First(service => service is TManagedService);
            }
            catch (InvalidOperationException)
            {
                TManagedService t = Object.FindFirstObjectByType<TManagedService>();
                if (t != null)
                {
                    ManagedServiceCollection.Add(t);
                    return t;
                }

                if (!createIfNotPresent)
                {
                    return null;
                }
                GameObject managedServiceGo = new GameObject($"{typeof(TManagedService).Name}");
                Object.DontDestroyOnLoad(managedServiceGo);
                t = managedServiceGo.AddComponent<TManagedService>();
                ManagedServiceCollection.Add(t);
                return t;
            }
        }
    }
}
