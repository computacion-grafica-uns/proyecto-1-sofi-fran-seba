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

    //VARIBALES PARA LOS TABLONES
     
    float Traslacion_En_X = 0 ;
    float Traslacion_En_Z = 0 ;

    int Indice_Color_A_Acceder = 0 ;
    Color MaderaOscura = new Color(0.25f, 0.15f, 0.1f);   // #402619
    Color MaderaMedia  = new Color(0.45f, 0.30f, 0.2f);   // #734D33
    Color MaderaClara  = new Color(0.65f, 0.50f, 0.35f);  // #A68059
    Color[] Arreglo_De_Colores; 



    void Start()
    {
        miCamara = new GameObject();

        //esto no vamas 
        miCamara.AddComponent<Camera>();

        miCamara.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        //establecemos el color negro
        miCamara.GetComponent<Camera>().backgroundColor = UnityEngine.Color.black;
        cc = new ControladorCamras();

        //-------------------------------------------------------------------------------------------------------------------------------------

        // ESTRUCTURA CASA

        // -------------------------------- ||| PAREDES ||| --------------------------------
        
        Color grisPizarraOscuro = new Color(0.07f, 0.07f, 0.07f, 1.0f);

        // --Pared 7mts con Puerta--
        ObjetoDeLaEscena paredPuerta = new ObjetoDeLaEscena();
        paredPuerta.SetearFileReader(GetComponent<FileReader>());
        paredPuerta.CrearObjeto("Pared2", new Vector3(0, 0, 2.5f), new Vector3(0, 180, 0), new Vector3(1, 1, 1), grisPizarraOscuro);
        misObjetos.Add(paredPuerta);
        
        // --Pared del Baño 3mts con ventana--
        ObjetoDeLaEscena pared1_banio = new ObjetoDeLaEscena() ;
        pared1_banio.SetearFileReader(GetComponent<FileReader>()) ;
        pared1_banio.CrearObjeto("Pared1_banio", new Vector3(3.5f, 0, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 1), grisPizarraOscuro);
        misObjetos.Add(pared1_banio) ;

        // --Pared de la Cocina 2 mts con ventana--
        ObjetoDeLaEscena pared1_cocina = new ObjetoDeLaEscena();
        pared1_cocina.SetearFileReader(GetComponent<FileReader>());
        pared1_cocina.CrearObjeto("Pared1_cocina", new Vector3(3.5f, 0, -1.5f), new Vector3(0, 0, 0), new Vector3(1, 1, 1), grisPizarraOscuro);
        misObjetos.Add(pared1_cocina);

        // --Pared con Ventanal--
        ObjetoDeLaEscena ParedConVentanal = new ObjetoDeLaEscena();
        ParedConVentanal.SetearFileReader(GetComponent<FileReader>());
        ParedConVentanal.CrearObjeto("Pared3", new Vector3(-3.5f, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1), grisPizarraOscuro);
        misObjetos.Add(ParedConVentanal);

        // --Pared 7 mts--
        ObjetoDeLaEscena pared4 = new ObjetoDeLaEscena();
        pared4.SetearFileReader(GetComponent<FileReader>());
        pared4.CrearObjeto("Pared4", new Vector3(0, 0, -2.5f), new Vector3(0, 0, 0), new Vector3(1, 1, 1), grisPizarraOscuro);
        misObjetos.Add(pared4);

        // --Techo--
        ObjetoDeLaEscena Techo = new ObjetoDeLaEscena();
        Techo.SetearFileReader(GetComponent<FileReader>());
        Techo.CrearObjeto("Piso", new Vector3(0, 2.5f, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Color.white);
        misObjetos.Add(Techo);

        // --Pared que divide la cocina del baño--
        ObjetoDeLaEscena Pared2_banio = new ObjetoDeLaEscena();
        Pared2_banio.SetearFileReader(GetComponent<FileReader>());
        Pared2_banio.CrearObjeto("Pared2_Banio", new Vector3(2.5f, 0, -0.5f), new Vector3(0, 0, 0), new Vector3(1, 1, 1), grisPizarraOscuro);
        misObjetos.Add(Pared2_banio);

        // --Pared que divide el baño del comedor con Puerta--
        ObjetoDeLaEscena Pared3_banio = new ObjetoDeLaEscena();
        Pared3_banio.SetearFileReader(GetComponent<FileReader>());
        Pared3_banio.CrearObjeto("Pared3_Banio", new Vector3(1.5f, 0, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 1), grisPizarraOscuro);
        misObjetos.Add(Pared3_banio);

        // -------------------------------- ||| SUELO ||| --------------------------------

        // --Suelo Comedor--
        ObjetoDeLaEscena piso = new ObjetoDeLaEscena();
        piso.SetearFileReader(GetComponent<FileReader>());
        piso.CrearObjeto("Piso", new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Color.grey);
        misObjetos.Add(piso);

        Arreglo_De_Colores = new Color[] { MaderaOscura, MaderaMedia, MaderaClara };

        ObjetoDeLaEscena TablonPiso ;

        for (int k = 0 ; k < 10 ; k++)
        {
            Traslacion_En_X = 0 ;


            for (int j = 0 ; j < 4 ; j++)
            {
                //Distincion por casos

                // --Primer Linea--
                if (j == 0)
                {
                    for (int i = 0 ; i < 5 ; i++)
                    {
                        TablonPiso = new ObjetoDeLaEscena();
                        TablonPiso.SetearFileReader(GetComponent<FileReader>());
                        TablonPiso.CrearObjeto("Tablon1MTS", new Vector3(-3f + i + Traslacion_En_X, 0.0099f, 2.4375f + Traslacion_En_Z), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Arreglo_De_Colores[Indice_Color_A_Acceder]);
                        misObjetos.Add(TablonPiso);
                        if (i < 4) //Almaceno siempre el ultimo color que use, porque la siguiente fila comienza con el ultimo color con el que pinte el ultimo tablon
                        {
                            Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) % 3 ;
                        }
                    }
                }

                // --Segunda Linea --
                if (j == 1)
                {
                    //Primer cuarto de tablon
                        TablonPiso = new ObjetoDeLaEscena();
                        TablonPiso.SetearFileReader(GetComponent<FileReader>());
                        TablonPiso.CrearObjeto("TablonUnCuartomts", new Vector3(-3.375f, 0.0099f, 2.4375f + Traslacion_En_Z), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Arreglo_De_Colores[Indice_Color_A_Acceder]);
                        misObjetos.Add(TablonPiso);
                        if (Indice_Color_A_Acceder == 2) //Estoy en el ultimo color
                        {
                            Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) % 3 ; //Lo vuelvo a setear a 0
                        }
                        else //De lo contrario, avanzo al siguiente color
                        {
                            Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) ;
                        }
                    
                    //Tablones Intermedios, son 3
                    for (int i = 0 ; i < 4 ; i++)
                    {
                        TablonPiso = new ObjetoDeLaEscena();
                        TablonPiso.SetearFileReader(GetComponent<FileReader>());
                        TablonPiso.CrearObjeto("Tablon1MTS", new Vector3(-3f + i + Traslacion_En_X, 0.0099f, 2.4375f + Traslacion_En_Z), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Arreglo_De_Colores[Indice_Color_A_Acceder]);
                        misObjetos.Add(TablonPiso);

                        if (Indice_Color_A_Acceder == 2) //Estoy en el ultimo color
                            {
                                Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) % 3 ; //Lo vuelvo a setear a 0
                            }
                        else //De lo contrario, avanzo al siguiente color
                            {
                                Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) ;
                            }
                    }

                    //Ultimo Tablon, son 3/4 de tablon
                        TablonPiso = new ObjetoDeLaEscena();
                        TablonPiso.SetearFileReader(GetComponent<FileReader>());
                        TablonPiso.CrearObjeto("TablonTresCuartosmts", new Vector3(1.125f, 0.0099f, 2.4375f + Traslacion_En_Z), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Arreglo_De_Colores[Indice_Color_A_Acceder]);
                        misObjetos.Add(TablonPiso);
                    //Aca siempre el ultimo indice de color NO lo toco, porque la siguiente fila tiene que seguir con el mismo color
                }

                // --Tercera Linea --
                if (j == 2)
                {
                    //Primer cuarto de tablon
                        TablonPiso = new ObjetoDeLaEscena();
                        TablonPiso.SetearFileReader(GetComponent<FileReader>());
                        TablonPiso.CrearObjeto("TablonMediomts", new Vector3(-3.25f, 0.0099f, 2.4375f + Traslacion_En_Z), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Arreglo_De_Colores[Indice_Color_A_Acceder]);
                        misObjetos.Add(TablonPiso);
                        if (Indice_Color_A_Acceder == 2) //Estoy en el ultimo color
                        {
                            Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) % 3 ; //Lo vuelvo a setear a 0
                        }
                        else //De lo contrario, avanzo al siguiente color
                        {
                            Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) ;
                        }
                    
                    //Tablones Intermedios, son 3
                    for (int i = 0 ; i < 4 ; i++)
                    {
                        TablonPiso = new ObjetoDeLaEscena();
                        TablonPiso.SetearFileReader(GetComponent<FileReader>());
                        TablonPiso.CrearObjeto("Tablon1MTS", new Vector3(-3f + i + Traslacion_En_X, 0.0099f, 2.4375f + Traslacion_En_Z), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Arreglo_De_Colores[Indice_Color_A_Acceder]);
                        misObjetos.Add(TablonPiso);

                        if (Indice_Color_A_Acceder == 2) //Estoy en el ultimo color
                            {
                                Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) % 3 ; //Lo vuelvo a setear a 0
                            }
                        else //De lo contrario, avanzo al siguiente color
                            {
                                Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) ;
                            }
                    }

                    //Ultimo Tablon, son 3/4 de tablon
                        TablonPiso = new ObjetoDeLaEscena();
                        TablonPiso.SetearFileReader(GetComponent<FileReader>());
                        TablonPiso.CrearObjeto("TablonMediomts", new Vector3(1.25f, 0.0099f, 2.4375f + Traslacion_En_Z), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Arreglo_De_Colores[Indice_Color_A_Acceder]);
                        misObjetos.Add(TablonPiso);
                    //Aca siempre el ultimo indice de color NO lo toco, porque la siguiente fila tiene que seguir con el mismo color
                }
                    
                
                // --Cuarta Linea --
                if (j == 3)
                 {
                    //Primer cuarto de tablon
                        TablonPiso = new ObjetoDeLaEscena();
                        TablonPiso.SetearFileReader(GetComponent<FileReader>());
                        TablonPiso.CrearObjeto("TablonTresCuartosmts", new Vector3(-3.125f, 0.0099f, 2.4375f + Traslacion_En_Z), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Arreglo_De_Colores[Indice_Color_A_Acceder]);
                        misObjetos.Add(TablonPiso);
                        if (Indice_Color_A_Acceder == 2) //Estoy en el ultimo color
                        {
                            Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) % 3 ; //Lo vuelvo a setear a 0
                        }
                        else //De lo contrario, avanzo al siguiente color
                        {
                            Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) ;
                        }
                    
                    //Tablones Intermedios, son 3
                    for (int i = 0 ; i < 4 ; i++)
                    {
                        TablonPiso = new ObjetoDeLaEscena();
                        TablonPiso.SetearFileReader(GetComponent<FileReader>());
                        TablonPiso.CrearObjeto("Tablon1MTS", new Vector3(-3f + i + Traslacion_En_X, 0.0099f, 2.4375f + Traslacion_En_Z), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Arreglo_De_Colores[Indice_Color_A_Acceder]);
                        misObjetos.Add(TablonPiso);

                        if (Indice_Color_A_Acceder == 2) //Estoy en el ultimo color
                            {
                                Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) % 3 ; //Lo vuelvo a setear a 0
                            }
                        else //De lo contrario, avanzo al siguiente color
                            {
                                Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) ;
                            }
                    }

                    //Ultimo Tablon, son 3/4 de tablon
                        TablonPiso = new ObjetoDeLaEscena();
                        TablonPiso.SetearFileReader(GetComponent<FileReader>());
                        TablonPiso.CrearObjeto("TablonUnCuartomts", new Vector3(1.375f, 0.0099f, 2.4375f + Traslacion_En_Z), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Arreglo_De_Colores[Indice_Color_A_Acceder]);
                        misObjetos.Add(TablonPiso);
                    //Aca siempre el ultimo indice de color NO lo toco, porque la siguiente fila tiene que seguir con el mismo color 
                } 
                    

                Traslacion_En_X = (Traslacion_En_X + 0.25f) ;
                Traslacion_En_Z = (Traslacion_En_Z - 0.125f) ;
            }
        }

        // --Suelo Baño--
    

        float tonoAleatorio ;
        Color colorBaldosa ;
        ObjetoDeLaEscena Baldosa ;
        Traslacion_En_Z = 0 ;
        for (int j = 0 ; j < 6 ; j++)
        {
            Traslacion_En_X = 0 ; //Esto cada vez que vuelvo a iterar en j (cambio de columna)
            //Traslacion en Z cambia una vez que cambia J
            //Traslacion en X aumenta 0.5 cada vez
            for (int i = 0 ; i < 4 ; i++)
            {
                tonoAleatorio = Random.Range(0.1f, 0.3f); // Grises medios
                colorBaldosa = new Color(tonoAleatorio, tonoAleatorio, tonoAleatorio);
                Baldosa = new ObjetoDeLaEscena();
                Baldosa.SetearFileReader(GetComponent<FileReader>());
                Baldosa.CrearObjeto("Baldosa", new Vector3(1.75f + Traslacion_En_X, 0.0099f, 2.25f + Traslacion_En_Z), new Vector3(0, 0, 0), new Vector3(1, 1, 1), colorBaldosa);
                misObjetos.Add(Baldosa);
                Traslacion_En_X = (Traslacion_En_X + 0.5f) ;
            }
            Traslacion_En_Z = (Traslacion_En_Z - 0.5f) ;
        }
        

        // -------------------------------- ||| VENTANAS ||| --------------------------------

        // Color azul clarito (R=0.5, G=0.7, B=1) con Alpha de 0.3 (muy transparente)
        UnityEngine.Color colorVentana = new UnityEngine.Color(0.5f, 0.7f, 1.0f, 0.3f);

        // --Ventanal del comedor--
        ObjetoDeLaEscena VentanalComedor = new ObjetoDeLaEscena();
        VentanalComedor.SetearFileReader(GetComponent<FileReader>());
        VentanalComedor.CrearObjeto("VentanalComedor", new Vector3(-3.5f, 0.5f, 0f), new Vector3(0, 180, 0), new Vector3(1, 1, 1), colorVentana);
        misObjetos.Add(VentanalComedor);

        // --Ventana Cocina--
        ObjetoDeLaEscena VentanaCocina = new ObjetoDeLaEscena();
        VentanaCocina.SetearFileReader(GetComponent<FileReader>());
        VentanaCocina.CrearObjeto("VentanaPequenia", new Vector3(3.5f, 1.5f, -1.5f), new Vector3(0, 180, 0), new Vector3(1, 1, 1), colorVentana);
        misObjetos.Add(VentanaCocina);

        // --Ventana Baño--
        ObjetoDeLaEscena VentanaBanio = new ObjetoDeLaEscena();
        VentanaBanio.SetearFileReader(GetComponent<FileReader>());
        VentanaBanio.CrearObjeto("VentanaPequenia", new Vector3(3.5f, 2f, 1f), new Vector3(0, 180, 0), new Vector3(1, 1, 1), colorVentana);
        misObjetos.Add(VentanaBanio);

        // --Marco Ventana--

        ObjetoDeLaEscena MarcoInferiorVentanal = new ObjetoDeLaEscena();
        MarcoInferiorVentanal.SetearFileReader(GetComponent<FileReader>());
        MarcoInferiorVentanal.CrearObjeto("Zocalo1mts", new Vector3(-3.4999f, 0.5f, 0f), new Vector3(0, 180, 0), new Vector3(1, 0.25f, 3), Color.black);
        misObjetos.Add(MarcoInferiorVentanal);

        ObjetoDeLaEscena MarcoSuperiorVentanal = new ObjetoDeLaEscena();
        MarcoSuperiorVentanal.SetearFileReader(GetComponent<FileReader>());
        MarcoSuperiorVentanal.CrearObjeto("Zocalo1mts", new Vector3(-3.4999f, 2f, 0f), new Vector3(0, 180, 0), new Vector3(1, 0.25f, 3), Color.black);
        misObjetos.Add(MarcoSuperiorVentanal);

        ObjetoDeLaEscena MarcoCentroVentanal = new ObjetoDeLaEscena();
        MarcoCentroVentanal.SetearFileReader(GetComponent<FileReader>());
        MarcoCentroVentanal.CrearObjeto("Zocalo1mts", new Vector3(-3.4999f, 1.25f, 0f), new Vector3(90, 180, 0), new Vector3(1, 0.5f, 1.5f), Color.black);
        misObjetos.Add(MarcoCentroVentanal);

        ObjetoDeLaEscena MarcoIzquierdoVentanal = new ObjetoDeLaEscena();
        MarcoIzquierdoVentanal.SetearFileReader(GetComponent<FileReader>());
        MarcoIzquierdoVentanal.CrearObjeto("Zocalo1mts", new Vector3(-3.4999f, 1.25f, 1.5f), new Vector3(90, 180, 0), new Vector3(1, 0.25f, 1.5f), Color.black);
        misObjetos.Add(MarcoIzquierdoVentanal);

        ObjetoDeLaEscena MarcoDerechoVentanal = new ObjetoDeLaEscena();
        MarcoDerechoVentanal.SetearFileReader(GetComponent<FileReader>());
        MarcoDerechoVentanal.CrearObjeto("Zocalo1mts", new Vector3(-3.4999f, 1.25f, -1.5f), new Vector3(90, 180, 0), new Vector3(1, 0.25f, 1.5f), Color.black);
        misObjetos.Add(MarcoDerechoVentanal);

        UnityEngine.Color colorReflejo = new UnityEngine.Color(0.5f, 0.7f, 1.0f, 0.5f);

        /*ObjetoDeLaEscena Reflejo1 = new ObjetoDeLaEscena();
        Reflejo1.SetearFileReader(GetComponent<FileReader>());
        Reflejo1.CrearObjeto("Zocalo1mts", new Vector3(-3.4999f, 1.25f, 0.75f), new Vector3(45, 180, 0), new Vector3(1, 0.25f, 1.5f), colorReflejo);
        misObjetos.Add(Reflejo1);

         ObjetoDeLaEscena Reflejo2 = new ObjetoDeLaEscena();
        Reflejo2.SetearFileReader(GetComponent<FileReader>());
        Reflejo2.CrearObjeto("Zocalo1mts", new Vector3(-3.4999f, 1.50f, 1f), new Vector3(45, 180, 0), new Vector3(1f, 0.25f, 0.5f), colorReflejo);
        misObjetos.Add(Reflejo2);

        ObjetoDeLaEscena Reflejo4 = new ObjetoDeLaEscena();
        Reflejo4.SetearFileReader(GetComponent<FileReader>());
        Reflejo4.CrearObjeto("Zocalo1mts", new Vector3(-3.4999f, 1.375f, 0.875f), new Vector3(45, 180, 0), new Vector3(1f, 0.25f, 1f), colorReflejo);
        misObjetos.Add(Reflejo4);

        ObjetoDeLaEscena Reflejo3 = new ObjetoDeLaEscena();
        Reflejo3.SetearFileReader(GetComponent<FileReader>());
        Reflejo3.CrearObjeto("Zocalo1mts", new Vector3(-3.4999f, 1f, 0.5f), new Vector3(45, 180, 0), new Vector3(1f, 0.25f, 0.5f), colorReflejo);
        misObjetos.Add(Reflejo3); 
        
          ObjetoDeLaEscena Reflejo5 = new ObjetoDeLaEscena();
        Reflejo5.SetearFileReader(GetComponent<FileReader>());
        Reflejo5.CrearObjeto("Zocalo1mts", new Vector3(-3.4999f, 1.125f, 0.375f), new Vector3(45, 180, 0), new Vector3(1f, 0.25f, 1f), colorReflejo);
        misObjetos.Add(Reflejo5);
        */


        //-------------------------------------------------------------------------------------------------------------------------------------

        // SECTOR COMEDOR

        // -------------------------------- ||| MOBILIARIO ||| --------------------------------

        // --Mesa--
        ObjetoDeLaEscena mesa = new ObjetoDeLaEscena();
        mesa.SetearFileReader(GetComponent<FileReader>());
        mesa.CrearObjeto("Table", new Vector3(-2.5f, 0, 2.1f), new Vector3(0, 0, 0), new Vector3(0.9f, 0.7f, 0.3f), Color.white);
        misObjetos.Add(mesa);

        //--banquetas
        ObjetoDeLaEscena banqueta1 = new ObjetoDeLaEscena();
        banqueta1.SetearFileReader(GetComponent<FileReader>());
        banqueta1.CrearObjeto("Table", new Vector3(-2.8f, 0, 1.9f), new Vector3(0, 0, 0), new Vector3(0.2f, 0.5f, 0.2f), Color.white);
        misObjetos.Add(banqueta1);
        ObjetoDeLaEscena banqueta2 = new ObjetoDeLaEscena();
        banqueta2.SetearFileReader(GetComponent<FileReader>());
        banqueta2.CrearObjeto("Table", new Vector3(-2.1f, 0, 1.9f), new Vector3(0, 0, 0), new Vector3(0.2f, 0.5f, 0.2f), Color.white);
        misObjetos.Add(banqueta2);

        // --Television--
        ObjetoDeLaEscena Television = new ObjetoDeLaEscena();
        Television.SetearFileReader(GetComponent<FileReader>());
        Television.CrearObjeto("tv", new Vector3(0, 0.7f, 0), new Vector3(0, 0, 0), new Vector3(0.01f, 0.01f, 0.01f), Color.magenta);
        //pared1_banio.CrearObjeto("Pared1_banio", new Vector3(10, 0, 0), Vector3.zero, Vector3.one, Color.white);
        misObjetos.Add(Television);

        // -------------------------------- ||| ESTRUCTURAS ||| --------------------------------


        //-------------------------------------------------------------------------------------------------------------------------------------

        // SECTOR BAÑO

        // -------------------------------- ||| MOBILIARIO ||| --------------------------------

        // --Ducha--
        ObjetoDeLaEscena ducha = new ObjetoDeLaEscena();
        ducha.SetearFileReader(GetComponent<FileReader>());
        ducha.CrearObjeto("shower", new Vector3(3.1f, 0, 2f), new Vector3(0, 0, 0), new Vector3(1f,1f, 1f), colorVentana);
        misObjetos.Add(ducha);
        
        // --Lavamanos--
        ObjetoDeLaEscena lavamanos = new ObjetoDeLaEscena();
        lavamanos.SetearFileReader(GetComponent<FileReader>());
        lavamanos.CrearObjeto("sink", new Vector3(2.14f, 0, 2.2f), new Vector3(0, 90, 0), new Vector3(0.7f, 0.7f, 0.7f), Color.yellow);
        misObjetos.Add(lavamanos);

        // --Espejo--
        ObjetoDeLaEscena espejo = new ObjetoDeLaEscena();
        espejo.SetearFileReader(GetComponent<FileReader>());
        espejo.CrearObjeto("mirror", new Vector3(2.14f, 0.9f, 2.4f), new Vector3(0, 90, 0), new Vector3(1, 1, 1), Color.cyan);
        misObjetos.Add(espejo);

        // --Inodoro--
        ObjetoDeLaEscena inodoro = new ObjetoDeLaEscena();
        inodoro.SetearFileReader(GetComponent<FileReader>());
        inodoro.CrearObjeto("toilet2", new Vector3(2.8f, 0, 0.01f), new Vector3(0, 270, 0), new Vector3(0.7f, 0.7f, 0.7f), Color.cyan);
        misObjetos.Add(inodoro);

        // -------------------------------- ||| ESTRUCTURAS ||| --------------------------------

        //-------------------------------------------------------------------------------------------------------------------------------------
        
        // SECTOR COCINA

        // -------------------------------- ||| MOBILIARIO ||| --------------------------------

        // --Cocina--
        ObjetoDeLaEscena cocina = new ObjetoDeLaEscena();
        cocina.SetearFileReader(GetComponent<FileReader>());
        cocina.CrearObjeto("KitchenStoveWithOven", new Vector3(3f, 0, -2f), new Vector3(0, 180, 0), new Vector3(0.9f, 0.9f, 0.9f  ), Color.blue);
        misObjetos.Add(cocina);

        // --Heladera--
        ObjetoDeLaEscena Heladera = new ObjetoDeLaEscena();
        Heladera.SetearFileReader(GetComponent<FileReader>());
        Heladera.CrearObjeto("Heladera", new Vector3(0.8f, 0, -2f), new Vector3(0, 270, 0), new Vector3(1, 1, 1), Color.blue);
        misObjetos.Add(Heladera);

        // -------------------------------- ||| ESTRUCTURAS ||| --------------------------------

        //-------------------------------------------------------------------------------------------------------------------------------------

        // SECTOR DORMITORIO

        // -------------------------------- ||| MOBILIARIO ||| --------------------------------

        // --Cama--
        ObjetoDeLaEscena cama = new ObjetoDeLaEscena() ;
        cama.SetearFileReader(GetComponent<FileReader>());
        cama.CrearObjeto("Bed1", new Vector3(-2f, 0, -1.2f), new Vector3(0, 0, 0), new Vector3(0.067f, 0.067f, 0.08f), Color.green);
        misObjetos.Add(cama);

        // --Armario--
        ObjetoDeLaEscena armario = new ObjetoDeLaEscena();
        armario.SetearFileReader(GetComponent<FileReader>());
        armario.CrearObjeto("Wardrobe1", new Vector3(-0.5f, 0, -2.1f), new Vector3(0, 270, 0), new Vector3(0.9f, 0.9f, 0.9f), Color.black);
        misObjetos.Add(armario);

        // --Mesa de Luz--
        ObjetoDeLaEscena mesaLuz = new ObjetoDeLaEscena();
        mesaLuz.SetearFileReader(GetComponent<FileReader>());
        mesaLuz.CrearObjeto("littleOne", new Vector3(-2.95f, 0, -2.2f), new Vector3(0, 270, 0), new Vector3(0.8f, 0.8f, 0.8f), Color.magenta);
        misObjetos.Add(mesaLuz);

        // -------------------------------- ||| ESTRUCTURAS ||| --------------------------------

        //-------------------------------------------------------------------------------------------------------------------------------------
       
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

