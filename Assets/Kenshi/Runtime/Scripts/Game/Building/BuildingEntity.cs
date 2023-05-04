using System;
using System.Collections.Generic; 
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization; 
#if UNITY_EDITOR 
#endif
namespace Kenshi
{  
    [Serializable]
    public class BuildingConstructData 
    {
        [FormerlySerializedAs("_maxBuryHeight")] 
        [SerializeField] float _allowBuryHeight; 
        [SerializeField] private Bounds bound;
        public float AllowBuryHeight
        {
            get => _allowBuryHeight;
            set => _allowBuryHeight = value;
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

        void OnDrawGizmos()
        {
            Vector3[] controlPoints = new Vector3[5];
            controlPoints[0] = new Vector3(-6, -2, 32);
            controlPoints[1] = new Vector3(-2, 4, 12);
            controlPoints[2] = Vector3.zero;
            controlPoints[3] = new Vector3(32, -4, 4);
            controlPoints[4] = new Vector3(1, 2, 2);
 
            for (int i = 0; i <= 50; i++)
            {
                float t = (float)i / 50f;
                Vector3 point = BezierCurve(controlPoints, t);
                float r = Mathf.Lerp(0f, 1f, t);
                Gizmos.color = new Color(r, r, r);

                Gizmos.DrawSphere(point, 0.1f);
            }
        }

        private Vector3 BezierCurve(Vector3[] points, float t)
        {
            if (points == null || points.Length < 2)
                return Vector3.zero;

            Vector3[] temp = points.Clone() as Vector3[];

            for (int k = 1; k < temp.Length; k++)
            {
                for (int i = 0; i < temp.Length - k; i++)
                {
                    temp[i] = (1 - t) * temp[i] + t * temp[i + 1];
                }
            }
            return temp[0];
        }
    }
}

 