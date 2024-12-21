using System.Collections.Generic;
using UnityEngine;

namespace Game.State.Core
{
    /// <summary>
    /// Per-level Game "Initializer" reads data and spawns the necessary initial actors into the game
    /// also manages match lifecycle 
    /// </summary>
    public class GameMatch<TGameMode, TGameState, TPlayerState> : MonoBehaviour, IManagedService
        where TGameMode : GameMode
        where TGameState : GameState
        where TPlayerState : PlayerState

    {
        protected virtual TGameMode GameMode { get; set; }
        protected TGameState State;
        protected readonly List<TPlayerState> PlayerStates = new List<TPlayerState>();
        #warning TODO Implement Player controllers and pawns
        protected readonly List<GameObject> PlayerControllers = new List<GameObject>();

        void PurgePreviousStateIfPresent()
        {
            if (State != null)
            {
                Destroy(State);
                Debug.LogError("Game State already present in game, beware of memory leaks and early game finalization/late initialization");
            }
            
            if (PlayerStates.Count > 0)
            {
                foreach (TPlayerState state in PlayerStates)
                {
                    Destroy(state);
                }
                PlayerStates.Clear();                
                Debug.LogError("Player States already present in game, beware of memory leaks and early game finalization/late game initialization");
            }
        }
        
        protected void InitializeMatch(int playerCount)
        {
            PurgePreviousStateIfPresent();
            GameObject gameStateGo = new GameObject("Game State");
            State = gameStateGo.AddComponent<TGameState>();
            GameObject playerStateGo = new GameObject("Player 0 State");
            PlayerStates.Add(playerStateGo.AddComponent<TPlayerState>());
            for (int i = 0; i < playerCount; i++)
            {
                GameObject playerGo = Instantiate(GameMode.playerController);
                playerGo.name = $"Player_{i}";
                PlayerControllers.Add(playerGo);
            }
        }

        protected virtual void StartMatch()
        {
        }

        protected virtual void EndMatch()
        {
        }
    }
}
