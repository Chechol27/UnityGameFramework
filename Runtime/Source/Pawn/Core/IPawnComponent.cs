using UnityEngine;

namespace UnityGameFramework.Pawns.Core
{
    public interface IPawnComponent
    {
        Pawn ParentPawn { get; set; }
        void RegisterPawn(Pawn pawn)
        {
            Debug.Log($"Registering parent pawn: {pawn}");
            ParentPawn = pawn;
        }

        void Restart()
        {
            
        }
    }
    
    public interface IPawnComponent<out TPawn> : IPawnComponent where TPawn : Pawn
    {
        TPawn ParentPawn => ((IPawnComponent) this).ParentPawn as TPawn;
    }
}