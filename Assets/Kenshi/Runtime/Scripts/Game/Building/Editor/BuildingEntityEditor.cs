using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Kit;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.Profiling;
using UnityEngine; 
using UnityEngine.UIElements; 

namespace Kenshi
{
   
    [UnityEditor.CustomEditor(typeof(BuildingEntity))] 
    public class BuildingEntityEditor : KitCustomEditor
    { 
        public const string id = "ExampleToolbar/DropdownToggle";  
        public override void OnDuringSceneGUI(SceneView view)
        {  
             
        } 
    }
}