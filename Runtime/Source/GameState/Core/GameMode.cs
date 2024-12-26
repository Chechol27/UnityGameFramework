using UnityEngine;
using UnityGameFramework.Pawns.Core;
using UnityGameFramework.Player.Core;

namespace UnityGameFramework.Game.State.Core
{
    /// <summary>
    /// Base class for game modes, defines all data needed for the game rules to be executed and enforced correctly
    /// </summary>
    [CreateAssetMenu(menuName = "Gameplay/GameModeData")]
    public class GameMode : ScriptableObject
    {
        public Pawn defaultPawnPrefab;
        public Controller playerController;
    }
}
