using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using Color = UnityEngine.Color;

public class GizmosDrawing : MonoBehaviour
{
    // The gizmo points that define the Bezier curve
    public Transform[] gizmoPoints;

    // The speed of the scroll effect
    public float scrollSpeed = 0.5f;

    // The color of the lines
    public Color lineColor = Color.white;

    // The points that make up each line
    private Vector3[] linePoints = new Vector3[16];

    private void OnDrawGizmos()
    {
        // Set the color of the gizmos and lines
        Gizmos.color = lineColor;

        // Iterate over each Bezier curve segment defined by four gizmo points
        for (int i = 0; i < gizmoPoints.Length - 2; i += 2) 
        {
            // Calculate the 16 points that make up the current Bezier curve segment
            for (int j = 0; j < 16; j++)
            {
                // Calculate the Bezier curve point at the current t value
                float t = (float)j / 15f;
                linePoints[j] = CalculateBezierPoint(t, gizmoPoints[i].position, gizmoPoints[i + 1].position, gizmoPoints[i + 2].position);

                // Add a scroll effect to the y position of the point
                linePoints[j].y += Mathf.Sin(Time.time * scrollSpeed + j) * 0.1f;
                
                Gizmos.DrawLine(linePoints[j], linePoints[j] + Vector3.down);
            }

            // Draw the lines between the 16 points
            for (int k = 0; k < linePoints.Length - 1; k++)
            {
                Gizmos.DrawLine(linePoints[k], linePoints[k + 1]);
            }
        }
    }

    // Calculate the position of a point on a Bezier curve given four control points
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u; 

        Vector3 p = uu * p0;
        p += 3f * uu * t * p1;
        p += 3f * u * tt * p2; 

        return p;
    }
}