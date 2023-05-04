using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Kenshi.Runtime.Scripts.Editor
{
    public class KenshiPrefabAssetPostProcessor : AssetPostprocessor
    {
        private const string PREFAB_BUILDING = "Assets/Kenshi/Runtime/Prefabs/Buildings";
        private void OnPostprocessPrefab(GameObject go)
        {
            // var path = AssetDatabase.GetAssetPath(go);
            // if (path == PREFAB_BUILDING)
            // {
            //     var settings = AddressableAssetSettingsDefaultObject.Settings;
            //     var group = settings.FindGroup("Kenshi"); 
            // }
        } 
         
    }
}