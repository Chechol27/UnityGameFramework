using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Game;
using UnityGameFramework.Pawns.Core;
using Component = UnityEngine.Component;

namespace UnityGameFramework.EditorOnly
{
    
    
    [CustomEditor(typeof(GameInstance))]
    public class GameInstanceEditor : Editor
    {
    }
}
