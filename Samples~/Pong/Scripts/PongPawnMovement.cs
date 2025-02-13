using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityGameFramework.Game;
using UnityGameFramework.Pawns.Core;

namespace UnityGameFramework.Samples.Pong
{
    [RequireComponent(typeof(Rigidbody))]
    public class PongPawnMovement : MonoBehaviour, IPawnComponent
    {
        [SerializeField] private float maxMotion = 5f;
        [SerializeField] private float motionVelocity = 1f;
        public Pawn ParentPawn { get; set; }
        
        [SerializeField]private float currentDelta;
        [SerializeField]private float targetDelta;
        private float currentVelocity;

        
        private float t;

        private Rigidbody rigidbody;

        public void Restart()
        {
            targetDelta = 0;
            currentDelta = 0;
            Vector3 spawnPosition = ((PongPawn)ParentPawn).SpawnPosition;
            rigidbody.position = spawnPosition;
        }

        public void OnMove(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                (ParentPawn as PongPawn)?.OnMoved?.Invoke();
                Vector2 val = ctx.ReadValue<Vector2>();
                targetDelta = val.y;
            }
            else
            {
                targetDelta = 0;
            }
        }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Vector3 spawnPosition = ((PongPawn)ParentPawn).SpawnPosition;
            currentDelta =
                Mathf.SmoothDamp(currentDelta, targetDelta, ref currentVelocity, Time.fixedDeltaTime * 3f);
            t += currentDelta * Time.fixedDeltaTime * motionVelocity;
            t = Mathf.Clamp(t, -1, 1);
            rigidbody.position = Vector3.Lerp(spawnPosition - Vector3.forward * maxMotion, spawnPosition + Vector3.forward * maxMotion, t * 0.5f + 0.5f);
        }
    }
}
