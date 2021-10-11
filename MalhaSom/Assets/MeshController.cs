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
    //private Mesh originalMesh;
    //private Color color;

    private Vector3[] vertices, modifiedVertices, originalVertices;

    public AudioSource audioSource;

    float[] spectrum = new float[256];

    int controleSpectrum;

    float tempo;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponentInChildren<MeshFilter>().mesh;
        vertices = mesh.vertices;
        originalVertices = (Vector3[])vertices.Clone();
        modifiedVertices = (Vector3[])vertices.Clone();
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        controleSpectrum = 0;
        //color = GetComponentInChildren<Material>().color;
    }

    void RecalculateMesh()
    {
        mesh.vertices = vertices;
        //GetComponentInChildren<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
        //modifiedVertices = originalVertices;
    }

    // Update is called once per frame
    void Update()
    {
        tempo += Time.deltaTime;
        Debug.Log(Time.deltaTime);
        if (tempo > 0.5f)
        {
            vertices = (Vector3[])originalVertices.Clone();

            //float teste = spectrum[100];
            for (int i = 0; i < vertices.Length; i++)
            {
                if (i < vertices.Length / 3)
                {
                    vertices[i].x *= (spectrum[controleSpectrum] * 2000000000) + 1;
                    //color.r *= (spectrum[controleSpectrum]);
                }
                else if (i < (3 * vertices.Length) / 2)
                {
                    vertices[i].y *= (spectrum[controleSpectrum] * 1500000000) + 1;
                    //color.g *= (spectrum[controleSpectrum]);
                }
                else
                    vertices[i].z *= (spectrum[controleSpectrum] * 1000000000) + 1;
                //color.b *= (spectrum[controleSpectrum]);

            }

            //mesh.vertices = vertices;

            Debug.Log(spectrum[controleSpectrum]);
            Debug.Log(controleSpectrum);

            controleSpectrum++;
            if (controleSpectrum >= 255)
            {
                controleSpectrum = 0;
            }

            RecalculateMesh();
            tempo = 0;
        }

        
    }
}
