using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace Kenshi
{
    public class GameWorld : MonoBehaviour
    {
        public Mesh preBuildingMesh;
        public Material mat;
        // Start is called before the first frame update
        void Start()
        {
            NavMeshBuildSource source = new NavMeshBuildSource(); 
        }

        // Update is called once per frame
        void Update()
        { 
        }
    }

 
    [EditorTool("Activated GameWorld", typeof(GameWorld))] 
    public class GameWorldEditorTool : EditorTool, ISupportsOverlays {
   

        public override void OnActivated()
        {
            SceneView.lastActiveSceneView.ShowNotification(new GUIContent("Entering Platform Tool"), .1f);
        }
        // Called before the active tool is changed, or destroyed. The exception to this rule is if you have manually
        // destroyed this tool (ex, calling `Destroy(this)` will skip the OnWillBeDeactivated invocation).
        public override void OnWillBeDeactivated()
        { 
            SceneView.lastActiveSceneView.ShowNotification(new GUIContent("Exiting Platform Tool"), .1f);
        }

        public override void OnToolGUI(EditorWindow window)
        {   
            Profiler.BeginSample("GameWorld"); 
            var world = target as GameWorld; 
            var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition); 
            if(Physics.Raycast(ray, out var hit, 10000))
            {    
                var cmd = new CommandBuffer();
                var cam = SceneView.currentDrawingSceneView.camera;  
                Vector3 dir = hit.point - hit.normal;
                var rot = Quaternion.LookRotation(dir); 
                var trs = Matrix4x4.TRS(hit.point, quaternion.identity, Vector3.one); 
                cmd.DrawMesh(world.preBuildingMesh, trs, world.mat);  
                Graphics.ExecuteCommandBuffer(cmd);
                Handles.color = Color.green;
                Handles.DrawLine(hit.point, hit.point + (hit.normal * 2), 1.25f); 
      
            }

            Profiler.EndSample();

        } 
    }
}

