using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class ObjetoDeLaEscena
{
    //Malla que recibimos por parametro (datos del objeto, vertices y triangulos)
    private Mesh Malla ;
    private UnityEngine.Color[] colores ;

    public Vector3 posicion;
    public Vector3 rotacion;
    public Vector3 escalado;

    private GameObject objeto_game_object;
    private Renderer objRenderer;

    private FileReader fileReader ;

    public void CrearObjeto(string nombreArchivo, Vector3 Posicion, Vector3 Rotacion, Vector3 Escalado, UnityEngine.Color ComponentesColorRGB)
    {

        // guardolos parametros de las variables  
        this.posicion = Posicion;
        this.rotacion = Rotacion;
        this.escalado = Escalado;

        Malla = fileReader.ProcesarArchivo(nombreArchivo);

        colores = new UnityEngine.Color[Malla.vertices.Length] ;

        for (int i = 0 ; i < Malla.vertices.Length ; i++)
        {
            colores[i] = ComponentesColorRGB ;
        }

        //Creamos un nuevo gameObject para la escena
        objeto_game_object = new GameObject(nombreArchivo);
        objeto_game_object.AddComponent<MeshFilter>();
        objeto_game_object.GetComponent<MeshFilter>().mesh = Malla;
        objeto_game_object.GetComponent<MeshFilter>().mesh.colors = colores ;
        objRenderer = objeto_game_object.AddComponent<MeshRenderer>();


        Material newMaterial = new Material(Shader.Find("ShaderBasico"));
        objRenderer.material = newMaterial;

    }

    //------------------------------ PARA LAS PAREDES ------------------------------------
    public void CrearObjeto(string nombreArchivo, UnityEngine.Color ComponentesColorRGB)
    {


        this.posicion = Vector3.zero;
        this.rotacion = Vector3.zero;
        this.escalado = new Vector3(1, 1, 1);
        Malla = fileReader.ProcesarArchivo(nombreArchivo);

        colores = new UnityEngine.Color[Malla.vertices.Length] ;

        for (int i = 0 ; i < Malla.vertices.Length ; i++)
        {
            colores[i] = ComponentesColorRGB ;
        }

        //Creamos un nuevo gameObject para la escena
        objeto_game_object = new GameObject(nombreArchivo);
        objeto_game_object.AddComponent<MeshFilter>();
        objeto_game_object.GetComponent<MeshFilter>().mesh = Malla;
        objeto_game_object.GetComponent<MeshFilter>().mesh.colors = colores ;
        objRenderer = objeto_game_object.AddComponent<MeshRenderer>();


        Material newMaterial = new Material(Shader.Find("ShaderBasico"));
        objRenderer.material = newMaterial;
    }

    public void Dibujar(Matrix4x4 vistaGlobal, Matrix4x4 proyeccionGlobal)
    {
        // Calculo mi propia matriz (Unica para este objeto)

        Matrix4x4 modelMatrix = Matrices.CreateModelMatrix(posicion, rotacion, escalado);

        // Pasamos las 3 matrices al shader
        objRenderer.material.SetMatrix("_ModelMatrix", modelMatrix);
        objRenderer.material.SetMatrix("_ViewMatrix", vistaGlobal);
        objRenderer.material.SetMatrix("_ProjectionMatrix", proyeccionGlobal);
    }

    public void SetearFileReader(FileReader fr)
    {
        fileReader = fr ;
    }


}
