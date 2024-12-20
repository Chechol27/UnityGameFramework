namespace Game.State.Core
{
    /// <summary>
    /// Per-level Game "Initializer" reads data and spawns the necessary initial actors into the game
    /// also manages a match lifecycle 
    /// </summary>
    public interface IGameMode : IManagedService
    {
        void InitializeMatch();
        void StartMatch();
        void EndMatch();
    }
}
