## PlayerState

Now that we know what data will be updated on a match level, let's add the lower level: Player specific data

in this case we'll only need one single filed to store:

- Score

so let's create a script, inherit from `PlayerState` and add the field:

```csharp
using UnityGameFramework.Game.State.Core;

public class PongPlayerState : PlayerState
{
    public int score;
}
```