using UnityEngine;

namespace Kenshi
{
    public abstract class KenshiBehaviour : MonoBehaviour
    {
        public Vector3 GetXZVector3() => new Vector3(transform.position.x, 0, transform.position.z);
        public Vector2 GetXZVector2() => new Vector2(transform.position.x, transform.position.z);
    }
}