using UnityEngine;
using System;

public static class RendererArrayExtension
{
    public static Bounds ComputeBounds(this Renderer[] renderers)
    {
        Bounds bounds = new Bounds();
        for (int ir = 0; ir < renderers.Length; ir++)
        {
            Renderer renderer = renderers[ir];
            if (ir == 0)
                bounds = renderer.bounds;
            else
                bounds.Encapsulate(renderer.bounds);
        }
        return bounds;
    }
}

public class ShowMeshBounds : MonoBehaviour
{
    public Color color_bounds = new Color(1.0f, 1.0f, 0.0f, 0.1f);
    public bool Hierarchical = false;
    public bool Disable = false;

    public void OnDrawGizmos()
    {
        Bounds b = new Bounds();
        if (Hierarchical)
        {
            Renderer[] r = gameObject.GetComponentsInChildren<Renderer>();
            b = r.ComputeBounds();
        }
        else
        {
            Renderer r = gameObject.GetComponent<Renderer>();
            if (r != null)
            {
                b = r.bounds;
            }
        }
        Gizmos.color = color_bounds;
        Gizmos.DrawCube(b.center, b.size);

#if UNITY_EDITOR
        UnityEditor.Handles.Label(b.center, $"Width: {String.Format("{0:0.00}", b.size.x)} m\nHeight: {String.Format("{0:0.00}", b.size.y)} m\nDepth: {String.Format("{0:0.00}", b.size.z)} m");
#endif
    }
}