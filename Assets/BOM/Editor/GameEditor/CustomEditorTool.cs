using System;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Graphs;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace BOM
{


    public interface ISceneEditor
    { 
        SceneView currentActiveSceneView => SceneView.lastActiveSceneView;
        VisualElement sceneViewRootElement => currentActiveSceneView.rootVisualElement;  
        /// <summary>
        /// 씬뷰에 GUI를 그립니다.
        /// </summary>
        /// <param name="view"></param>
        void OnDuringSceneGUI(SceneView view);
    }
    /// <summary>
    /// 씬 에디터 툴을 쉽게 관리하기 위한 전략
    /// </summary>
    public abstract class CustomEditorTool : EditorTool, ISceneEditor
    {
        public virtual bool UseDuringScene
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Is Object Selected?
        /// </summary>
        public static bool Selected;
        public abstract void OnDuringSceneGUI(SceneView view);
        protected SceneView currentActiveSceneView => SceneView.lastActiveSceneView;

        protected VisualElement rootVisualElement
        {
            get
            {
                if (currentActiveSceneView == null) return null;
                if (currentActiveSceneView.rootVisualElement == null) return null;
                return  currentActiveSceneView.rootVisualElement;
                
            }
        }

        protected virtual VisualElement EditorVisualElement
        {
            get; 
        }
 

        protected void Repaint()
        {
            SceneView.RepaintAll();
        }
 
        public virtual void OnEnable()
        { 
            Selected = true; 
            if(UseDuringScene)
               SceneView.duringSceneGui += OnDuringSceneGUI;  
            if(rootVisualElement != null && EditorVisualElement != null)
               rootVisualElement.Add(EditorVisualElement); 
        }

        public virtual void OnDisable()
        {
            Selected = false;
            if(UseDuringScene)
               SceneView.duringSceneGui -= OnDuringSceneGUI; 
            
            if(rootVisualElement != null&& EditorVisualElement != null)
               rootVisualElement.Remove(EditorVisualElement);
        }
        
        
    }
}