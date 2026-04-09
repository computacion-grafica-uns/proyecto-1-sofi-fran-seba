using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pared1_banio : MonoBehaviour
{
    //Arreglo para almacenar vertices
    private Vector3[] vertices;

    //Arreglo para almacenar indices a los vertices
    private int[] triangles;

    //Referencia al objetoCuadrado que vamos a crear
    private GameObject Pared1b;

    //Objeto Camara
    private GameObject MiCamara;
    // Start is called before the first frame update
    void Start()
    {
        //Creamos un nuevo objeto de tipo GameObject
        //Todos los objetos que podemos poner en una escena son de tipo GameObject
        Pared1b = new GameObject();
        //A este objeto le agregamos un componente de tipo MeshFilter
        //Este componente maneja mallas (Mesh)
        Pared1b.AddComponent<MeshFilter>().mesh = new Mesh();
        //Agregamos un componente de tipo MeshRenderer
        //El MeshRenderer toma la geometria del MeshFilter y la renderiza en la posicione
        //Definida por el componente Transform del GameObject
        Pared1b.AddComponent<MeshRenderer>();

        CreateModel();
        UpdateMesh();
        CreateMaterial();

        CreateCamera();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateMaterial()
    {
        //Creamos un nuevo material que utiliza el shader que le pasemos por parametro
        Material newMaterial = new Material(Shader.Find("Standard"));

        //Asignamos el nuevo material al MeshRenderer
        Pared1b.GetComponent<MeshRenderer>().material = newMaterial;
    }

    private void CreateCamera()
    {
        MiCamara = new GameObject();
        MiCamara.AddComponent<Camera>();

        //Posicion de la camara (Coordenadas)
        MiCamara.transform.position = new Vector3(0, 10, 0);
        //Posicion de la camara (Angulo)
        MiCamara.transform.rotation = Quaternion.Euler(90, 0, 0); //MIRAR ESTO

        //Decimos que el fondo de la camara sea un color solido
        MiCamara.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;

        //Establecemos el color negro
        MiCamara.GetComponent<Camera>().backgroundColor = Color.black;
    }

    private void CreateModel()
    {
        //arreglo de posiciones de vertices
        vertices = new Vector3[]
        {
            new Vector3(0,0,2), //Vertice 0
            new Vector3(0,2.5f,2), //Vertice 1
            new Vector3(0,2.5f,3), //Vertice 2
            new Vector3(0,0,3), //Vertice 3
            
            new Vector3(0,2,3), //Vertice 4
            new Vector3(0,2,4), //Vertice 5
            new Vector3(0,0,4), //Vertice 6

            new Vector3(0,2.5f,4), //Vertice 7
            new Vector3(0,2.5f,5), //Vertice 8
            new Vector3(0,0,5), //Vertice 9
          
        };

        //arreglo de triangulo o caras
        triangles = new int[]
        {
            0,2,1, //Triangulo 1
            0,3,2,  //Triangulo 2
            3,5,4,//triangulo 3
            3,6,5,//triangulo 4
            6,8,7,//traingulo 5
            6,9,8,//triangulo 6

            //invertidos
            0,1,2,
            0,2,3,
            3,4,5,
            3,5,6,
             6,7,8,
            6,8,9

        };
    }

    private void UpdateMesh()
    {
        //Actualizamos los vertices de la malla
        Pared1b.GetComponent<MeshFilter>().mesh.vertices = vertices;

        //Actualizamos los triangulos de la malla
        Pared1b.GetComponent<MeshFilter>().mesh.triangles = triangles;
    }

}

