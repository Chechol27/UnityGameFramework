using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityGameFramework.Game.State.Core;
using UnityGameFramework.Pawns.Core;
using UnityGameFramework.Player.Core;

namespace UnityGameFramework.Samples.Pong
{
    public class PongMatch : GameMatch, IGameStateHandler<PongGameState>, IPlayerStateHandler<PongPlayerState>
    {
        public PongGameState State { get; set; }
        public IGameStateHandler<PongGameState> AsGameStateHandler => this;
        public List<PongPlayerState> PlayerStates { get; private set; } = new List<PongPlayerState>();
        public IPlayerStateHandler<PongPlayerState> AsPlayerStateHandler => this;

        public UnityEvent OnGoalScored;

        private PongBall ball;

        public override void PlayerJoin(Controller playerPrefab)
        {
            base.PlayerJoin(playerPrefab);
            AsPlayerStateHandler.SpawnPlayerState(Players.Count - 1);
        }

        protected override void InitializeMatch()
        {
            AsGameStateHandler.InitializeGameState();
            PlayerJoin(GameMode.playerController);
            PlayerJoin(GameMode.playerController);
        }

        private void PlayBall()
        {
            if(State.ballPlayed) return;
            State.ballPlayed = true;
            Vector3 push = Vector3.right * (State.lastScoredPlayerId > 0 ? 1 : -1);
            push = Quaternion.AngleAxis(Random.Range(-45, 45), Vector3.up) * push;
            ball?.StartPush(push * 10);
        }

        public void ScoreGoal(int playerId)
        {
            if (playerId < 0 || playerId > 1) return;
            PongPlayerState state = PlayerStates[playerId];
            if (state.score + 1 >= ((PongGameMode)GameMode).winScore)
            {
                EndMatch();
            }
            OnGoalScored?.Invoke();
            state.score++;
        }

        public override Pawn SpawnPlayer(int playerId, Pawn pawnPrefab = null, bool ditchPreviousControlledPawn = true)
        {
            Pawn ret = base.SpawnPlayer(playerId, pawnPrefab, ditchPreviousControlledPawn);
            if (ret != null && ret is PongPawn pongPawn)
            {
                pongPawn.Input.SwitchCurrentActionMap(((PongGameMode)GameMode).localPerPlayerInputMappings[playerId]);
                pongPawn.OnMoved.AddListener(PlayBall);
                OnGoalScored.AddListener(pongPawn.Restart);
            }

            return ret;
        }

        protected override void StartMatch()
        {
            SpawnPlayer(0, GameMode.defaultPawnPrefab); 
            SpawnPlayer(1, GameMode.defaultPawnPrefab);
            ball = Instantiate(((PongGameMode)GameMode).ballPrefab);
            OnGoalScored.AddListener(ball.Restart);
            OnGoalScored.AddListener(() => State.ballPlayed = false);
        }

        protected override void EndMatch()
        {
            foreach (Controller player in Players)
            {
                player.Release();
            }
            Invoke(nameof(FinalizeMatch), 3);
        }

        protected override void FinalizeMatch()
        {
            
        }

        private void Start()
        {
            StartMatch();
        }
    }
}
