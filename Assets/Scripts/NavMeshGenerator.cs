using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshGenerator : MonoBehaviour
{

    public List<NavMeshSurface> surfaces;
    // Start is called before the first frame update


    [ContextMenu("Generate")]
    public void Generate()
    {

        foreach (NavMeshSurface surface in surfaces)
        {
            surface.BuildNavMesh();
        }
    }
}
