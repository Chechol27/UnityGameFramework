using System.Collections.Generic;
using Services.Core;
using UnityEngine;

namespace Game.Delivery
{
    /// <summary>
    /// Game information holder for all things related to the game logic
    /// Helps with locating game subsystems (services) that need static access
    /// </summary>
    public class Game : MonoBehaviour, IServiceLocator
    {
        public static Game Instance { get; private set; }
        
        public List<INativeService> ServiceCollection { get; } = new List<INativeService>();
        public List<IManagedService> ManagedServiceCollection { get; } = new List<IManagedService>();
        
        public TService GetNativeSubsystem<TService>() where TService : INativeService, new()
        {
            return ((IServiceLocator)this).GetNativeService<TService>();
        }

        public TManagedService GetManagedSubSystem<TManagedService>() where TManagedService : Component, IManagedService
        {
            return ((IServiceLocator)this).GetManagedService<TManagedService>();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void InitializeGameInstance()
        {
            if (Instance != null)
            {
                Debug.LogError("WTF?, how did a previous game instance leak? ._.");
            }
            GameObject gameObjectGo = new GameObject("GameInstance");
            DontDestroyOnLoad(gameObjectGo);
            Instance = gameObjectGo.AddComponent<Game>();
            Debug.Log("Game Instance Initialized");
        }
    }
}
