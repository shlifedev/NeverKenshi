using UnityEngine;
namespace Kenshi
{
    public interface IEntity
    {

    }

    public interface IMoveable
    {
        void MoveToDirection(Vector3 direction);
    }
}