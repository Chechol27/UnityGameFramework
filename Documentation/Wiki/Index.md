# Unity Game Framework

Unity is a very powerful and user-friendly game engine that allows for great projects to be made. its user friendliness
is in part thanks to how "barebones" the engine is shipped in regards to a framework.

This project is one of many that tries to standardize a way to manage Game states, matches, players and controllers; a
battle tested, well known architectural approach present in other engines such as Unreal or in house proprietary engines.

here's an overview of de different systems currently at play and how they interact with each other as well as a simple guide
to extend and use this framework in your games

```mermaid
flowchart TB
    gameInstance["`_**Game Instance**_
     - Manages all services
     - Persistent across scenes`"]
     
     gameMode(("`_**Game Mode**_
     - Initialization data for matches
     - Defines default controllers
     - Defines default pawns
     - Defines default states (Player and Game)`"))
     
    gameMatch["`_**Game Match**_
    - Manages match lifecycle (join, start, win/loss, end)
    - Manages current game state`"]
    
    gameState["`_**Game State**_
    - Current match information (team scores, match timer, etc)
    - Level dependent`"]
    
    playerState["`_**PlayerState**_
    - Individual player information (Player score, health, shield, money, etc)`"]
    
    pawnSpawner["`_**Pawn Spawner**_
    Provides a world position to spawn pawns`"]

    controller["`_**Controller**_
    - Non physical part of a player
    - Controls the in-scene Pawns as a brain
    - Either AI or Human`"]
    
    pawn["`_**Pawn**_
    - Physical part of a player
    - The actual characters visible in-game
    - Controlled by any type of controller
    - Receive input either via Input system for humans or proxy actions for AI`"]
    
    gameInstance --manages, provides static access--> gameMatch
    gameInstance --manages, provides static access---> pawnSpawner
    gameMatch --manages, provides static access--> gameState
    gameMode --read on consturction--->gameMatch
    gameMatch --manages, provides static access, multiple instances--> playerState
    gameMatch --manages, provides static access, multiple instances--> controller
    
    controller --controls--> pawn
```