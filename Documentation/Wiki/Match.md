## Match

This framework is designed to abstract the game into a series of matches and non-matches, let's define these 2 core terms and their constraints:

- Match: a scene or group of scenes that have a lifecycle and requires Players to control any kind of character (Pawn)
- Non-Match: a scene or group of scenes that provide simple functionality, like a main menu, an intermediate loading screen or any other instance in which there is no need for any character to be cotnroller

this means we can focus on the actual match gameplay "scene" that will define how our game behaves.

First let's join the game rules with the match system syntax/workflow and let's start from the lifecycle:

### A Match Lifecycle

Each match has a pre-defined collection of lifecycle methods that are not entirely automatically managed:
#### Managed
- PlayerJoin
    - Creates a controller object and its associated state
- PlayerSpawn
    - Spawns a pawn controlled by the designed controller
- PlayerRemove
    - Removes a pawn controlled by the designed controller
- PlayerLeave
    - Removes a controller from the match
- Initialize
    - This one is managed automatically, and it's called on Awake before all other objects in scene
    - Join all players retrieved from your own systems (like photon if making an online game)
#### Unmanaged
- Start
    - Starts the match (example: removing barriers in a tactical shooter or unlocking controls in a fighting game)
- Arbitrate
    - Checks if any of the conditions for the match to be ended are met (Example: checking if the red team has 200 points or testing whether the match timer is 0)
- End
    - Ends the match (Example: Removing controlled pawns and showing a scoreboard)
- Finalize
    - Finalizes the match (Example: Returning all players to a "lobby" scene)

Now let's create our own Match class specific to our needs:

create a script and inherit from `GameMatch`

```csharp
//Imported libraries...

public class PongMatch : GameMatch
{
}
```

now, a game match is too barebones because some matches maybe just a sandbox/spectate kind of "game", let's add game state and player state tracking capabilities
with the `IGameStateHandler<>` and `IPlayerStateHandler<>` interfaces and use our custom `GameState` and `PlayerState`
classes as a type parameter

```csharp
//Imported libraries...

public class PongMatch : GameMatch, IGameStateHandler<PongGameState>, IPlayerStateHandler<PongPlayerState>
{
    public PongGameState State { get; set; }
    public IGameStateHandler<PongGameState> AsGameStateHandler => this;
    public List<PongPlayerState> PlayerStates { get; }
    public IPlayerStateHandler<PongPlayerState> AsPlayerStateHandler => this;
}
```

now let's initialize our match by spawning our players

```csharp
// ... rest of class
    
protected override void InitializeMatch()
{
    PlayerJoin(GameMode.playerController);
    PlayerJoin(GameMode.playerController);
}
//...
```


now whenever the scene starts, the match initializes 2 players with their respective ids (0 and 1).

now we could introduce an extra lifecycle given the nature of Pong, each time a goal is scored we need to reset things like
the ball and pawn positions, let's make a custom event only specific to Pong

```csharp
public UnityEvent OnGoalScored;
```

when spawning our pawns, we can observe their `ResetState` behavior with this event

```csharp
public override Pawn SpawnPlayer(int playerId, Pawn pawnPrefab = null, bool ditchPreviousControlledPawn = true)
{
    Pawn ret = base.SpawnPlayer(playerId, pawnPrefab, ditchPreviousControlledPawn);
    if(ret != null)
        OnGoalScored.AddListener(ret.ResetState);
    return ret;
}
```

Now let's start our match by spawning a pawn for each player, this will automatically enable our pawn inputs

```csharp
// ... rest of class
protected override void StartMatch()
{
    SpawnPlayer(0, GameMode.defaultPawnPrefab);
    SpawnPlayer(1, GameMode.defaultPawnPrefab);
}
//...
```

after the match has been initialized, there is only one thing to do for 