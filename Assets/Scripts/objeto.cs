using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class objeto : MonoBehaviour
{
    //Malla que recibimos por parametro (datos del objeto, vertices y triangulos)
    private Mesh Malla;
    public FileReader fileReader;

    //Datos para las matrices de transformación 
    public Vector3 posicion;
    public Vector3 rotacion;
    public Vector3 escalado;

    private GameObject Objeto;

    public void CrearObjeto(string nombreArchivo, Vector3 Posicion, Vector3 Rotacion, Vector3 Escalado, string ShaderObjeto)
    {
        fileReader = GetComponent<FileReader>();
        Malla = fileReader.ProcesarArchivo(nombreArchivo);

        //Creamos un nuevo gameObject para la escena
        Objeto = new GameObject(nombreArchivo);
        Objeto.AddComponent<MeshFilter>();
        Objeto.GetComponent<MeshFilter>().mesh = Malla;
        Objeto.AddComponent<MeshRenderer>();

        Material newMaterial = new Material(Shader.Find(ShaderObjeto));

        Matrix4x4 ModeloMatriz = Matrices.CreateModelMatrix(Posicion, Rotacion, Escalado);
        Objeto.GetComponent<Renderer>().material.SetMatrix("_ModelMatrix", ModeloMatriz);

        Objeto.GetComponent<MeshRenderer>().material = newMaterial;

        RecalcularMatrices();

    }

    public void Dibujar(Matrix4x4 vistaGlobal, Matrix4x4 proyeccionGlobal)
    {
        // Calculo mi propia matriz (única para este objeto)
        Matrix4x4 modelMatrix = Matrices.CreateModelMatrix(posicion, rotacion, escalado);

        Renderer r = GetComponent<Renderer>();

        // Seteo mi matriz única
        r.material.SetMatrix("_ModelMatrix", modelMatrix);

        // Seteo las matrices que me pasó el Manager (que son las mismas para todos)
        r.material.SetMatrix("_ViewMatrix", vistaGlobal);
        r.material.SetMatrix("_ProjectionMatrix", proyeccionGlobal);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
