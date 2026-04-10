using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class SceneManagerProy : MonoBehaviour
{
    private List<ObjetoDeLaEscena> misObjetos = new List<ObjetoDeLaEscena>();

    float fov = 60f;
    float aspectRatio = 16f / 9f;
    float nearClipPlane = 0.1f;
    float farClipPlane = 1000f;
    private ControladorCamras cc; 
    private GameObject miCamara;
    void Start()
    {
        miCamara = new GameObject();

        //esto no vamas 
        miCamara.AddComponent<Camera>();

        miCamara.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        //establecemos el color negro
        miCamara.GetComponent<Camera>().backgroundColor = UnityEngine.Color.black;
        cc = new ControladorCamras();

        //pruebo con la cama:
        /*ObjetoDeLaEscena cama = new ObjetoDeLaEscena() ;
        cama.SetearFileReader(GetComponent<FileReader>());
        cama.CrearObjeto("Bed1", new Vector3(0, 0, 10), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Color.green);
        misObjetos.Add(cama); 

        //pruebo una mesa
        ObjetoDeLaEscena mesa = new ObjetoDeLaEscena() ;
        mesa.SetearFileReader(GetComponent<FileReader>());
        mesa.CrearObjeto("Table", new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(10, 10, 10), Color.white);
        misObjetos.Add(mesa);*/

        ObjetoDeLaEscena pared1_banio = new ObjetoDeLaEscena() ;
        pared1_banio.SetearFileReader(GetComponent<FileReader>()) ;
        pared1_banio.CrearObjeto("Pared1_banio",Color.white) ;
        misObjetos.Add(pared1_banio) ;

    }

    void Update()
    {
        // calculamos las matrices GLOBALES una sola vez
       // Vector3 posCam = new Vector3(0, 40, -100);
        //Vector3 target = Vector3.zero;
        //Vector3 up = Vector3.up;

        //aca llamamos a actualizar camara clase

        cc.ProcesarInput();
        Matrix4x4 viewMat = cc.ObtenerMatrizVista();

        Matrix4x4 projMat = GL.GetGPUProjectionMatrix(
        Matrices.CalculatePerspectiveProjectionMatrix(fov, aspectRatio, nearClipPlane, farClipPlane),
       true
       );

        // se las pasamos a cada objeto de la lista
        foreach (ObjetoDeLaEscena obj in misObjetos)
        {
            obj.Dibujar(viewMat, projMat);
        }
    }
}

