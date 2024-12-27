## Game State

Now that we have de initial default data, let's start with the mutable data that represents the current state of the game,
here we could save things that are conceptually general to the match but not necesarilly attributes of a Player's state,
in the case of pong we could save the following information:

- Current match time
- Player that scored a goal previously (to send the ball in that player's direction)

the game state is not a `ScriptableObject` but a `MonoBehaviour` script, this could be a simple native class but making it a component
allows us to check the state directly as an object in the scene. Let's create a script, inherit from `GameState` and add the previously defined fields

```csharp
using UnityGameFramework.Game.State.Core;

public class PongGameState : GameState
{
    public float currentMatchTimer;
    public int lastScoredPlayerId;
}
```

now we've got an object that will store the current match data.