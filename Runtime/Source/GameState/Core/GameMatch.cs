using System;
using System.Collections.Generic;
using Services.Core;
using UnityEngine;
using UnityGameFramework.Pawns.Core;
using UnityGameFramework.Player.Core;

namespace UnityGameFramework.Game.State.Core
{
    #pragma warning disable 0108
    /// <summary>
    /// Per-level Game match manager:
    /// Reads data and spawns the necessary initial actors into the game
    /// Manages match lifecycle 
    /// </summary>
    [DefaultExecutionOrder(-3)]
    public abstract class GameMatch: MonoBehaviour, IManagedService
    {
        [field:SerializeField]protected GameMode GameMode { get; set; }
        public List<Controller> Players { get; private set; } = new List<Controller>();

        public virtual void PlayerJoin(Controller playerPrefab)
        {
            int playerId = Players.Count;
            Controller player = Instantiate(playerPrefab);
            player.gameObject.name = $"Player {playerId}";
            player.MatchId = playerId;
            Players.Add(player);
        }

        public virtual Pawn SpawnPlayer(int playerId, Pawn pawnPrefab = null, bool ditchPreviousControlledPawn = true)
        {
            try
            {
                pawnPrefab = pawnPrefab == null ? GameMode.defaultPawnPrefab : pawnPrefab;
                Controller desiredController = Players[playerId];
                Spawner spawner = GameInstance.Main.GetManagedSubSystem<Spawner>(false, playerId);
                Matrix4x4 spawnTransform = Matrix4x4.identity;
                if (spawner != null)
                {
                    spawnTransform = spawner.GetWorldPositioning;
                }
                Pawn pawn = Instantiate(pawnPrefab, spawnTransform.GetPosition(),spawnTransform.rotation);
                desiredController.Control(pawn, ditchPreviousControlledPawn);
                return pawn;
            }
            catch (IndexOutOfRangeException)
            {
                Debug.LogError($"Player #{playerId} is invalid");
            }
            //desired controller may be null or even the pawn to instantiate, let unity handle those exceptions
            return null;
        }

        public virtual void PlayerRemove(int playerId)
        {
            try
            {
                Controller player = Players[playerId];
                Pawn pawn = player.ControlledPawn;
                Players[playerId].Release();
                if(pawn != null)
                    Destroy(pawn.gameObject);
            }
            catch (IndexOutOfRangeException)
            {
                Debug.LogError($"Player #{playerId} is invalid");
            }
        }

        public virtual void PlayerLeave(int playerId)
        {
        }
        
        protected virtual void InitializeMatch()
        {
        }

        protected virtual void StartMatch()
        {
        }

        protected virtual void ArbitrateMatch()
        {
        }

        protected virtual void EndMatch()
        {
        }

        protected virtual void FinalizeMatch()
        {
            
        }

        protected void Awake()
        {
            InitializeMatch();
        }

        public bool IsPersistent => false;
    }
}
