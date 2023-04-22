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
        /// 씬뷰에 생성될 에디터용 비주얼 엘리먼트
        /// </summary>
        /// <param name="created"></param>
        void OnCreateVisualElement(VisualElement created);
        /// <summary>
        /// 씬뷰에 GUI를 그립니다.
        /// </summary>
        /// <param name="view"></param>
        void OnDuringSceneGUI(SceneView view);
    }
    /// <summary>
    /// 씬 에디터 툴을 쉽게 관리하기 위한 전략
    /// </summary>
    public abstract class SceneEditor : UnityEditor.Editor, ISceneEditor
    { 
        /// <summary>
        /// Is Object Selected?
        /// </summary>
        public static bool Selected;
        public abstract void OnDuringSceneGUI(SceneView view);
        protected SceneView currentActiveSceneView => SceneView.lastActiveSceneView;
        protected VisualElement rootVisualElement => currentActiveSceneView.rootVisualElement;

        protected VisualElement editorVisualElement
        {
            get;
            set;
        }
         

        /// <summary>
        /// editorVisualElement 에 캐싱됨.
        /// </summary>
        /// <param name="created">editorVisualElement</param>
        public virtual void OnCreateVisualElement(VisualElement created)
        {
            editorVisualElement = created;
        }
        protected void Repaint()
        {
            SceneView.RepaintAll();
        }
 
        public virtual void OnEnable()
        {
        
            Debug.Log("handle");
            Selected = true; 
            SceneView.duringSceneGui += OnDuringSceneGUI;  
            if(rootVisualElement != null)
               rootVisualElement.Add(editorVisualElement);
        }

        public virtual void OnDisable()
        {
            Selected = false;
            SceneView.duringSceneGui -= OnDuringSceneGUI; 
            if(rootVisualElement != null)
               rootVisualElement.Remove(editorVisualElement);
        }
        
        
    }
}