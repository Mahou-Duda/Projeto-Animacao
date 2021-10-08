using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshController : MonoBehaviour
{
    //[Range(1.5f, 5f)]
    //public float radius = 2f;

    //[Range(0.5f, 5f)]
    //public float deformationStrenght = 2f;

    private Mesh mesh;
    private Mesh originalMesh;

    private Vector3[] vertices, modifiedVertices, originalVertices;

    public AudioSource audioSource;

    float[] spectrum = new float[256];

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponentInChildren<MeshFilter>().mesh;
        originalMesh = GetComponentInChildren<MeshFilter>().mesh;
        Debug.Log(mesh.vertexCount);
        vertices = mesh.vertices;
        modifiedVertices = mesh.vertices;
        originalVertices = vertices;
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
    }

    void RecalculateMesh()
    {
        mesh.vertices = modifiedVertices;
        //GetComponentInChildren<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
        modifiedVertices = originalVertices;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < spectrum.Length; i++)
        {
            for (int v = 0; v < modifiedVertices.Length; v++)
            {
                float smoothFactor = 200f;
                float force = 0.2f;
                Vector3 teste;
                teste = vertices[v] * spectrum[i];
                //modifiedVertices[v] = modifiedVertices[v] * force / smoothFactor;
                modifiedVertices[v] = modifiedVertices[v] *0.5f; 
            }
        }

        RecalculateMesh();
    }
}
