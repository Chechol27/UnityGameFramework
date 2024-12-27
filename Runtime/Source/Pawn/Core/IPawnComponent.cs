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
    }
}