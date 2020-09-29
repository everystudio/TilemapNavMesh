using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(10)]
public class TestDrawMovePath : MonoBehaviour
{
    [SerializeField] LineRenderer line;

    [SerializeField] Transform startPos, endPos;

    private NavMeshPath path;

    void Awake()
    {
        path = new NavMeshPath();
    }

    void Update()
    {
        if (startPos != null && endPos != null)
        {
            var result = NavMesh.CalculatePath(startPos.position, endPos.position, NavMesh.AllAreas, path);
            enabled = line.enabled = result;
            if (result)
            {
                var corners = path.corners;
                line.positionCount = corners.Length;
                line.SetPositions(corners);
            }
            else
            {
                //Debug.Log(enabled);
            }

        }
    }
}
