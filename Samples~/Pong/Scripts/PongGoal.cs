using UnityEngine;
using UnityGameFramework.Game;

namespace UnityGameFramework.Samples.Pong{
    public class PongGoal : MonoBehaviour
    {
        [SerializeField] private int playerId;
        public void OnTriggerEnter(Collider other)
        {
            PongBall otherBall = other.GetComponent<PongBall>();
            if (otherBall == null) return;
            GameInstance.Main.GetManagedSubSystem<PongMatch>().ScoreGoal(playerId);
        }
    }
}