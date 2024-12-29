using UnityEngine;
using UnityEngine.InputSystem;
using UnityGameFramework.Player.Core;

namespace UnityGameFramework.Pawns.Core
{
    [DefaultExecutionOrder(-2)]
    [RequireComponent(typeof(PlayerInput))]
    public abstract class Pawn : MonoBehaviour
    {
        protected Controller CurrentController;
        public PlayerInput Input => GetComponent<PlayerInput>();

        protected virtual void Awake()
        {
            foreach (IPawnComponent pawnComponent in GetComponentsInChildren<IPawnComponent>())
            {
                pawnComponent.RegisterPawn(this);
            }
        }

        public virtual void Restart()
        {
            foreach (IPawnComponent pawnComponent in GetComponentsInChildren<IPawnComponent>())
            {
                pawnComponent.Restart();
            }
        }
        
        public virtual void OnControlled(Controller controller)
        {
            CurrentController = controller;
            Input.enabled = true;
        }

        public virtual void OnReleased()
        {
            Input.enabled = false;
        }
    }
}
