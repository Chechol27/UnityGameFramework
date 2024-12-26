using System;
using Services.Core;
using UnityEngine;
using UnityGameFramework.Game.State.Core;
using UnityGameFramework.Game;

namespace UnityGameFramework.Pawns.Core
{
    public class Spawner : MonoBehaviour, IManagedService
    {
        protected void AssertPawnUsability(Pawn pawnPrefab)
        {
            GameObject pawnGo = pawnPrefab.gameObject;
            if (pawnGo.scene.name != null || pawnGo.scene.name != gameObject.name)
                throw new ArgumentException($"Spawned pawn ({pawnGo.name}) is required to be a prefab");
        }

        public virtual void SetPawnWorldPositioning(Pawn pawn)
        {
            pawn.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }

        public virtual TPawn Spawn<TPawn>(int controllerId, TPawn pawnPrefab = null) where TPawn : Pawn
        {
            AssertPawnUsability(pawnPrefab);
            GameMatch match = GameInstance.Main.GetManagedSubSystem<GameMatch>();
            TPawn pawn = (TPawn)match.SpawnPlayer(controllerId, pawnPrefab, false);
            SetPawnWorldPositioning(pawn);
            return pawn;
        }

        public bool IsPersistent => false;
    }
}
