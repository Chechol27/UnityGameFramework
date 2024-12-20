using UnityEngine;

/// <summary>
/// Base class for game modes, defines all data needed for the game rules to be executed and enforced correctly
/// </summary>
public class GameModeData : ScriptableObject
{
#warning TODO: implement nice coupling with pawns
    public MonoBehaviour defaultPawn;

#warning TODO: implement nice coupling with game states
    public MonoBehaviour gameState;
    
#warning TODO: implement nice coupling with player states
    public MonoBehaviour playerState;
    
#warning TODO: implement nice coupling with player controllers
    public MonoBehaviour playerController;
}
