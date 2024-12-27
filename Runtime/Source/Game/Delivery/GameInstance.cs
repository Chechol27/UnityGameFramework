using System;
using System.Collections.Generic;
using Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityGameFramework.Game
{
    /// <summary>
    /// Game information holder for all things related to the game logic
    /// Helps with locating game subsystems (services) that need static access
    /// </summary>
    public class GameInstance : MonoBehaviour, IServiceLocator
    {
        public static GameInstance Main { get; private set; }
        
        public List<INativeService> ServiceCollection { get; } = new List<INativeService>();
        public List<IManagedService> ManagedServiceCollection { get; } = new List<IManagedService>();
        
        public TService GetNativeSubsystem<TService>() where TService : INativeService, new()
        {
            return ((IServiceLocator)this).GetNativeService<TService>();
        }

        public TManagedService GetManagedSubSystem<TManagedService>(bool createIfNotPreset = true, int id = 0) where TManagedService : Component, IManagedService
        {
            return ((IServiceLocator)this).GetManagedService<TManagedService>(createIfNotPreset, id);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void InitializeGameInstance()
        {
            if (Main != null)
            {
                Debug.LogError("WTF?, how did a previous game instance leak? ._.");
            }
            GameObject gameObjectGo = new GameObject("GameInstance");
            DontDestroyOnLoad(gameObjectGo);
            Main = gameObjectGo.AddComponent<GameInstance>();
            Debug.Log("Game Instance Initialized");
        }
    }
}
