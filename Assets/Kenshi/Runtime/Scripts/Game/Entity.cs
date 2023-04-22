using System;
using BOM;
using Kenshi;
using UnityEditor;
using UnityEngine;
namespace Kenshi
{

    public interface IDrawable
    {
        public MeshRenderer MeshRenderer { get; set; }
        public MeshFilter MeshFilter { get; set; }
    }
    
    public abstract class Entity : MonoBehaviour 
    {
        public abstract Transform Root { get; }  
    }
}