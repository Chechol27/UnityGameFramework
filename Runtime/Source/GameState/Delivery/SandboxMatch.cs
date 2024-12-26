using System.Collections.Generic;
using UnityGameFramework.Game.State;
using UnityGameFramework.Game.State.Core;
using UnityGameFramework.Player.Core;

namespace Game.State
{
    /// <summary>
    /// Base class for matches, corresponds to a single-player "Sandbox" state where the player can move freely
    /// </summary>
    public class SandboxMatch: GameMatch, IPlayerStateHandler<SandboxPlayerState>
    {
        public List<Controller> Players { get; private set; } = new List<Controller>();
        public List<SandboxPlayerState> PlayerStates { get; private set; } = new List<SandboxPlayerState>();

        public IPlayerStateHandler<SandboxPlayerState> AsPlayerStateHandler => this;

        protected override void InitializeMatch()
        {
            PlayerJoin(GameMode.playerController);
        }

        protected override void StartMatch()
        {
            base.StartMatch();
            SpawnPlayer(0, GameMode.defaultPawnPrefab);
        }

        private void Start()
        {
            StartMatch();
        }
    }
}