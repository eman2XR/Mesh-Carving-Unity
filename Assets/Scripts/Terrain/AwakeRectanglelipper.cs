using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vector2i = ClipperLib.IntPoint;

public class AwakeRectanglelipper : MonoBehaviour, IClip
{
    public DestructibleTerrain terrain;

    public float sizeX=2f;
    public float sizeY=1f;

    private Vector2 clipPosition;

    public bool CheckBlockOverlapping(Vector2 p, float size)
    {
        float dx = Mathf.Abs(clipPosition.x - p.x) - sizeX - size / 2;
        float dy = Mathf.Abs(clipPosition.y - p.y) - sizeY - size / 2;

        return dx < 0f && dy < 0f;
    }

    public ClipBounds GetBounds()
    {
        return new ClipBounds
        {
            lowerPoint = new Vector2(clipPosition.x - sizeX, clipPosition.y - sizeY),
            upperPoint = new Vector2(clipPosition.x + sizeX, clipPosition.y + sizeY)
        };
    }

    public List<Vector2i> GetVertices()
    {
        List<Vector2i> vertices = new List<Vector2i>();

        Vector2 point = clipPosition - Vector2.right*sizeX- Vector2.up * sizeY;
        vertices.Add(point.ToVector2i());

        point = clipPosition - Vector2.right * sizeX + Vector2.up * sizeY;
        vertices.Add(point.ToVector2i());

        point = clipPosition + Vector2.right * sizeX + Vector2.up * sizeY;
        vertices.Add(point.ToVector2i());

        point = clipPosition + Vector2.right * sizeX - Vector2.up * sizeY;
        vertices.Add(point.ToVector2i());

        return vertices;
    }

    void Start()
    {
        Vector2 positionWorldSpace = transform.position;
        clipPosition = positionWorldSpace - terrain.GetPositionOffset();

        terrain.ExecuteClip(this);
    }
}
