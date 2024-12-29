# Pawn Scripts

The pawn creation is a similar workflow, so, first let's create a child class of `Pawn`, these are going to be the actual
Objects the players are going to control in game, think of them as the characters of this game

```csharp
public class PongPawn : Pawn
{
}
```

thinking about how _**Pong**_ works, we'll need to implement a way to reset our `Pawn`'s position each time a goal is scored
for that let's do a bit of logic, fortunately, the `Pawn` parent class provides an overrideable method called `ResetState`
designed for a Respawn-like kind of situation

```csharp
public class PongPawn : Pawn
{
    private Vector3 startPosition;
    
    public override void ResetState()
    {
        transform.position = startPosition;
    }

    private void Awake()
    {
        startPosition = transform.position;
    }
}
```

also, since this is the actual controlled object, let's make a script for movement that works with the new Input system

```csharp
using UnityEngine.InputSystem;
using UnityGameFramework.Pawns.Core;

namespace UnityGameFramework.Samples.Pong
{
    [RequireComponent(typeof(Rigidbody))]
    public class PongPawnMovement : MonoBehaviour, IPawnComponent
    {
        [SerializeField] private float maxMotion = 5f;
        [SerializeField] private float motionVelocity = 5f;
        public Pawn ParentPawn { get; set; }
        
        private float currentPosition = 0;
        private float targetPosition;
        private float currentVelocity;

        private Rigidbody rigidbody;

        public void OnMove(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;

            Vector2 val = ctx.ReadValue<Vector2>();
            targetPosition = val.y > 0 ? 1 : 0;
        }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            currentPosition = Mathf.SmoothDamp(currentPosition, targetPosition, ref currentVelocity, 1/motionVelocity);
        }

        private void FixedUpdate()
        {
            rigidbody.position = Vector3.Lerp(((PongPawn)ParentPawn).SpawnPosition - transform.right * maxMotion, ((PongPawn)ParentPawn).SpawnPosition + transform.right * maxMotion, targetPosition);
        }
    }
}
```

now that we've got all the logic needed for our pawns, we have to create, setup and register their respective prefabs.

## Nect up... [Pawn part 2: prefabs](./PawnPrefab.md)