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
        private Matrix4x4 _constructMatrix; 
   
        public override void OnDuringSceneGUI(SceneView view)
        { 
            _constructMatrix = entity.transform.localToWorldMatrix;   
            Handles.matrix = _constructMatrix; 
            Handles.DrawWireCube(new Vector3(0,0,0), Vector3.one);


            var entityMatrix = Matrix4x4.identity * _constructMatrix;
            entityMatrix.SetTRS(entityMatrix.GetPosition(), entityMatrix.rotation, new Vector3(1,1,1));
            Handles.matrix = entityMatrix;

            var leftX = (entity.meshRenderer.bounds.size.x * 0.5f) - 1;
            
            Handles.DrawWireCube(new Vector3(-leftX,0,0), Vector3.one * 2); 
            
            Handles.BeginGUI();
            Handles.EndGUI();
            Vector3[] vertices = {
                new Vector3 (0, 0, 0),
                new Vector3 (1, 0, 0),
                new Vector3 (1, 1, 0),
                new Vector3 (0, 1, 0),
                new Vector3 (0, 1, 1),
                new Vector3 (1, 1, 1),
                new Vector3 (1, 0, 1),
                new Vector3 (0, 0, 1),
            };
            
            int[] triangles = {
                0, 2, 1, //face front
                0, 3, 2,
                2, 3, 4, //face top
                2, 4, 5,
                1, 2, 5, //face right
                1, 5, 6,
                0, 7, 4, //face left
                0, 4, 3,
                5, 4, 7, //face back
                5, 7, 6,
                0, 6, 7, //face bottom
                0, 1, 6
            };

            var mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.Optimize();

            var buffer = new CommandBuffer();  
            buffer.DrawMesh(mesh, Handles.matrix, entity.meshRenderer.sharedMaterial, 0, 0); 
            Graphics.ExecuteCommandBuffer(buffer); 
        }
 
        public override void OnEnable()
        { 
            
            entity = (BuildingEntity)target;
            _constructMatrix = entity.transform.localToWorldMatrix; 
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