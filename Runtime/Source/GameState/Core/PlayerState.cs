using UnityEngine;

namespace UnityGameFramework.Game.State.Core
{
    /// <summary>
    /// Contains all data pertaining to the current stat of a particular player
    /// </summary>
    public abstract class PlayerState : MonoBehaviour
    {
        public int PlayerId { get; set; }
    }
}
