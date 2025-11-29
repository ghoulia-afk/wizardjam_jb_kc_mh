using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class InstanceFromVolume : MonoBehaviour
{
    [Header("Source")]

    [SerializeField]
    Mesh mesh;

    [SerializeField]
    Material material;

    [Header("Source Parameters")]

    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    Vector2 instanceSizes = new(0.8f, 1.2f);

    [SerializeField]
    UnityEngine.Rendering.ShadowCastingMode shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

    [SerializeField]
    bool receiveShadows = true;

    [SerializeField]
    string layer = "Default";

    [SerializeField]
    bool orientToSurfaceNormal = true;

    [SerializeField]
    Vector2 instanceRotations = new(0f, 360f);

    [SerializeField]
    [Range(1, 1023)]
    int numberOfInstances = 1023;

    // Internals

    Matrix4x4[] matrices;
    MaterialPropertyBlock materialPropertyBlock;
    int attempts = 9999;
    int spawnedInstances = 0;
    RenderParams renderParams;
    Bounds renderBounds;

    void Start()
    {
        Compute();
    }

    void OnValidate()
    {
        Compute();
    }

    void Compute()
    {

        renderBounds = new Bounds(transform.position, Vector3.one);

        GenerateMatrix();
        GenerateLightprobeData();
        renderParams = new();
        renderParams.camera = null;
        renderParams.layer = LayerMask.NameToLayer(layer);
        renderParams.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.CustomProvided;
        renderParams.lightProbeProxyVolume = null;
        renderParams.material = material;
        renderParams.matProps = materialPropertyBlock;
        renderParams.motionVectorMode = MotionVectorGenerationMode.Camera;
        renderParams.receiveShadows = receiveShadows;
        renderParams.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.BlendProbesAndSkybox;
        renderParams.shadowCastingMode = shadowCastingMode;
        renderParams.worldBounds = renderBounds;
    }

    void GenerateLightprobeData()
    {
      

        float[] hueShift = new float[numberOfInstances];
        Vector3[] positions = new Vector3[numberOfInstances];

        for (int i = 0; i < numberOfInstances; i++)
        {
            hueShift[i] = Random.Range(-0.05f, 0f);
            positions[i] = matrices[i].GetPosition() + (Vector3.up * 0.5f);
        //    renderBounds.Encapsulate(  matrices[i].GetPosition());
        }

        var lightprobes = new UnityEngine.Rendering.SphericalHarmonicsL2[numberOfInstances];
        var occlusionprobes = new Vector4[numberOfInstances];
        LightProbes.CalculateInterpolatedLightAndOcclusionProbes(positions, lightprobes, occlusionprobes);

        materialPropertyBlock = new MaterialPropertyBlock();
        materialPropertyBlock.CopySHCoefficientArraysFrom(lightprobes);
        materialPropertyBlock.CopyProbeOcclusionArrayFrom(occlusionprobes);
        //    materialPropertyBlock.SetFloatArray("_HueShift", hueShift);

    }

    void GenerateMatrix()
    {
        renderBounds = new Bounds(transform.position, Vector3.one);


        attempts = 9999;
        spawnedInstances = 0;
        matrices = new Matrix4x4[numberOfInstances];

        for (int i = 0; i < numberOfInstances; i++)
        {
            Vector3 sproutPosition = transform.position + (transform.right * Random.Range(-transform.localScale.x * 0.5f, transform.localScale.x * 0.5f))
                                                                     + (transform.up * Random.Range(-transform.localScale.y * 0.5f, transform.localScale.y * 0.5f))
                                                                     + (transform.forward * Random.Range(-transform.localScale.z * 0.5f, transform.localScale.z * 0.5f));

            attempts--;
            if (attempts <= 0)
            {
                Debug.LogWarning("Ran out of attempts populating " + gameObject.name);
                Destroy(this.gameObject);
                return;
            }

            if (Physics.Raycast(sproutPosition, Vector3.down, out RaycastHit hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
            {
                Quaternion instanceRotation;
                if (orientToSurfaceNormal)
                {
                    instanceRotation = Quaternion.LookRotation(Random.insideUnitSphere, hit.normal);
                }
                else
                {
                    instanceRotation = Quaternion.Euler(0f, Random.Range(instanceRotations.x, instanceRotations.y), 0f);
                }

                renderBounds.Encapsulate(hit.point);
                matrices[spawnedInstances] = Matrix4x4.TRS(hit.point, instanceRotation, Vector3.one * Random.Range(instanceSizes.x, instanceSizes.y));
                spawnedInstances++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.hasChanged)
        {
            Compute();
            transform.hasChanged = false;
        }

        if (matrices.Length > 0 && materialPropertyBlock != null)
        {

            Graphics.RenderMeshInstanced(renderParams, mesh, 0, matrices);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 1f, 0.1f);
           Gizmos.matrix = transform.localToWorldMatrix;
           Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        //   Gizmos.DrawCube(Vector3.zero, Vector3.one);


        Gizmos.DrawWireCube(renderBounds.center, renderBounds.size);


 
    }
}
