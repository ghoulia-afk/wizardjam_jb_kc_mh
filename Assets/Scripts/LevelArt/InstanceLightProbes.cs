using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceLightProbes : MonoBehaviour
{
#if UNITY_EDITOR

    [SerializeField]
    MeshFilter destinationSurface;

    [SerializeField]
    int numberOfProbes = 1000;


    [SerializeField]
    bool generate = false;

    // Internals

    LightProbeGroup lightProbeGroup;


    void OnValidate()
    {
        if (generate)
        {
            Generate();
        }
    }

    void Generate()
    {
        lightProbeGroup = GetComponent<LightProbeGroup>();

        Mesh destinationMesh = destinationSurface.sharedMesh;

        int[] triangles = destinationMesh.triangles;
        Vector3[] vertices = destinationMesh.vertices;
        Vector3[] normals = destinationMesh.normals;

        List<Vector3> probePositions = new();

        for (int i = 0; i < numberOfProbes; i++)
        {
            int triangleIndex = 3 * Random.Range(0, (triangles.Length / 3) - 3);

            float randomA = Random.value;
            float randomB = Random.value;

            Vector3 finalPosition = Vector3.Lerp(vertices[triangles[triangleIndex]], Vector3.Lerp(vertices[triangles[triangleIndex + 1]], vertices[triangles[triangleIndex + 2]], randomA), randomB);
            Vector3 finalNormal = Vector3.Lerp(normals[triangles[triangleIndex]], Vector3.Lerp(normals[triangles[triangleIndex + 1]], normals[triangles[triangleIndex + 2]], randomA), randomB);

            probePositions.Add(finalPosition + (finalNormal * 0.5f));
            probePositions.Add(finalPosition + (finalNormal * 1.5f));

            lightProbeGroup.probePositions = probePositions.ToArray();

            generate = false;
        }
    }

#endif
}
