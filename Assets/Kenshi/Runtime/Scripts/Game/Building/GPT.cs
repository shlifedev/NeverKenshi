using UnityEditor;
using UnityEngine;

namespace Kenshi
{
    public class GPT : MonoBehaviour
    {
        public Transform startPoint;
        public Transform controlPoint1;
        public Transform controlPoint2;
        public Transform endPoint;
        public float duration = 0.3f;
        private float timer = 0f;

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer <= duration)
            {
                float t = timer / duration;
                Vector3 pos = CalculateBezierPoint(t, startPoint.position, controlPoint1.position,
                    controlPoint2.position, endPoint.position);
                transform.position = pos;
            }
        }



        private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 p = uuu * p0;
            p += 3 * uu * t * p1;
            p += 3 * u * tt * p2;
            p += ttt * p3;

            return p;
        }


         
        
        private void OnDrawGizmosSelected()
{
    // Draw the control points as handles
    Gizmos.color = Color.green;
    controlPoint1.position = Handles.PositionHandle(controlPoint1.position, Quaternion.identity);
    Gizmos.DrawWireCube(controlPoint1.position, new Vector3(0.2f, 0.2f, 0.2f));

    Gizmos.color = Color.blue;
    controlPoint2.position = Handles.PositionHandle(controlPoint2.position, Quaternion.identity);
    Gizmos.DrawWireCube(controlPoint2.position, new Vector3(0.2f, 0.2f, 0.2f));

    // Draw the Bezier curve
    Gizmos.color = Color.yellow;
    Gizmos.DrawLine(startPoint.position, controlPoint1.position);

    Gizmos.color = Color.green;
    Gizmos.DrawLine(controlPoint1.position, controlPoint2.position);

    Gizmos.color = Color.blue;
    Gizmos.DrawLine(controlPoint2.position, endPoint.position);

    Vector3 previous = CalculateBezierPoint(0f, startPoint.position, controlPoint1.position, controlPoint2.position, endPoint.position);
    float resolution = 0.0625f; // 1/16
    for (float i = resolution; i <= 1f; i += resolution)
    {
        Vector3 current = CalculateBezierPoint(i, startPoint.position, controlPoint1.position, controlPoint2.position, endPoint.position);
        Gizmos.DrawLine(previous, current);
        previous = current;
    }

    // Draw the points
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(startPoint.position, 0.1f);
    Gizmos.DrawWireSphere(controlPoint1.position, 0.1f);
    Gizmos.DrawWireSphere(controlPoint2.position, 0.1f);
    Gizmos.DrawWireSphere(endPoint.position, 0.1f);

    // Draw the white points between the start and end points
    Gizmos.color = Color.white;
    previous = startPoint.position;
    for (float i = resolution; i <= 1f; i += resolution)
    {
        Vector3 current = CalculateBezierPoint(i, startPoint.position, controlPoint1.position, controlPoint2.position, endPoint.position);
        Gizmos.DrawSphere(current, 0.05f);
        Gizmos.DrawLine(current, current + new Vector3(0f, 3f, 0f));
        Vector3 next = current;

        Vector3 previous2 = current + new Vector3(0f, 3f, 0f);
        float resolution2 = 0.2f;
        for (float j = resolution2; j <= 1f; j += resolution2)
        {
            Vector3 current2 = CalculateBezierPoint(j, current + new Vector3(0f, 3f, 0f), next, next, current + new Vector3(0f, 3f, 0f));
            Gizmos.DrawLine(previous2, current2);
            previous2 = current2;
        }

        previous = current;
    }
}
        
        
    private void OnDrawGizmos()
    {
        return;
    // Draw the Bezier curve
    Gizmos.color = Color.yellow;
    Gizmos.DrawLine(startPoint.position, controlPoint1.position);

    Gizmos.color = Color.green;
    Gizmos.DrawLine(controlPoint1.position, controlPoint2.position);

    Gizmos.color = Color.blue;
    Gizmos.DrawLine(controlPoint2.position, endPoint.position);

    Vector3 previous = CalculateBezierPoint(0f, startPoint.position, controlPoint1.position, controlPoint2.position, endPoint.position);
    float resolution = 0.0625f; // 1/16
    for (float i = resolution; i <= 1f; i += resolution)
    {
        Vector3 current = CalculateBezierPoint(i, startPoint.position, controlPoint1.position, controlPoint2.position, endPoint.position);
        Gizmos.DrawLine(previous, current);
        previous = current;
    }

    // Draw the points
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(startPoint.position, 0.1f);
    Gizmos.DrawWireSphere(controlPoint1.position, 0.1f);
    Gizmos.DrawWireSphere(controlPoint2.position, 0.1f);
    Gizmos.DrawWireSphere(endPoint.position, 0.1f);

    // Draw the control point boxes
    Gizmos.color = Color.green;
    Gizmos.DrawWireCube(controlPoint1.position, new Vector3(0.2f, 0.2f, 0.2f));
    Gizmos.color = Color.blue;
    Gizmos.DrawWireCube(controlPoint2.position, new Vector3(0.2f, 0.2f, 0.2f));

    // Draw the white points between the start and end points
    Gizmos.color = Color.white;
    previous = startPoint.position;
    for (float i = resolution; i <= 1f; i += resolution)
    {
        Vector3 current = CalculateBezierPoint(i, startPoint.position, controlPoint1.position, controlPoint2.position, endPoint.position);
        
        // Draw the line and calculate the next point
        Gizmos.DrawSphere(current, 0.05f);
        Gizmos.DrawLine(current, current + new Vector3(0f, 3f, 0f));
        Vector3 next = current + new Vector3(3, 10, 0);

        // Draw the curve
        Vector3 previous2 = current + new Vector3(0f, 3f, 0f);
        float resolution2 = 0.2f;
        for (float j = resolution2; j <= 1f; j += resolution2)
        {
            Vector3 current2 = CalculateBezierPoint(j, current + new Vector3(0f, 3f, 0f), next, next, current + new Vector3(0f, 3f, 0f));
            Gizmos.DrawLine(previous2, current2);
            previous2 = current2;
        }

        previous = current;
    }
}

    }
}