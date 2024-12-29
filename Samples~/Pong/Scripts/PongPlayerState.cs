using UnityGameFramework.Game.State.Core;

namespace UnityGameFramework.Samples.Pong
{
    public class PongPlayerState : PlayerState
    {
        public int score;

        public int Score
        {
            get => score;
            set
            {
                score = value;
            }
        }
    }
}
