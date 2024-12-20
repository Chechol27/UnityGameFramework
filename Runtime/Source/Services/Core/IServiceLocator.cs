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

        TManagedService GetManagedService<TManagedService>() where TManagedService : Component, IManagedService
        {
            try
            {
                return (TManagedService)ManagedServiceCollection.First(service => service is TManagedService);
            }
            catch (InvalidOperationException)
            {
                GameObject managedServiceGo = new GameObject($"{typeof(TManagedService).Name}");
                Object.DontDestroyOnLoad(managedServiceGo);
                TManagedService t = managedServiceGo.AddComponent<TManagedService>();
                ManagedServiceCollection.Add(t);
                return t;
            }
        }
    }
}
