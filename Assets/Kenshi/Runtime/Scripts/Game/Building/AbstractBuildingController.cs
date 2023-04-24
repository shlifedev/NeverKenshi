using System;
using UnityEngine;

namespace Kenshi
{
    public abstract class AbstractBuildingController : IBuildingConstructController
    {
        public const int TerrainAndBuildingLayerMask = 1 << 6 | 1 << 7;

        public delegate void StateChangeDelegate(BuildingConstructManager.BuildingState prev,
            BuildingConstructManager.BuildingState current);

        public StateChangeDelegate onStateChangeDelegate;
        public BuildingConstructManager.BuildingState State { get; set; }

        public BuildingConstructManager Instance
        {
            get { return BuildingConstructManager.Instance; }
        }  

        public virtual RaycastHit? RaycastAbovePoint(Vector2 screenPos)
        {
            var camera = Camera.main;
            var ray = camera.ScreenPointToRay(screenPos);
            var casted = Physics.Raycast(ray, out var hit, maxDistance: 100f, layerMask: TerrainAndBuildingLayerMask);
            if (casted) return hit;
            else return null;
        }

        public virtual void OnUpdate()
        {
            
        }
    }
}