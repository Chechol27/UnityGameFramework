using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityGameFramework.Pawns.Core
{
    public abstract class Pawn : MonoBehaviour
    {
        protected PlayerInput Input => GetComponent<PlayerInput>();

        public abstract void ResetState();
        
        public virtual void OnControlled()
        {
            Input.enabled = true;
        }

        public virtual void OnReleased()
        {
            Input.enabled = false;
        }
    }
}
