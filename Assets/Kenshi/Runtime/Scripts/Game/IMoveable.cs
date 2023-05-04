using UnityEngine;
namespace Kenshi
{ 

    public interface IMoveable
    {
        Vector3 Position
        {
            get;
            set;
        }
        
        
        void MoveToDirection(Vector3 direction);
    }
}