namespace Services.Core
{
    /// <summary>
    /// Interface intended for services that need to be located by Game instance or other locator
    /// but is not constrained by the game's world, these require a public parameterless constructor
    /// </summary>
    public interface INativeService
    {
        void InitializeService();
        void FinalizeService();
    }
}
