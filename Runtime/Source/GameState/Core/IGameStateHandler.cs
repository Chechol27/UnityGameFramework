using UnityEngine;

namespace UnityGameFramework.Game.State.Core
{
    public interface IGameStateHandler<TGameState> where TGameState : GameState 
    {
        TGameState State { get; set; }
        IGameStateHandler<TGameState> AsGameStateHandler { get; }
        
        void PurgePreviousStateIfPresent()
        {
            if (State != null)
            {
                Object.Destroy(State);
                Debug.LogError("Game State already present in game, beware of memory leaks and early game finalization/late initialization");
            }
        }

        void InitializeGameState()
        {
            GameObject gameStateGo = new GameObject("Game State");
            State = gameStateGo.AddComponent<TGameState>();
        }
    }
}