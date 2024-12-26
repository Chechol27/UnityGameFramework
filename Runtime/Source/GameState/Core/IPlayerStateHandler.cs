using System;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Pawns.Core;
using UnityGameFramework.Player.Core;
using Object = UnityEngine.Object;

namespace UnityGameFramework.Game.State.Core
{
    /// <summary>
    /// Allows for handling and managing players and their lifecycles across a match
    /// a player's lifecycle:
    /// Join:           When a player first joins the match but is not yet participating in
    ///                 the match's progression (for example in a tactical shooter when a player joins and has to choose a team
    ///                 before being spawned into the game)
    /// 
    /// PlayerSpawn:    When a player has joined the match and decides to start participating in the match's progression
    /// 
    /// PlayerRemove:   When a player decides not to participate in the match's progression (i.e: spectate if allowed)
    /// 
    /// PlayerLeave:    When a player leaves the match
    /// </summary>
    /// <typeparam name="TPlayerState"></typeparam>
    public interface IPlayerStateHandler<TPlayerState> where TPlayerState : PlayerState
    {
        List<TPlayerState> PlayerStates { get; }
        
        IPlayerStateHandler<TPlayerState> AsPlayerStateHandler { get; }

        void SpawnPlayerState(int playerId)
        {
            GameObject playerStateGo = new GameObject($"Player {playerId} State");
            TPlayerState playerState = playerStateGo.AddComponent<TPlayerState>();
            playerState.PlayerId = playerId;
            PlayerStates.Add(playerState);
        }
    }
}