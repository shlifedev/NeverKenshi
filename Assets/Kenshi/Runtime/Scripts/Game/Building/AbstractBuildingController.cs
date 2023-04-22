using System;
using UnityEngine;

namespace Kenshi
{
    public abstract class AbstractBuildingController : IBuildingConstructController
    {
        public delegate void StateChangeDelegate(BuildingConstructManager.BuildingState prev,
            BuildingConstructManager.BuildingState current);

        public StateChangeDelegate onStateChangeDelegate;
        public BuildingConstructManager.BuildingState State { get; set; }
        public BuildingConstructManager Instance
        {
            get
            {
                return BuildingConstructManager.Instance;
            }
        }
        
        public Ray ScreenToRay()
        {
            throw new NotImplementedException();
        }
    }
}