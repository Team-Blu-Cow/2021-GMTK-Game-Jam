using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JUtil.Grids;

public class TestScript : MonoBehaviour
{
    InputMaster input;
    public Vector3[] path;

    public Transform[] positions;

    [SerializeField] private GridTestScript gridTest;

    private void Awake()
    {
        input = new InputMaster();
        input.testmap.Space.performed += ctx => FindPath();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    public void FindPath()
    {
        if (positions == null || positions[0] == null || positions[1] == null)
            return;

        path = gridTest.GetPath(positions[0].position, positions[1].position);
    }

    private void OnDrawGizmos()
    {
        if (path != null && path.Length > 1)
        {
            Gizmos.color = Color.black;

            Vector3 pos = gridTest.Grid(0).ToWorld(gridTest.Grid(0).WorldToGrid(positions[0].position));
            Gizmos.DrawCube(pos, Vector3.one * 0.125f);
            Gizmos.DrawLine(pos, path[0]);

            Gizmos.DrawCube(path[0], Vector3.one * 0.125f);

            for (int i = 1; i < path.Length; i++)
            {
                Gizmos.DrawCube(path[i], Vector3.one * 0.125f);
                Gizmos.DrawLine(path[i - 1], path[i]);
            }
        }
    }

}
