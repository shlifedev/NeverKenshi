 
using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;

namespace Kenshi
{
    [CustomEditor(typeof(BuildingConstructManager))]
    public class BuildingLevelDesignEditor : Editor, ISupportsOverlays
    {
        private Tool m_lastUsedTool = Tool.None;
        private int m_performance_test_stack_cnt = 0;
        private void OnDuringSceneGUI(SceneView view)
        {
            Handles.DrawWireCube(Vector3.zero, Vector3.one); 
            var buildingManager = target as BuildingConstructManager;
            var position = buildingManager.transform.position + Vector3.forward;
            Handles.Slider (position, buildingManager.transform.right); //X 축
            
        }

     
        private void OnSceneGUI() 
        {     
            var buildingManager = target as BuildingConstructManager;
            var ev = Event.current;

            if (buildingManager != null)
            {
                var position = buildingManager.transform.position;
                Handles.Label(position, $"{Event.current.ToString()}"); 
                Handles.Button(position, Quaternion.identity, 15f, 3f, Handles.RectangleHandleCap);
            }    
            
            switch (ev.type)
            {
                case EventType.MouseDown:
                    if (ev.button == 0)
                    {
                        Event.current.Use();
                    } 
                    break;
            
            }
    
        }

        private void OnEnable()
        {
            m_performance_test_stack_cnt++; 
            SceneView.duringSceneGui += OnDuringSceneGUI;
            m_lastUsedTool = Tools.current;
            Tools.current = Tool.None;
        }

        private void OnDisable()
        { 
            SceneView.duringSceneGui -= OnDuringSceneGUI;
            Tools.current = m_lastUsedTool;
        }
    }
}