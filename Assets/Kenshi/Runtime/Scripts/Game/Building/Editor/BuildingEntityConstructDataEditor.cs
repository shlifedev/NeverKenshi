using BOM;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Rendering;

namespace Kenshi
{
    [CustomEditor( typeof(BuildingEntity))] 
    public class BuildingEntityConstructDataEditor : SceneEditor, ISupportsOverlays
    { 
        public const string id = "ExampleToolbar/DropdownToggle";
        public BuildingEntity entity;
        private Matrix4x4 entityMatrix; 
   
        public override void OnDuringSceneGUI(SceneView view)
        { 
            this.entityMatrix = entity.transform.localToWorldMatrix;   
            Handles.matrix = this.entityMatrix; 
            Handles.DrawWireCube(new Vector3(0,0,0), Vector3.one); 
          
            // 엔티티 위치와 회전 기준으로 피벗을 재설정한다
  
            Handles.matrix = Matrix4x4.TRS(entityMatrix.GetPosition(), entityMatrix.rotation, Vector3.one);; 
            var leftX = (entity.meshRenderer.bounds.size.x * 0.5f) - 1; 
            Handles.DrawWireCube(new Vector3(-leftX,0,0), Vector3.one * 2);  
        }
        
 
        public override void OnEnable()
        { 
            
            entity = (BuildingEntity)target;
            entityMatrix = entity.transform.localToWorldMatrix; 
            base.OnEnable();
        }

        public override void OnDisable()
        {
            entity = null;  
            base.OnDisable();
        }
 
        [DrawGizmo(GizmoType.Selected)]
        static void DrawGizmos(BuildingEntity entity, GizmoType gizmoType)
        { 
            Gizmos.matrix = entity.transform.localToWorldMatrix; 
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
        }
    }
}