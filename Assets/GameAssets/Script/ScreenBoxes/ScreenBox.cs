using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBox : MonoBehaviour
{
    public Vector2 minCamPos;
    public Vector2 maxCamPos;
    public bool isScreenBox;
    public Camera mainCamera;

    Vector2[] screenBoxCorners = new Vector2[4];

    void OnDrawGizmos()
    {
        float halfCamHeight = mainCamera.orthographicSize;
        float halfCamWidth = halfCamHeight * mainCamera.aspect;

        Vector2 minSBDimensions = new Vector2(minCamPos.x - halfCamWidth + transform.position.x,
                                              minCamPos.y - halfCamHeight + transform.position.y);

        Vector2 maxSBDimensions = new Vector2(maxCamPos.x + halfCamWidth + transform.position.x,
                                              maxCamPos.y + halfCamHeight + transform.position.y);

        screenBoxCorners[0] = new Vector2(minSBDimensions.x, minSBDimensions.y);
        screenBoxCorners[1] = new Vector2(minSBDimensions.x, maxSBDimensions.y);
        screenBoxCorners[2] = new Vector2(maxSBDimensions.x, maxSBDimensions.y);
        screenBoxCorners[3] = new Vector2(maxSBDimensions.x, minSBDimensions.y);

        Gizmos.color = isScreenBox ? Color.green : Color.red;

        for(int i = 0; i < screenBoxCorners.Length; i++)
        {
            int nextPos = (i + 1) % 4;
            Gizmos.DrawLine(screenBoxCorners[i], screenBoxCorners[nextPos]);
        }
    }
}
