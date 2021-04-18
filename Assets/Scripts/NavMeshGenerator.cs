using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshGenerator : MonoBehaviour
{

    public List<NavMeshSurface> surfaces;


    [ContextMenu("Generate")]
    public void Generate()
    {

        foreach (NavMeshSurface surface in surfaces)
        {
            surface.BuildNavMesh();
        }
    }
}
