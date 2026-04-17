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
        ObjetoDeLaEscena cama = new ObjetoDeLaEscena() ;
        cama.SetearFileReader(GetComponent<FileReader>());
        cama.CrearObjeto("Bed1", new Vector3(-2f, 0, -1.2f), new Vector3(0, 0, 0), new Vector3(0.067f, 0.067f, 0.08f), Color.green);
        misObjetos.Add(cama);
       
        //pruebo una mesa
        ObjetoDeLaEscena mesa = new ObjetoDeLaEscena();
        mesa.SetearFileReader(GetComponent<FileReader>());
        mesa.CrearObjeto("Table", new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0.5f, 0.7f, 0.5f), Color.white);
        misObjetos.Add(mesa);
        //pared PUERTA
        ObjetoDeLaEscena paredPuerta = new ObjetoDeLaEscena();
        paredPuerta.SetearFileReader(GetComponent<FileReader>());
        paredPuerta.CrearObjeto("Pared2", new Vector3(0, 0, 2.5f), new Vector3(0, 180, 0), new Vector3(1, 1, 1), Color.green);
        //pared1_banio.CrearObjeto("Pared1_banio", new Vector3(10, 0, 0), Vector3.zero, Vector3.one, Color.white);
        misObjetos.Add(paredPuerta);
        
        //pared Banio
        ObjetoDeLaEscena pared1_banio = new ObjetoDeLaEscena() ;
        pared1_banio.SetearFileReader(GetComponent<FileReader>()) ;
        pared1_banio.CrearObjeto("Pared1_banio", new Vector3(3.5f, 0, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Color.white);
        //pared1_banio.CrearObjeto("Pared1_banio", new Vector3(10, 0, 0), Vector3.zero, Vector3.one, Color.white);

        misObjetos.Add(pared1_banio) ;

        //piso
        ObjetoDeLaEscena piso = new ObjetoDeLaEscena();
        piso.SetearFileReader(GetComponent<FileReader>());
        piso.CrearObjeto("Piso", new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Color.grey);
        //pared1_banio.CrearObjeto("Pared1_banio", new Vector3(10, 0, 0), Vector3.zero, Vector3.one, Color.white);
        misObjetos.Add(piso);

        //pared1_cocina
        ObjetoDeLaEscena pared1_cocina = new ObjetoDeLaEscena();
        pared1_cocina.SetearFileReader(GetComponent<FileReader>());
        pared1_cocina.CrearObjeto("Pared1_cocina", new Vector3(3.5f, 0, -1.5f), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Color.red);
        //pared1_banio.CrearObjeto("Pared1_banio", new Vector3(10, 0, 0), Vector3.zero, Vector3.one, Color.white);
        misObjetos.Add(pared1_cocina);
        //Pared con ventanal
        ObjetoDeLaEscena ParedConVentanal = new ObjetoDeLaEscena();
        ParedConVentanal.SetearFileReader(GetComponent<FileReader>());
        ParedConVentanal.CrearObjeto("Pared3", new Vector3(-3.5f, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Color.cyan);
        //pared1_banio.CrearObjeto("Pared1_banio", new Vector3(10, 0, 0), Vector3.zero, Vector3.one, Color.white);
        misObjetos.Add(ParedConVentanal);

        //pared4
        ObjetoDeLaEscena pared4 = new ObjetoDeLaEscena();
        pared4.SetearFileReader(GetComponent<FileReader>());
        pared4.CrearObjeto("Pared4", new Vector3(0, 0, -2.5f), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Color.yellow);
        //pared1_banio.CrearObjeto("Pared1_banio", new Vector3(10, 0, 0), Vector3.zero, Vector3.one, Color.white);
        misObjetos.Add(pared4);

        //Techo
        ObjetoDeLaEscena Techo = new ObjetoDeLaEscena();
        Techo.SetearFileReader(GetComponent<FileReader>());
        Techo.CrearObjeto("Piso", new Vector3(0, 2.5f, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Color.gray);
        //pared1_banio.CrearObjeto("Pared1_banio", new Vector3(10, 0, 0), Vector3.zero, Vector3.one, Color.white);
        misObjetos.Add(Techo);

        //TELEVISION BUAJAJAJAJAJ
        ObjetoDeLaEscena Television = new ObjetoDeLaEscena();
        Television.SetearFileReader(GetComponent<FileReader>());
        Television.CrearObjeto("tv", new Vector3(0, 0.7f, 0), new Vector3(0, 0, 0), new Vector3(0.01f, 0.01f, 0.01f), Color.magenta);
        //pared1_banio.CrearObjeto("Pared1_banio", new Vector3(10, 0, 0), Vector3.zero, Vector3.one, Color.white);
        misObjetos.Add(Television);

        ObjetoDeLaEscena Heladera = new ObjetoDeLaEscena();
        Heladera.SetearFileReader(GetComponent<FileReader>());
        Heladera.CrearObjeto("Heladera", new Vector3(0.8f, 0, -2f), new Vector3(0, 270, 0), new Vector3(1, 1, 1), Color.blue);
        misObjetos.Add(Heladera);

        ObjetoDeLaEscena inodoro = new ObjetoDeLaEscena();
        inodoro.SetearFileReader(GetComponent<FileReader>());
        inodoro.CrearObjeto("toilet2", new Vector3(2.8f, 0, 0.01f), new Vector3(0, 270, 0), new Vector3(0.7f, 0.7f, 0.7f), Color.cyan);
        misObjetos.Add(inodoro);
        //pared que divide la cocina del baño
        ObjetoDeLaEscena Pared2_banio = new ObjetoDeLaEscena();
        Pared2_banio.SetearFileReader(GetComponent<FileReader>());
        Pared2_banio.CrearObjeto("Pared2_Banio", new Vector3(2.5f, 0, -0.5f), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Color.blue);
        misObjetos.Add(Pared2_banio);

        //pared con puerta
        ObjetoDeLaEscena Pared3_banio = new ObjetoDeLaEscena();
        Pared3_banio.SetearFileReader(GetComponent<FileReader>());
        Pared3_banio.CrearObjeto("Pared3_Banio", new Vector3(1.5f, 0, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Color.red);
        misObjetos.Add(Pared3_banio);

        // Color azul clarito (R=0.5, G=0.7, B=1) con Alpha de 0.3 (muy transparente)
        UnityEngine.Color colorVentana = new UnityEngine.Color(0.5f, 0.7f, 1.0f, 0.3f);

        ObjetoDeLaEscena VentanaPrueba = new ObjetoDeLaEscena();
        VentanaPrueba.SetearFileReader(GetComponent<FileReader>());
        VentanaPrueba.CrearObjeto("VentanalComedor", new Vector3(-3.5f, 0.5f, 0f), new Vector3(0, 180, 0), new Vector3(1, 1, 1), colorVentana);
        misObjetos.Add(VentanaPrueba);
       
        ObjetoDeLaEscena ducha = new ObjetoDeLaEscena();
        ducha.SetearFileReader(GetComponent<FileReader>());
        ducha.CrearObjeto("shower", new Vector3(3.1f, 0, 2f), new Vector3(0, 0, 0), new Vector3(1f,1f, 1f), Color.cyan);
        misObjetos.Add(ducha);
        
        ObjetoDeLaEscena lavamanos = new ObjetoDeLaEscena();
        lavamanos.SetearFileReader(GetComponent<FileReader>());
        lavamanos.CrearObjeto("sink", new Vector3(2.14f, 0, 2.2f), new Vector3(0, 90, 0), new Vector3(0.7f, 0.7f, 0.7f), Color.yellow);
        misObjetos.Add(lavamanos);

        ObjetoDeLaEscena espejo = new ObjetoDeLaEscena();
        espejo.SetearFileReader(GetComponent<FileReader>());
        espejo.CrearObjeto("mirror", new Vector3(2.14f, 0.9f, 2.4f), new Vector3(0, 90, 0), new Vector3(1, 1, 1), Color.cyan);
        misObjetos.Add(espejo);
        
        ObjetoDeLaEscena cocina = new ObjetoDeLaEscena();
        cocina.SetearFileReader(GetComponent<FileReader>());
        cocina.CrearObjeto("KitchenStoveWithOven", new Vector3(3f, 0, -2f), new Vector3(0, 180, 0), new Vector3(0.9f, 0.9f, 0.9f  ), Color.blue);
        misObjetos.Add(cocina);

        ObjetoDeLaEscena armario = new ObjetoDeLaEscena();
        armario.SetearFileReader(GetComponent<FileReader>());
        armario.CrearObjeto("Wardrobe1", new Vector3(-0.5f, 0, -2.1f), new Vector3(0, 270, 0), new Vector3(0.9f, 0.9f, 0.9f), Color.black);
        misObjetos.Add(armario);

        //mesa de luz:
        ObjetoDeLaEscena mesaLuz = new ObjetoDeLaEscena();
        mesaLuz.SetearFileReader(GetComponent<FileReader>());
        mesaLuz.CrearObjeto("littleOne", new Vector3(-2.95f, 0, -2.2f), new Vector3(0, 270, 0), new Vector3(0.8f, 0.8f, 0.8f), Color.magenta);
        misObjetos.Add(mesaLuz);
       
        //sillas

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

