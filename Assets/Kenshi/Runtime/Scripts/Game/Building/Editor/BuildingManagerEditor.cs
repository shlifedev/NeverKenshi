 
using Drawing;
using Kit; 
using Unity.Mathematics;
using UnityEditor; 
using UnityEngine;
using UnityEngine.Rendering; 
using UnityEngine.UIElements;
using Draw = Drawing.Draw;

namespace Kenshi
{
    [UnityEditor.CustomEditor(typeof(BuildingConstructManager))]
    public class BuildingManagerEditor : KitCustomEditor 
    {
        private Tool m_lastUsedTool = Tool.None;
        private int m_performance_test_stack_cnt = 0;

        
        public override void OnDuringSceneGUI(SceneView view)
        {
            var t = target as BuildingConstructManager;  
        }
    }
}