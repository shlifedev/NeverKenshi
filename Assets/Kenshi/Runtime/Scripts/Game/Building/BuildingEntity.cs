using System; 
using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR 
#endif
namespace Kenshi
{  
    [Serializable]
    public class BuildingConstructData
    {
        [FormerlySerializedAs("_maxBuryHeight")] [SerializeField] float _allowBuryHeight;
        [SerializeField] Transform _leftLinkPoint;
        [SerializeField] Transform _rightLinkPoint;
        public class ArrowHandleAttribute : PropertyAttribute
        {
            
        }
        
        [SerializeField] [ArrowHandle] Vector3 test;

        public float AllowBuryHeight
        {
            get => _allowBuryHeight;
            set => _allowBuryHeight = value;
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


    public class BuildingEntity : Entity, IDrawable
    {
        public override Transform Root { get; }  
        public BuildingConstructData constructData;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private MeshFilter _meshFilter;

        public MeshRenderer MeshRenderer
        {
            get => _meshRenderer;
            set => _meshRenderer = value;
        }

        public MeshFilter MeshFilter
        {
            get => _meshFilter;
            set => _meshFilter = value;
        }
    }
}