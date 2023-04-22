using System;
using BOM;
using Kenshi;
using UnityEditor;
using UnityEngine;
namespace Kenshi
{
    
    
    public abstract class Entity : MonoBehaviour, IEntity
    {
        public abstract Transform Root { get; } 

    }
}