using UnityEngine;
using UnityGameFramework.Game.State.Core;

namespace UnityGameFramework.Samples.Pong
{

    [CreateAssetMenu(menuName = "Gameplay/Pong/GameModeData")]
    public class PongGameMode : GameMode
    {
        public PongBall ballPrefab;
        public int winScore = 8;
        public float timeLimitSeconds = 300;
        public string[] localPerPlayerInputMappings;
    }
}
