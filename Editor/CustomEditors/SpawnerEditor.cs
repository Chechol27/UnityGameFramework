using UnityEditor;
using UnityEngine;
using UnityGameFramework.Pawns.Core;

namespace UnityGameFramework.EditorOnly
{
    public class GameInstanceGizmoDrawer
    {
        [DrawGizmo(GizmoType.Pickable | GizmoType.Selected | GizmoType.Active, typeof(Spawner))]
        public static void DrawGizmos(Component component, GizmoType gizmoType)
        {
            Transform transform = component.transform;
            float lineThickness = 0.5f;
            Handles.DrawWireDisc(transform.position + transform.up, transform.up, 0.5f, lineThickness);
            Handles.DrawWireDisc(transform.position - transform.up, transform.up, 0.5f, lineThickness);
            Handles.DrawWireDisc(transform.position, transform.up, 0.5f, lineThickness);
            Vector3 upperDiscCenter = transform.position + transform.up;
            Vector3 upperDiscRight = transform.position + transform.right * 0.5f + transform.up;
            Vector3 upperDiscLeft = upperDiscRight - transform.right;
            Vector3 upperDiscTop = transform.position + transform.forward * 0.5f + transform.up;
            Vector3 upperDiscBottom = upperDiscTop - transform.forward;

            Vector3 lowerDiscCenter = transform.position - transform.up;
            Vector3 lowerDiscRight = upperDiscRight - transform.up * 2;
            Vector3 lowerDiscLeft = upperDiscLeft - transform.up * 2;
            Vector3 lowerDiscTop = upperDiscTop - transform.up * 2; 
            Vector3 lowerDiscBottom = upperDiscBottom - transform.up * 2;
            Handles.DrawLine(upperDiscRight, lowerDiscRight, lineThickness);
            Handles.DrawLine(upperDiscLeft, lowerDiscLeft, lineThickness);
            Handles.DrawLine(upperDiscTop, lowerDiscTop, lineThickness);
            Handles.DrawLine(upperDiscBottom, lowerDiscBottom, lineThickness);

            Matrix4x4 handleMatrix = Handles.matrix;
            Handles.matrix = transform.localToWorldMatrix;
            Handles.DrawWireArc(Vector3.up, Vector3.forward, Vector3.right, 180, 0.5f, lineThickness);
            Handles.DrawWireArc(Vector3.up, -Vector3.right, Vector3.forward, 180, 0.5f, lineThickness);
            
            Handles.DrawWireArc(-Vector3.up, -Vector3.forward, Vector3.right, 180, 0.5f, lineThickness);
            Handles.DrawWireArc(-Vector3.up, Vector3.right, Vector3.forward, 180, 0.5f, lineThickness);
            Handles.ArrowHandleCap(0, Vector3.zero, Quaternion.identity, 1, EventType.Repaint);
            Handles.matrix = handleMatrix;
        }
    }
    public class SpawnerEditor
    {
        
    }
}