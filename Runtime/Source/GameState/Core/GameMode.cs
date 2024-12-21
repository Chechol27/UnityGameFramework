using UnityEngine;

namespace Game.State.Core
{
    /// <summary>
    /// Base class for game modes, defines all data needed for the game rules to be executed and enforced correctly
    /// </summary>
    [CreateAssetMenu(menuName = "Gameplay/GameModeData")]
    public class GameMode : ScriptableObject
    {
#warning TODO: implement nice coupling with pawns
        public GameObject defaultPawn;

#warning TODO: implement nice coupling with player controllers
        public GameObject playerController;
    }
}
