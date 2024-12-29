using UnityEngine;
using UnityEngine.Events;
using UnityGameFramework.Pawns.Core;

namespace UnityGameFramework.Samples.Pong
{
    public class PongPawn : Pawn
    {
        public UnityEvent OnMoved;
        private Vector3 spawnPosition;

        protected override void Awake()
        {
            base.Awake();
            spawnPosition = transform.position;
        }

        public Vector3 SpawnPosition => spawnPosition;
    }
}