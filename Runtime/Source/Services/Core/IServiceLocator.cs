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

        TManagedService GetManagedService<TManagedService>(bool createIfNotPresent = true, int id = 0) where TManagedService : Component, IManagedService
        {
            bool isOrdered = typeof(IIndexedManagedService).IsAssignableFrom(typeof(TManagedService));
            try
            {
                if (isOrdered)
                {
                    TManagedService ret = (TManagedService)ManagedServiceCollection.First(service => service is TManagedService && ((IIndexedManagedService)service).Order == id);
                    return ret;
                }

                return (TManagedService)ManagedServiceCollection.First(service => service is TManagedService);
            }
            catch (Exception)
            {
                TManagedService t = null;
                if (isOrdered)
                {
                    try
                    {
                        t = Object.FindObjectsByType<TManagedService>(FindObjectsSortMode.None)
                            .First(service => ((IIndexedManagedService)service).Order == id);
                    }
                    catch
                    {
                        // ignored, already null
                    }
                }
                else
                {
                    t = Object.FindFirstObjectByType<TManagedService>();
                }

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
                if(isOrdered)
                    ((IIndexedManagedService)t).Order = id;
                ManagedServiceCollection.Add(t);
                return t;
            }
        }
    }
}
