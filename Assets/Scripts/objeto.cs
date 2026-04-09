using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class objeto : MonoBehaviour
{
    //Malla que recibimos por parametro (datos del objeto, vertices y triangulos)
    private Mesh Malla;
    public FileReader fileReader;

    public Vector3 posicion;
    public Vector3 rotacion;
    public Vector3 escalado;

    private GameObject Objeto;
    private Renderer objRenderer;

    public void CrearObjeto(string nombreArchivo, Vector3 Posicion, Vector3 Rotacion, Vector3 Escalado, string ShaderObjeto)
    {

        // guardolos parametros de las variables  
        this.posicion = Posicion;
        this.rotacion = Rotacion;
        this.escalado = Escalado;


        fileReader = GetComponent<FileReader>();
        Malla = fileReader.ProcesarArchivo(nombreArchivo);

        //Creamos un nuevo gameObject para la escena
        Objeto = new GameObject(nombreArchivo);
        Objeto.AddComponent<MeshFilter>();
        Objeto.GetComponent<MeshFilter>().mesh = Malla;
        objRenderer = Objeto.AddComponent<MeshRenderer>();

        Material newMaterial = new Material(Shader.Find(ShaderObjeto));
        objRenderer.material = newMaterial;

    }

    public void Dibujar(Matrix4x4 vistaGlobal, Matrix4x4 proyeccionGlobal)
    {
        // Calculo mi propia matriz (única para este objeto)

        Matrix4x4 modelMatrix = Matrices.CreateModelMatrix(posicion, rotacion, escalado);

        // Pasamos las 3 matrices al shader
        objRenderer.material.SetMatrix("_ModelMatrix", modelMatrix);
        objRenderer.material.SetMatrix("_ViewMatrix", vistaGlobal);
        objRenderer.material.SetMatrix("_ProjectionMatrix", proyeccionGlobal);
    }


}
