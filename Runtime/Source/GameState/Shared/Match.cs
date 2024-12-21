using System;
using System.Collections.Generic;
using Game.State.Core;
using UnityEngine;

namespace Game.State.Shared
{
    /// <summary>
    /// Base class for matches, corresponds to a single-player "Sandbox" state where the player can move freely
    /// </summary>
    public class Match: GameMatch<GameMode, GameState, PlayerState>
    {
        [field:SerializeField]protected override GameMode GameMode { get; set; }
        
        private void Awake()
        {
            InitializeMatch(1);
        }
    }
}