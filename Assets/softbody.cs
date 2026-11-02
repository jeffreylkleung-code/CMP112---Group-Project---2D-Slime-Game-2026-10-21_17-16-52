// code learned from its IT wala how to create soft body physics in unity
using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class softbody : MonoBehaviour
{
    [Range(0.0f, 2.0f)]
    public float softness = 1f;

    [Range(0.0f, 2.0f)]
    public float damping= 1f;

    public float stiffness = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateSoftBodyPhysics();
    }

    void CreateSoftBodyPhysics()
    {
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        if (skinnedMeshRenderer == null)
        {
            Debug.Log("Uh oh :(");
            return;
        }
        Cloth cloth = gameObject.AddComponent<Cloth>();
        cloth.damping = damping;
        cloth.bendingStiffness = stiffness;
        cloth.coefficients = GenerateClothCoefficients(skinnedMeshRenderer.sharedMesh.vertices.Length);
    }

    private ClothSkinningCoefficient[] GenerateClothCoefficients(int vertexCount)
    {
        ClothSkinningCoefficient[] coefficients = new ClothSkinningCoefficient[vertexCount];

        for (int i = 0; i < vertexCount; i++)
        {
            coefficients[i].maxDistance = softness;
            coefficients[i].collisionSphereDistance = 0f;
        }
        return coefficients;
    }
}
