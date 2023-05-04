using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Kit;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Kenshi
{ 
    public class ResourceManager : SingletonBehaviour<ResourceManager>
    {
        
        public Dictionary<string, AsyncOperationHandle<GameObject>> loadedGameObject = new Dictionary<string, AsyncOperationHandle<GameObject>>();

        public UniTask<T> LoadAsset<T>(string key) where T : UnityEngine.Object
        {
            if (loadedGameObject.ContainsKey(key))
            {
                var handle = loadedGameObject[key]; 
            }
            else
            {
                Addressables.LoadAssetAsync<T>(key); 
            }

            return default;
        }

    }
    
    
}