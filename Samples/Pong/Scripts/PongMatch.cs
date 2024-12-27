using System.Collections.Generic;
using UnityEngine.Events;
using UnityGameFramework.Game.State.Core;
using UnityGameFramework.Pawns.Core;

namespace UnityGameFramework.Samples.Pong
{
    public class PongMatch : GameMatch, IGameStateHandler<PongGameState>, IPlayerStateHandler<PongPlayerState>
    {
        public PongGameState State { get; set; }
        public IGameStateHandler<PongGameState> AsGameStateHandler => this;
        public List<PongPlayerState> PlayerStates { get; }
        public IPlayerStateHandler<PongPlayerState> AsPlayerStateHandler => this;

        public UnityEvent OnGoalScored;
        
        protected override void InitializeMatch()
        {
            PlayerJoin(GameMode.playerController);
            PlayerJoin(GameMode.playerController);
        }

        public override Pawn SpawnPlayer(int playerId, Pawn pawnPrefab = null, bool ditchPreviousControlledPawn = true)
        {
            Pawn ret = base.SpawnPlayer(playerId, pawnPrefab, ditchPreviousControlledPawn);
            if (ret != null)
            {
                ret.Input.SwitchCurrentActionMap(((PongGameMode)GameMode).localPerPlayerInputMappings[playerId]);
                OnGoalScored.AddListener(ret.ResetState);
            }

            return ret;
        }

        protected override void StartMatch()
        {
            SpawnPlayer(0, GameMode.defaultPawnPrefab); 
            SpawnPlayer(1, GameMode.defaultPawnPrefab);
        }

        private void Start()
        {
            StartMatch();
        }
    }
}
