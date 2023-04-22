using System.Collections;
using System.Collections.Generic; 
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.AI;
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


    [Overlay(typeof(SceneView), " Builder Tool", true)]
    public class GameWorldEditorToolOverlay : Overlay
    {
        public override VisualElement CreatePanelContent()
        {
            var root = new VisualElement() { name = "My Toolbar Root" };
            root.Add(new Label() { text = "Hello" });
            return root;
        }
    }

    [EditorTool("Activated GameWorld", typeof(GameWorld))] 
    public class GameWorldEditorTool : EditorTool, ISupportsOverlays { 
        void OnEnable()
        {
            // Allocate unmanaged resources or perform one-time set up functions here
            Debug.Log(" game world editor tool"); 
        }

        void OnDisable()
        { 
        }
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
            if (!(window is SceneView sceneView))
                return;

            Handles.BeginGUI();
            var world = target as GameWorld;
         
            var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition); 
            if(Physics.Raycast(ray, out var raycastResult, 10000))
            {  
                Graphics.DrawMesh(world.preBuildingMesh, raycastResult.point, Quaternion.identity, world.mat, 0);
                Handles.color = Color.red;
                Handles.DrawWireCube(raycastResult.point + world.preBuildingMesh.bounds.center, Vector3.one * 8);
                Handles.DrawLine(raycastResult.point + world.preBuildingMesh.bounds.center, Vector3.one);
                Handles.Label(raycastResult.point + world.preBuildingMesh.bounds.center, "center"); 
            }
            SceneView.currentDrawingSceneView.Repaint();
            Handles.EndGUI();
        }
        
        

        // IDrawSelectedHandles interface allows tools to draw gizmos when the target objects are selected, but the tool
        // has not yet been activated. This allows you to keep MonoBehaviour free of debug and gizmo code.
        public void OnDrawHandles()
        {
           
        }
    }
}

