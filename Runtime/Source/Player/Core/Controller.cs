using UnityEngine;
using UnityEngine.Assertions;
using UnityGameFramework.Pawns.Core;

namespace UnityGameFramework.Player.Core
{
    public abstract class Controller : MonoBehaviour
    {
        public Pawn ControlledPawn { get; protected set; }

        public virtual void Control(Pawn pawnToControl, bool ditchControlledPawn = true)
        {
            Assert.IsNotNull(pawnToControl, $"Passed pawn to {gameObject.name} controller, was null");
            if (ControlledPawn != null)
            {
                if (!ditchControlledPawn)
                {
                    Debug.LogWarning($"This controller is already controlling a pawn, call with \"{nameof(ditchControlledPawn)}\" as True");
                    return;
                }
                Release();
            }

            ControlledPawn = pawnToControl;
            ControlledPawn.OnControlled();
        }

        public virtual void Release()
        {
            ControlledPawn.OnReleased();
            ControlledPawn = null;
        }
    }
}
