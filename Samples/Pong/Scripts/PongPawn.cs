using UnityEngine;
using UnityGameFramework.Pawns.Core;

namespace UnityGameFramework.Samples.Pong
{
    public class PongPawn : Pawn
    {
        private Vector3 spawnPosition;
        
        public override void ResetState()
        {
            transform.position = spawnPosition;
        }

        protected override void Awake()
        {
            base.Awake();
            spawnPosition = transform.position;
        }

        public Vector3 SpawnPosition => spawnPosition;
    }
}