using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vector2i = ClipperLib.IntPoint;

public class AwakePolygonClipper : MonoBehaviour, IClip
{
    public DestructibleTerrain terrain;


    private Vector2 clipPosition;

    private PolygonCollider2D polygonCollider2D;

    private void Awake()
    {
        if (polygonCollider2D == null)
            polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    public bool CheckBlockOverlapping(Vector2 p, float size)
    {
        float dx = Mathf.Abs(clipPosition.x - p.x) - 0.5f - size / 2;
        float dy = Mathf.Abs(clipPosition.y - p.y) - 0.5f - size / 2;

        return dx < 0f && dy < 0f;
    }

    public ClipBounds GetBounds()
    {      
        return new ClipBounds
        {
            lowerPoint = new Vector2(clipPosition.x - 0.5f, clipPosition.y - 0.5f),
            upperPoint = new Vector2(clipPosition.x + 0.5f, clipPosition.y + 0.5f)
        };             
    }

    public List<Vector2i> GetVertices()
    {
        List<Vector2i> vertices = new List<Vector2i>();
        for (int i = 0; i < polygonCollider2D.points.Length; i++)
        {
            Vector2i point_i64 =  polygonCollider2D.points[i].ToVector2i();
            vertices.Add(point_i64);
        }
        return vertices;
    }

    void Start()
    {
        Vector2 positionWorldSpace = transform.position;
        clipPosition = positionWorldSpace - terrain.GetPositionOffset();

        terrain.ExecuteClip(this);
    }
}
