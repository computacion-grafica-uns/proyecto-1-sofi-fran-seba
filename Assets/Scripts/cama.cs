

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActividadCama : MonoBehaviour
{
    public FileReader fileReader;


    Vector3[] vertices;
    int[] triangulos;
    private GameObject MiCamara;
    private GameObject cama;
    private Color[] colores;


    Vector3 posModel = new Vector3(4, 1, 2);
    Vector3 rotModel = new Vector3(0, 0, 0);
    Vector3 scaleModel = new Vector3(0.01f, 0.01f, 0.01f); 

    float fov = 90f;
    float aspectRatio = 16f / 9f;
    float nearClipPlane = 0.1f;
    float farClipPlane = 1000f;


    void Start()
    {
        fileReader = GetComponent<FileReader>(); //puse esta linea y aparecio la cama!!!
        fileReader.ProcesarArchivo("Bed1");
        vertices = fileReader.vertices;
        triangulos = fileReader.triangles;

        cama = this.gameObject; 
        cama.AddComponent<MeshFilter>(); 
        cama.GetComponent<MeshFilter>().mesh = new Mesh(); 
        cama.AddComponent<MeshRenderer>();

        CreateModel();
        UpdateMesh();
        CreateMaterial();
        RecalcularMatrices();
        

        CreateCamera();
    }


    // Update is called once per frame
    void Update()
    {


    }

    private Vector3 CalcularCentro() //para centarr la cama en el origen, calculo el centro con vertice mas lejano y mas cercano y promedio
    {
        Vector3 min = vertices[0];
        Vector3 max = vertices[0];

        for (int i = 1; i < vertices.Length; i++)
        {
            min = Vector3.Min(min, vertices[i]);
            max = Vector3.Max(max, vertices[i]);
        }
        return (min + max) / 2f;
    }

    private void CreateModel()
    {
        colores = new Color[vertices.Length];

        Vector3 centro = CalcularCentro();

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = vertices[i] - centro; // traslado la malla
            colores[i] = Color.white;
        }
    }


    private void UpdateMesh() // establece los vértices y triángulos del MeshFilter del objetoCuadrado
    {
        cama.GetComponent<MeshFilter>().mesh.vertices = vertices;
        cama.GetComponent<MeshFilter>().mesh.triangles = triangulos;
        cama.GetComponent<MeshFilter>().mesh.colors = colores;
    }

    private void CreateMaterial()
    {
        Material newMaterial = new Material(Shader.Find("ShaderBasico"));

        Matrix4x4 modelMatrix = Matrices.CreateModelMatrix(posModel, rotModel, scaleModel);
        cama.GetComponent<Renderer>().material.SetMatrix("_ModelMatrix", modelMatrix);

        cama.GetComponent<MeshRenderer>().material = newMaterial;
    }


    private void RecalcularMatrices()
    {
        

        Vector3 pos = new Vector3(0, 40, -100);
        Vector3 target = new Vector3(0, 0, 0);
        Vector3 up = new Vector3(0, 0, 1);

        Matrix4x4 ViewMatrix = Matrices.CreateViewMatrix(pos, target, up);
        cama.GetComponent<Renderer>().material.SetMatrix("_ViewMatrix", ViewMatrix);

        Matrix4x4 projectionMatrix = Matrices.CalculatePerspectiveProjectionMatrix(fov, aspectRatio, nearClipPlane, farClipPlane);
        Matrix4x4 gpuProjection = GL.GetGPUProjectionMatrix(projectionMatrix, true); //convertir para la gpu
        cama.GetComponent<Renderer>().material.SetMatrix("_ProjectionMatrix", gpuProjection);
    }


    private void CreateCamera()
    {
        MiCamara = new GameObject();
        MiCamara.AddComponent<Camera>();

        //ahora estas lineas no ya que va a definir lo que veo mi matriz de vista
        // MiCamara.transform.position = new Vector3(0, 40, 100);
        // MiCamara.transform.rotation = Quaternion.Euler(20, 180, 0);

        MiCamara.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        MiCamara.GetComponent<Camera>().backgroundColor = Color.black;
    }


}