using UnityEngine;
using System.Collections;

/**
 * @class   DrawGridScript
 * @brief   Handles drawing of the grid values set up in Level Settings...
 * @author  Eagan
 * @date    2/17/2012
 */
public class DrawGridScript : MonoBehaviour
{
    // Attributes
    public bool DrawGrid = true;
    public bool DrawSubDeviders = true;
    public Color GridColor = new Color(0.20f, 0.74f, 0.27f, 0.50f);
    public Color SubDeviderColor = new Color(0.10f, 0.37f, 0.14f, 0.50f);
    public float GridSize = 10.0f;

    /**
     * @fn  void OnDrawGizmos()
     * @brief   Handles drawing a grid based on our current level attributes...
     * @author  Eagan
     * @date    2/17/2012
     */
    void OnDrawGizmos()
    {
        LevelAttributes currentAttributes = LevelAttributes.GetInstance();
        Rect bounds = currentAttributes.bounds;
        Gizmos.color = GridColor;
        Vector3 lowerLeft = new Vector3(bounds.xMin, bounds.yMax, 0);
        Vector3 upperLeft = new Vector3(bounds.xMin, bounds.yMin, 0);
        Vector3 lowerRight = new Vector3(bounds.xMax, bounds.yMax, 0);
        Vector3 upperRight = new Vector3(bounds.xMax, bounds.yMin, 0);

        Gizmos.DrawLine(lowerLeft, upperLeft);
        Gizmos.DrawLine(upperLeft, upperRight);
        Gizmos.DrawLine(upperRight, lowerRight);
        Gizmos.DrawLine(lowerRight, lowerLeft);

        // Draw horizontal and vertical grids along the z axis
        float zIndex = 0.0f;
        //for (float zIndex = -(GridSize * 2); zIndex < (GridSize * 2); zIndex += GridSize)
        {
            // Draw our horizontal grid
            for (float yIndex = bounds.yMin; yIndex < bounds.yMax; yIndex += GridSize / 2.0f)
            {
                Vector3 from = new Vector3(bounds.xMin, yIndex, zIndex);
                Vector3 to = new Vector3(bounds.xMax, yIndex, zIndex);
                if (yIndex % GridSize == 0.0f)
                {
                    if (DrawGrid == true)
                    {
                        Gizmos.color = GridColor;
                        Gizmos.DrawLine(from, to);
                    }
                }
                else
                {
                    if (DrawSubDeviders == true)
                    {
                        Gizmos.color = SubDeviderColor;
                        Gizmos.DrawLine(from, to);
                    }
                }

            }

            // Draw our vertical grid
            for (float xIndex = bounds.xMin; xIndex < bounds.xMax; xIndex += GridSize / 2.0f)
            {
                Vector3 from = new Vector3(xIndex, bounds.yMin, zIndex);
                Vector3 to = new Vector3(xIndex, bounds.yMax, zIndex);
                if (xIndex % GridSize == 0.0f)
                {
                    if (DrawGrid == true)
                    {
                        Gizmos.color = GridColor;
                        Gizmos.DrawLine(from, to);
                    }
                }
                else
                {
                    if (DrawSubDeviders == true)
                    {
                        Gizmos.color = SubDeviderColor;
                        Gizmos.DrawLine(from, to);
                    }
                }
            }
        }
    }
}
