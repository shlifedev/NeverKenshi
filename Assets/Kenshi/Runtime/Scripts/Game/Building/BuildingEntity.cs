using System; 
using UnityEngine; 
#if UNITY_EDITOR 
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