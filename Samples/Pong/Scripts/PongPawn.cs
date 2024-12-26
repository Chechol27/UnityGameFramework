using System;
using UnityEngine;
using UnityGameFramework.Pawns.Core;

namespace UnityGameFramework.Samples.Pong
{
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
}