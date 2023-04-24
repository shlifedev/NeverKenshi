using UnityEngine;

namespace Kenshi
{
    public interface IBuildingConstructController
    {         
        RaycastHit? RaycastAbovePoint(Vector2 screenPos);
        void OnUpdate(); 
    }
}