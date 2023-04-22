using System;
using BOM.Editor;
using UnityEditor.EditorTools;
using UnityEditor.Overlays;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Kenshi
{  
    [Serializable]
    public class BuildingConstructData
    {
        [SerializeField] float _maxBuryHeight;
        [SerializeField] Transform _leftLinkPoint;
        [SerializeField] Transform _rightLinkPoint;
        public class ArrowHandleAttribute : PropertyAttribute
        {
            
        }
        
        [SerializeField] [ArrowHandle] Vector3 test;

        public float MaxBuryHeight
        {
            get => _maxBuryHeight;
            set => _maxBuryHeight = value;
        }

        public Transform LeftLinkPoint
        {
            get => _leftLinkPoint;
            set => _leftLinkPoint = value;
        }

        public Transform RightLinkPoint
        {
            get => _rightLinkPoint;
            set => _rightLinkPoint = value;
        }
    }


    public class BuildingEntity : Entity
    {
        public override Transform Root { get; }
        public MeshRenderer meshRenderer;
        public BuildingConstructData constructData;  
    }
}