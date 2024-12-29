using UnityGameFramework.Game.State.Core;

namespace UnityGameFramework.Samples.Pong
{
    public class PongGameState : GameState
    {
        public float currentMatchTimer;
        public bool ballPlayed;
        public int lastScoredPlayerId;
    }
}
