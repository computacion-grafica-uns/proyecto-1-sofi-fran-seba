using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    //Arreglo para almacenar vertices
    private Vector3[] vertices ;

    //Arreglo para almacenar indices a los vertices
    private int[] triangles ;

    //Referencia al objetoCuadrado que vamos a crear
    private GameObject Objeto_Suelo ;

    //Objeto Camara
    private GameObject MiCamara ;

    // Start is called before the first frame update
    void Start()
    {
        //Creamos un nuevo objeto de tipo GameObject
        //Todos los objetos que podemos poner en una escena son de tipo GameObject
        Objeto_Suelo = new GameObject() ;
        //A este objeto le agregamos un componente de tipo MeshFilter
        //Este componente maneja mallas (Mesh)
        Objeto_Suelo.AddComponent<MeshFilter>().mesh = new Mesh() ;
        //Agregamos un componente de tipo MeshRenderer
        //El MeshRenderer toma la geometria del MeshFilter y la renderiza en la posicione
        //Definida por el componente Transform del GameObject
        Objeto_Suelo.AddComponent<MeshRenderer>() ; 

        CreateModel() ;
        UpdateMesh() ;
        CreateMaterial() ;

        CreateCamera() ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void CreateMaterial()
    {
        //Creamos un nuevo material que utiliza el shader que le pasemos por parametro
        Material newMaterial = new Material(Shader.Find("Standard")) ;

        //Asignamos el nuevo material al MeshRenderer
        Objeto_Suelo.GetComponent<MeshRenderer>().material = newMaterial ;
    }

    private void CreateCamera()
    {
        MiCamara = new GameObject() ;
        MiCamara.AddComponent<Camera>() ;
        
        //Posicion de la camara (Coordenadas)
        MiCamara.transform.position = new Vector3(0,10,0) ;
        //Posicion de la camara (Angulo)
        MiCamara.transform.rotation = Quaternion.Euler(90,0,0) ; //MIRAR ESTO

        //Decimos que el fondo de la camara sea un color solido
        MiCamara.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor ;

        //Establecemos el color negro
        MiCamara.GetComponent<Camera>().backgroundColor = Color.black ;
    }

    private void CreateModel()
    {
        //arreglo de posiciones de vertices
        vertices = new Vector3[]
        {
            new Vector3(0,0,0), //Vertice 0
            new Vector3(7,0,0), //Vertice 1
            new Vector3(7,0,5), //Vertice 2
            new Vector3(0,0,5), //Vertice 3
            
        } ;

        //arreglo de triangulo o caras
        triangles = new int[]
        {
            0,3,1, //Triangulo 0
            3,2,1  //Triangulo 1
        } ;
    }

    private void UpdateMesh()
    {
        //Actualizamos los vertices de la malla
        Objeto_Suelo.GetComponent<MeshFilter>().mesh.vertices = vertices ;

        //Actualizamos los triangulos de la malla
        Objeto_Suelo.GetComponent<MeshFilter>().mesh.triangles = triangles ;
    }

}
