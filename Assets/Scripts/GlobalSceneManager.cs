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
    Color BlancoHumo = new Color(0.85f, 0.85f, 0.85f);
    Color colorGrisClarito = new Color(0.42f, 0.42f, 0.42f);
    Color GrisCeniza = new Color(0.72f, 0.72f, 0.72f);
    Color CafeProfundo = new Color(0.15f, 0.08f, 0.05f); // #26140D
    Color[] Arreglo_De_Colores;

    // objetos que podre desactivar
    private ObjetoDeLaEscena elTecho;
    private List<ObjetoDeLaEscena> lasParedes = new List<ObjetoDeLaEscena>(); // Lista solo de paredes



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
        lasParedes.Add(paredPuerta);

        // --Pared del Baño 3mts con ventana--
        ObjetoDeLaEscena pared1_banio = new ObjetoDeLaEscena();
        pared1_banio.SetearFileReader(GetComponent<FileReader>());
        pared1_banio.CrearObjeto("Pared1_banio", new Vector3(3.5f, 0, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 1), grisPizarraOscuro);
        misObjetos.Add(pared1_banio);
        lasParedes.Add(pared1_banio);

        // --Pared de la Cocina 2 mts con ventana--
        ObjetoDeLaEscena pared1_cocina = new ObjetoDeLaEscena();
        pared1_cocina.SetearFileReader(GetComponent<FileReader>());
        pared1_cocina.CrearObjeto("Pared1_cocina", new Vector3(3.5f, 0, -1.5f), new Vector3(0, 0, 0), new Vector3(1, 1, 1), grisPizarraOscuro);
        misObjetos.Add(pared1_cocina);
        lasParedes.Add(pared1_cocina);

        // --Pared con Ventanal--
        ObjetoDeLaEscena ParedConVentanal = new ObjetoDeLaEscena();
        ParedConVentanal.SetearFileReader(GetComponent<FileReader>());
        ParedConVentanal.CrearObjeto("Pared3", new Vector3(-3.5f, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1), grisPizarraOscuro);
        misObjetos.Add(ParedConVentanal);
        lasParedes.Add(ParedConVentanal);


        // --Pared 7 mts--
        ObjetoDeLaEscena pared4 = new ObjetoDeLaEscena();
        pared4.SetearFileReader(GetComponent<FileReader>());
        pared4.CrearObjeto("Pared4", new Vector3(0, 0, -2.5f), new Vector3(0, 0, 0), new Vector3(1, 1, 1), grisPizarraOscuro);
        misObjetos.Add(pared4);
        lasParedes.Add(pared4);

        // --Techo--
        ObjetoDeLaEscena Techo = new ObjetoDeLaEscena();
        Techo.SetearFileReader(GetComponent<FileReader>());
        Techo.CrearObjeto("Piso", new Vector3(0, 2.5f, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1), BlancoHumo);
        misObjetos.Add(Techo);
        elTecho = Techo;

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

        //Zocalos
        // --- ZOCALOS (Agregados a la lista de paredes para desaparecer con la tecla P) ---

        ObjetoDeLaEscena Zocalo1 = new ObjetoDeLaEscena();
        Zocalo1.SetearFileReader(GetComponent<FileReader>());
        Zocalo1.CrearObjeto("Zocalo1mts", new Vector3(-3.4999f, 0f, 0f), new Vector3(0, 180, 0), new Vector3(1, 1, 5), GrisCeniza);
        misObjetos.Add(Zocalo1);
        lasParedes.Add(Zocalo1);

        ObjetoDeLaEscena Zocalo2 = new ObjetoDeLaEscena();
        Zocalo2.SetearFileReader(GetComponent<FileReader>());
        Zocalo2.CrearObjeto("Zocalo1mts", new Vector3(-3.4999f, 2.375f, 0f), new Vector3(0, 180, 0), new Vector3(1, 1, 5), GrisCeniza);
        misObjetos.Add(Zocalo2);
        lasParedes.Add(Zocalo2);

        ObjetoDeLaEscena Zocalo3 = new ObjetoDeLaEscena();
        Zocalo3.SetearFileReader(GetComponent<FileReader>());
        Zocalo3.CrearObjeto("Zocalo1mts", new Vector3(-1f, 0f, -2.4999f), new Vector3(0, 90, 0), new Vector3(1, 1, 5), GrisCeniza);
        misObjetos.Add(Zocalo3);
        lasParedes.Add(Zocalo3);

        ObjetoDeLaEscena Zocalo4 = new ObjetoDeLaEscena();
        Zocalo4.SetearFileReader(GetComponent<FileReader>());
        Zocalo4.CrearObjeto("Zocalo1mts", new Vector3(-1f, 2.375f, -2.4999f), new Vector3(0, 90, 0), new Vector3(1, 1, 5), GrisCeniza);
        misObjetos.Add(Zocalo4);
        lasParedes.Add(Zocalo4);

        ObjetoDeLaEscena Zocalo5 = new ObjetoDeLaEscena();
        Zocalo5.SetearFileReader(GetComponent<FileReader>());
        Zocalo5.CrearObjeto("Zocalo1mts", new Vector3(-1f, 2.375f, 2.4999f), new Vector3(0, 90, 0), new Vector3(1, 1, 5), GrisCeniza);
        misObjetos.Add(Zocalo5);
        lasParedes.Add(Zocalo5);

        ObjetoDeLaEscena Zocalo6 = new ObjetoDeLaEscena();
        Zocalo6.SetearFileReader(GetComponent<FileReader>());
        Zocalo6.CrearObjeto("Zocalo1mts", new Vector3(-1.75f, 0f, 2.4999f), new Vector3(0, 90, 0), new Vector3(1, 1, 3.5f), GrisCeniza);
        misObjetos.Add(Zocalo6);
        lasParedes.Add(Zocalo6);

        ObjetoDeLaEscena Zocalo7 = new ObjetoDeLaEscena();
        Zocalo7.SetearFileReader(GetComponent<FileReader>());
        Zocalo7.CrearObjeto("Zocalo1mts", new Vector3(1.25f, 0f, 2.4999f), new Vector3(0, 90, 0), new Vector3(1, 1, 0.5f), GrisCeniza);
        misObjetos.Add(Zocalo7);
        lasParedes.Add(Zocalo7);

        ObjetoDeLaEscena Zocalo8 = new ObjetoDeLaEscena();
        Zocalo8.SetearFileReader(GetComponent<FileReader>());
        Zocalo8.CrearObjeto("Zocalo1mts", new Vector3(1.4999f, 0f, 2f), new Vector3(0, 0, 0), new Vector3(1, 1, 1f), GrisCeniza);
        misObjetos.Add(Zocalo8);
        lasParedes.Add(Zocalo8);

        ObjetoDeLaEscena Zocalo9 = new ObjetoDeLaEscena();
        Zocalo9.SetearFileReader(GetComponent<FileReader>());
        Zocalo9.CrearObjeto("Zocalo1mts", new Vector3(1.4999f, 0f, 0f), new Vector3(0, 0, 0), new Vector3(1, 1, 1f), GrisCeniza);
        misObjetos.Add(Zocalo9);
        lasParedes.Add(Zocalo9);

        ObjetoDeLaEscena Zocalo10 = new ObjetoDeLaEscena();
        Zocalo10.SetearFileReader(GetComponent<FileReader>());
        Zocalo10.CrearObjeto("Zocalo1mts", new Vector3(1.4999f, 2.375f, 1f), new Vector3(0, 0, 0), new Vector3(1, 1, 3f), GrisCeniza);
        misObjetos.Add(Zocalo10);
        lasParedes.Add(Zocalo10);

        ObjetoDeLaEscena Zocalo11 = new ObjetoDeLaEscena();
        Zocalo11.SetearFileReader(GetComponent<FileReader>());
        Zocalo11.CrearObjeto("Zocalo1mts", new Vector3(2.5f, 2.375f, -2.4999f), new Vector3(0, 90, 0), new Vector3(1, 1, 2f), GrisCeniza);
        misObjetos.Add(Zocalo11);
        lasParedes.Add(Zocalo11);

        ObjetoDeLaEscena Zocalo12 = new ObjetoDeLaEscena();
        Zocalo12.SetearFileReader(GetComponent<FileReader>());
        Zocalo12.CrearObjeto("Zocalo1mts", new Vector3(2.5f, 0f, -2.4999f), new Vector3(0, 90, 0), new Vector3(1, 1, 2f), GrisCeniza);
        misObjetos.Add(Zocalo12);
        lasParedes.Add(Zocalo12);

        ObjetoDeLaEscena Zocalo13 = new ObjetoDeLaEscena();
        Zocalo13.SetearFileReader(GetComponent<FileReader>());
        Zocalo13.CrearObjeto("Zocalo1mts", new Vector3(2.5f, 2.375f, -0.5001f), new Vector3(0, 90, 0), new Vector3(1, 1, 2f), GrisCeniza);
        misObjetos.Add(Zocalo13);
        lasParedes.Add(Zocalo13);

        ObjetoDeLaEscena Zocalo14 = new ObjetoDeLaEscena();
        Zocalo14.SetearFileReader(GetComponent<FileReader>());
        Zocalo14.CrearObjeto("Zocalo1mts", new Vector3(2.5f, 0f, -0.5001f), new Vector3(0, 90, 0), new Vector3(1, 1, 2f), GrisCeniza);
        misObjetos.Add(Zocalo14);
        lasParedes.Add(Zocalo14);

        ObjetoDeLaEscena Zocalo15 = new ObjetoDeLaEscena();
        Zocalo15.SetearFileReader(GetComponent<FileReader>());
        Zocalo15.CrearObjeto("Zocalo1mts", new Vector3(3.4999f, 2.375f, -1.5f), new Vector3(0, 0, 0), new Vector3(1, 1, 2f), GrisCeniza);
        misObjetos.Add(Zocalo15);
        lasParedes.Add(Zocalo15);

        ObjetoDeLaEscena Zocalo16 = new ObjetoDeLaEscena();
        Zocalo16.SetearFileReader(GetComponent<FileReader>());
        Zocalo16.CrearObjeto("Zocalo1mts", new Vector3(3.4999f, 0f, -1.5f), new Vector3(0, 0, 0), new Vector3(1, 1, 2f), GrisCeniza);
        misObjetos.Add(Zocalo16);
        lasParedes.Add(Zocalo16);


        // -------------------------------- ||| SUELO ||| --------------------------------

        // --Suelo Comedor--
        ObjetoDeLaEscena piso = new ObjetoDeLaEscena();
        piso.SetearFileReader(GetComponent<FileReader>());
        piso.CrearObjeto("Piso", new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Color.grey);
        misObjetos.Add(piso);

        Arreglo_De_Colores = new Color[] { MaderaOscura, MaderaMedia, MaderaClara };

        ObjetoDeLaEscena TablonPiso;

        for (int k = 0; k < 10; k++)
        {
            Traslacion_En_X = 0;


            for (int j = 0; j < 4; j++)
            {
                //Distincion por casos

                // --Primer Linea--
                if (j == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        TablonPiso = new ObjetoDeLaEscena();
                        TablonPiso.SetearFileReader(GetComponent<FileReader>());
                        TablonPiso.CrearObjeto("Tablon1MTS", new Vector3(-3f + i + Traslacion_En_X, 0.0099f, 2.4375f + Traslacion_En_Z), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Arreglo_De_Colores[Indice_Color_A_Acceder]);
                        misObjetos.Add(TablonPiso);
                        if (i < 4) //Almaceno siempre el ultimo color que use, porque la siguiente fila comienza con el ultimo color con el que pinte el ultimo tablon
                        {
                            Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) % 3;
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
                        Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) % 3; //Lo vuelvo a setear a 0
                    }
                    else //De lo contrario, avanzo al siguiente color
                    {
                        Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1);
                    }

                    //Tablones Intermedios, son 3
                    for (int i = 0; i < 4; i++)
                    {
                        TablonPiso = new ObjetoDeLaEscena();
                        TablonPiso.SetearFileReader(GetComponent<FileReader>());
                        TablonPiso.CrearObjeto("Tablon1MTS", new Vector3(-3f + i + Traslacion_En_X, 0.0099f, 2.4375f + Traslacion_En_Z), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Arreglo_De_Colores[Indice_Color_A_Acceder]);
                        misObjetos.Add(TablonPiso);

                        if (Indice_Color_A_Acceder == 2) //Estoy en el ultimo color
                        {
                            Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) % 3; //Lo vuelvo a setear a 0
                        }
                        else //De lo contrario, avanzo al siguiente color
                        {
                            Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1);
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
                        Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) % 3; //Lo vuelvo a setear a 0
                    }
                    else //De lo contrario, avanzo al siguiente color
                    {
                        Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1);
                    }

                    //Tablones Intermedios, son 3
                    for (int i = 0; i < 4; i++)
                    {
                        TablonPiso = new ObjetoDeLaEscena();
                        TablonPiso.SetearFileReader(GetComponent<FileReader>());
                        TablonPiso.CrearObjeto("Tablon1MTS", new Vector3(-3f + i + Traslacion_En_X, 0.0099f, 2.4375f + Traslacion_En_Z), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Arreglo_De_Colores[Indice_Color_A_Acceder]);
                        misObjetos.Add(TablonPiso);

                        if (Indice_Color_A_Acceder == 2) //Estoy en el ultimo color
                        {
                            Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) % 3; //Lo vuelvo a setear a 0
                        }
                        else //De lo contrario, avanzo al siguiente color
                        {
                            Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1);
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
                        Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) % 3; //Lo vuelvo a setear a 0
                    }
                    else //De lo contrario, avanzo al siguiente color
                    {
                        Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1);
                    }

                    //Tablones Intermedios, son 3
                    for (int i = 0; i < 4; i++)
                    {
                        TablonPiso = new ObjetoDeLaEscena();
                        TablonPiso.SetearFileReader(GetComponent<FileReader>());
                        TablonPiso.CrearObjeto("Tablon1MTS", new Vector3(-3f + i + Traslacion_En_X, 0.0099f, 2.4375f + Traslacion_En_Z), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Arreglo_De_Colores[Indice_Color_A_Acceder]);
                        misObjetos.Add(TablonPiso);

                        if (Indice_Color_A_Acceder == 2) //Estoy en el ultimo color
                        {
                            Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1) % 3; //Lo vuelvo a setear a 0
                        }
                        else //De lo contrario, avanzo al siguiente color
                        {
                            Indice_Color_A_Acceder = (Indice_Color_A_Acceder + 1);
                        }
                    }

                    //Ultimo Tablon, son 3/4 de tablon
                    TablonPiso = new ObjetoDeLaEscena();
                    TablonPiso.SetearFileReader(GetComponent<FileReader>());
                    TablonPiso.CrearObjeto("TablonUnCuartomts", new Vector3(1.375f, 0.0099f, 2.4375f + Traslacion_En_Z), new Vector3(0, 0, 0), new Vector3(1, 1, 1), Arreglo_De_Colores[Indice_Color_A_Acceder]);
                    misObjetos.Add(TablonPiso);
                    //Aca siempre el ultimo indice de color NO lo toco, porque la siguiente fila tiene que seguir con el mismo color 
                }


                Traslacion_En_X = (Traslacion_En_X + 0.25f);
                Traslacion_En_Z = (Traslacion_En_Z - 0.125f);
            }
        }

        // --Suelo Baño--


        float tonoAleatorio;
        Color colorBaldosa;
        ObjetoDeLaEscena Baldosa;
        Traslacion_En_Z = 0;
        for (int j = 0; j < 6; j++)
        {
            Traslacion_En_X = 0; //Esto cada vez que vuelvo a iterar en j (cambio de columna)
            //Traslacion en Z cambia una vez que cambia J
            //Traslacion en X aumenta 0.5 cada vez
            for (int i = 0; i < 4; i++)
            {
                tonoAleatorio = Random.Range(0.1f, 0.3f); // Grises medios
                colorBaldosa = new Color(tonoAleatorio, tonoAleatorio, tonoAleatorio);
                Baldosa = new ObjetoDeLaEscena();
                Baldosa.SetearFileReader(GetComponent<FileReader>());
                Baldosa.CrearObjeto("Baldosa", new Vector3(1.75f + Traslacion_En_X, 0.0099f, 2.25f + Traslacion_En_Z), new Vector3(0, 0, 0), new Vector3(1, 1, 1), colorBaldosa);
                misObjetos.Add(Baldosa);
                Traslacion_En_X = (Traslacion_En_X + 0.5f);
            }
            Traslacion_En_Z = (Traslacion_En_Z - 0.5f);
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

         // --Marco Ventana Cocina--

        ObjetoDeLaEscena MarcoInferiorVentanaCocina = new ObjetoDeLaEscena();
        MarcoInferiorVentanaCocina.SetearFileReader(GetComponent<FileReader>());
        MarcoInferiorVentanaCocina.CrearObjeto("Zocalo1mts", new Vector3(3.4999f, 1.5f, -1.5f), new Vector3(0, 0, 0), new Vector3(1, 0.25f, 1), Color.black);
        misObjetos.Add(MarcoInferiorVentanaCocina);

        ObjetoDeLaEscena MarcoSuperiorVentanaCocina = new ObjetoDeLaEscena();
        MarcoSuperiorVentanaCocina.SetearFileReader(GetComponent<FileReader>());
        MarcoSuperiorVentanaCocina.CrearObjeto("Zocalo1mts", new Vector3(3.4999f, 2f, -1.5f), new Vector3(0, 0, 0), new Vector3(1, 0.25f, 1), Color.black);
        misObjetos.Add(MarcoSuperiorVentanaCocina);

        ObjetoDeLaEscena MarcoIzquierdoVentanaCocina = new ObjetoDeLaEscena();
        MarcoIzquierdoVentanaCocina.SetearFileReader(GetComponent<FileReader>());
        MarcoIzquierdoVentanaCocina.CrearObjeto("Zocalo1mts", new Vector3(3.4999f, 1.75f, -2f), new Vector3(90, 0, 0), new Vector3(1, 0.25f, 0.5f), Color.black);
        misObjetos.Add(MarcoIzquierdoVentanaCocina);

        ObjetoDeLaEscena MarcoDerechoVentanaCocina = new ObjetoDeLaEscena();
        MarcoDerechoVentanaCocina.SetearFileReader(GetComponent<FileReader>());
        MarcoDerechoVentanaCocina.CrearObjeto("Zocalo1mts", new Vector3(3.4999f, 1.767f, -1f), new Vector3(90, 0, 0), new Vector3(1, 0.25f, 0.535f), Color.black);
        misObjetos.Add(MarcoDerechoVentanaCocina);

        ObjetoDeLaEscena MarcoCentralVentanaCocina = new ObjetoDeLaEscena();
        MarcoCentralVentanaCocina.SetearFileReader(GetComponent<FileReader>());
        MarcoCentralVentanaCocina.CrearObjeto("Zocalo1mts", new Vector3(3.4999f, 1.75f, -1.51f), new Vector3(90, 0, 0), new Vector3(1, 0.5f, 0.5f), Color.black);
        misObjetos.Add(MarcoCentralVentanaCocina);

        // --Marco Ventana Baño --

        ObjetoDeLaEscena MarcoInferiorVentanaBaño = new ObjetoDeLaEscena();
        MarcoInferiorVentanaBaño.SetearFileReader(GetComponent<FileReader>());
        MarcoInferiorVentanaBaño.CrearObjeto("Zocalo1mts", new Vector3(3.4999f, 2f, 1.0f), new Vector3(0, 0, 0), new Vector3(1, 0.25f, 1), Color.black);
        misObjetos.Add(MarcoInferiorVentanaBaño);

        ObjetoDeLaEscena ManijaVentanaBaño = new ObjetoDeLaEscena();
        ManijaVentanaBaño.SetearFileReader(GetComponent<FileReader>());
        ManijaVentanaBaño.CrearObjeto("Zocalo1mts", new Vector3(3.4999f, 2.0625f, 1.0f), new Vector3(0, 0, 0), new Vector3(1, 0.25f, 0.125f), Color.black);
        misObjetos.Add(ManijaVentanaBaño);

        ObjetoDeLaEscena MarcoSuperiorVentanaBaño = new ObjetoDeLaEscena();
        MarcoSuperiorVentanaBaño.SetearFileReader(GetComponent<FileReader>());
        MarcoSuperiorVentanaBaño.CrearObjeto("Zocalo1mts", new Vector3(3.4999f, 2.465f, 1.0f), new Vector3(0, 0, 0), new Vector3(1, 0.25f, 1), Color.black);
        misObjetos.Add(MarcoSuperiorVentanaBaño);

        ObjetoDeLaEscena MarcoIzquierdoVentanaBaño = new ObjetoDeLaEscena();
        MarcoIzquierdoVentanaBaño.SetearFileReader(GetComponent<FileReader>());
        MarcoIzquierdoVentanaBaño.CrearObjeto("Zocalo1mts", new Vector3(3.4999f, 2.25f, 0.5f), new Vector3(90, 0, 0), new Vector3(1, 0.25f, 0.5f), Color.black);
        misObjetos.Add(MarcoIzquierdoVentanaBaño);

        ObjetoDeLaEscena MarcoDerechoVentanaBaño = new ObjetoDeLaEscena();
        MarcoDerechoVentanaBaño.SetearFileReader(GetComponent<FileReader>());
        MarcoDerechoVentanaBaño.CrearObjeto("Zocalo1mts", new Vector3(3.4999f, 2.25f, 1.5f), new Vector3(90, 0, 0), new Vector3(1, 0.25f, 0.5f), Color.black);
        misObjetos.Add(MarcoDerechoVentanaBaño);

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
        mesa.CrearObjeto("Table", new Vector3(-2.5f, 0, 2.1f), new Vector3(0, 0, 0), new Vector3(0.9f, 0.7f, 0.3f), Color.black);
        misObjetos.Add(mesa);

        //--banquetas
        ObjetoDeLaEscena banqueta1 = new ObjetoDeLaEscena();
        banqueta1.SetearFileReader(GetComponent<FileReader>());
        banqueta1.CrearObjeto("Table", new Vector3(-2.8f, 0, 1.9f), new Vector3(0, 0, 0), new Vector3(0.2f, 0.5f, 0.2f), Color.black);
        misObjetos.Add(banqueta1);
        ObjetoDeLaEscena banqueta2 = new ObjetoDeLaEscena();
        banqueta2.SetearFileReader(GetComponent<FileReader>());
        banqueta2.CrearObjeto("Table", new Vector3(-2.1f, 0, 1.9f), new Vector3(0, 0, 0), new Vector3(0.2f, 0.5f, 0.2f), Color.black);
        misObjetos.Add(banqueta2);

        // --Television--
        ObjetoDeLaEscena Television = new ObjetoDeLaEscena();
        Television.SetearFileReader(GetComponent<FileReader>());
        Television.CrearObjeto("tv", new Vector3(-3.1f, 0.75f, 2.1f), new Vector3(0, 45, 0), new Vector3(0.007f, 0.007f, 0.007f), colorGrisClarito);
        //pared1_banio.CrearObjeto("Pared1_banio", new Vector3(10, 0, 0), Vector3.zero, Vector3.one, Color.white);
        misObjetos.Add(Television);

        ObjetoDeLaEscena Estanteria = new ObjetoDeLaEscena();
        Estanteria.SetearFileReader(GetComponent<FileReader>());
        Estanteria.CrearObjeto("Wardrobe2", new Vector3(-0.98f, 0f, 2.1f), new Vector3(0, 90, 0), new Vector3(1f, 0.8f, 0.75f), CafeProfundo);
        misObjetos.Add(Estanteria);

        // -------------------------------- ||| ESTRUCTURAS ||| --------------------------------


        //-------------------------------------------------------------------------------------------------------------------------------------

        // SECTOR BAÑO

        // -------------------------------- ||| MOBILIARIO ||| --------------------------------

        // --Ducha--
        ObjetoDeLaEscena ducha = new ObjetoDeLaEscena();
        ducha.SetearFileReader(GetComponent<FileReader>());
        ducha.CrearObjeto("shower", new Vector3(3.0f, 0, 2f), new Vector3(0, 90f, 0), new Vector3(1f, 1f, 1f), colorVentana);
        misObjetos.Add(ducha);

        // --Lavamanos--
        ObjetoDeLaEscena lavamanos = new ObjetoDeLaEscena();
        lavamanos.SetearFileReader(GetComponent<FileReader>());
        lavamanos.CrearObjeto("sink", new Vector3(2.14f, 0, 2.2f), new Vector3(0, 90, 0), new Vector3(0.7f, 0.7f, 0.7f), BlancoHumo);
        misObjetos.Add(lavamanos);

        UnityEngine.Color colorEspejo = new UnityEngine.Color(0.5f, 0.7f, 1.0f, 0.8f);

        // --Espejo--
        ObjetoDeLaEscena espejo = new ObjetoDeLaEscena();
        espejo.SetearFileReader(GetComponent<FileReader>());
        espejo.CrearObjeto("mirror", new Vector3(2.14f, 0.9f, 2.4f), new Vector3(0, 90, 0), new Vector3(1, 1, 1), colorEspejo);
        misObjetos.Add(espejo);

        // --Inodoro--
        ObjetoDeLaEscena inodoro = new ObjetoDeLaEscena();
        inodoro.SetearFileReader(GetComponent<FileReader>());
        inodoro.CrearObjeto("toilet2", new Vector3(2.8f, 0, 0.01f), new Vector3(0, 270, 0), new Vector3(0.7f, 0.7f, 0.7f), BlancoHumo);
        misObjetos.Add(inodoro);

        // -------------------------------- ||| ESTRUCTURAS ||| --------------------------------

        //-------------------------------------------------------------------------------------------------------------------------------------

        // SECTOR COCINA

        // -------------------------------- ||| MOBILIARIO ||| --------------------------------

        // --Cocina--
        ObjetoDeLaEscena cocina = new ObjetoDeLaEscena();
        cocina.SetearFileReader(GetComponent<FileReader>());
        cocina.CrearObjeto("KitchenStoveWithOven", new Vector3(3f, 0, -2f), new Vector3(0, 180, 0), new Vector3(1f, 1f, 1f), BlancoHumo);
        misObjetos.Add(cocina);

        // --- detalles cocina
        float xC = 3.0f;
        float zC = -2.0f;
        float yH = 1.04f; // Altura sobre la mesada

        //  rejilla(cruces9
        ObjetoDeLaEscena r1 = new ObjetoDeLaEscena();
        r1.SetearFileReader(GetComponent<FileReader>());
        r1.CrearObjeto("Zocalo1mts", new Vector3(xC, yH, zC), new Vector3(0, 180, 0), new Vector3(0.02f, 0.02f, 0.7f), Color.black);
        misObjetos.Add(r1);

        ObjetoDeLaEscena r2 = new ObjetoDeLaEscena();
        r2.SetearFileReader(GetComponent<FileReader>());
        r2.CrearObjeto("Zocalo1mts", new Vector3(xC, yH, zC), new Vector3(0, 90, 0), new Vector3(0.02f, 0.02f, 0.7f), Color.black);
        misObjetos.Add(r2);


        //  hornallas(escalar el piso)
        float d = 0.18f;
        float[,] posic = { { d, d }, { d, -d }, { -d, d }, { -d, -d } };

        for (int i = 0; i < 4; i++)
        {
            ObjetoDeLaEscena h = new ObjetoDeLaEscena();
            h.SetearFileReader(GetComponent<FileReader>());

            Vector3 p = new Vector3(xC + posic[i, 0], 1.05f, zC + posic[i, 1]);

            h.CrearObjeto("Piso", p, Vector3.zero, new Vector3(0.015f, 0.01f, 0.02f), Color.black);
            misObjetos.Add(h);
        }


        // --Heladera--
        ObjetoDeLaEscena Heladera = new ObjetoDeLaEscena();
        Heladera.SetearFileReader(GetComponent<FileReader>());
        Heladera.CrearObjeto("Heladera", new Vector3(0.8f, 0, -2f), new Vector3(0, 270, 0), new Vector3(1, 1, 1), BlancoHumo);
        misObjetos.Add(Heladera);

        //linea detalle heladera:
        Vector3 posH = new Vector3(0.7f, 0, -2.03f);
        ObjetoDeLaEscena lineaH = new ObjetoDeLaEscena();
        lineaH.SetearFileReader(GetComponent<FileReader>());
        Vector3 posL = new Vector3(posH.x + 0.1f, 1.2f, posH.z + 0.5f);
        lineaH.CrearObjeto("Zocalo1mts", posL, new Vector3(0, 90, 0), new Vector3(0.025f, 0.07f, 0.8f), Color.black);
        misObjetos.Add(lineaH);




        // --Lavamanos--
        ObjetoDeLaEscena Lavamanos = new ObjetoDeLaEscena();
        Lavamanos.SetearFileReader(GetComponent<FileReader>());
        Lavamanos.CrearObjeto("KitchenCabinet1WithSink", new Vector3(3f, 0, -1f), new Vector3(0, 180, 0), new Vector3(1f, 1f, 1f), CafeProfundo);
        misObjetos.Add(Lavamanos);

        // -------------------------------- ||| ESTRUCTURAS ||| --------------------------------

        //-------------------------------------------------------------------------------------------------------------------------------------

        // SECTOR DORMITORIO

        // -------------------------------- ||| MOBILIARIO ||| --------------------------------

        // --Cama--
        ObjetoDeLaEscena cama = new ObjetoDeLaEscena();
        cama.SetearFileReader(GetComponent<FileReader>());
        cama.CrearObjeto("Bed1", new Vector3(-2f, 0, -1.2f), new Vector3(0, 0, 0), new Vector3(0.067f, 0.067f, 0.08f), CafeProfundo);
        misObjetos.Add(cama);

        // --Texturas Cama--

        ObjetoDeLaEscena ColchonCama1 = new ObjetoDeLaEscena();
        ColchonCama1.SetearFileReader(GetComponent<FileReader>());
        ColchonCama1.CrearObjeto("Zocalo1mts", new Vector3(-2.61f, 0.30f, -1.2f), new Vector3(0, 0, 0), new Vector3(1, 1, 2.18f), BlancoHumo);
        misObjetos.Add(ColchonCama1);

        ObjetoDeLaEscena ColchonCama2 = new ObjetoDeLaEscena();
        ColchonCama2.SetearFileReader(GetComponent<FileReader>());
        ColchonCama2.CrearObjeto("Zocalo1mts", new Vector3(-1.39f, 0.30f, -1.2f), new Vector3(0, 0, 0), new Vector3(1, 1, 2.18f), BlancoHumo);
        misObjetos.Add(ColchonCama2);

        ObjetoDeLaEscena ColchonCama3 = new ObjetoDeLaEscena();
        ColchonCama3.SetearFileReader(GetComponent<FileReader>());
        ColchonCama3.CrearObjeto("Zocalo1mts", new Vector3(-2f, 0.30f, -0.11f), new Vector3(0, 90, 0), new Vector3(1, 1, 1.22f), BlancoHumo);
        misObjetos.Add(ColchonCama3);

        ObjetoDeLaEscena ColchonCama4 = new ObjetoDeLaEscena();
        ColchonCama4.SetearFileReader(GetComponent<FileReader>());
        ColchonCama4.CrearObjeto("Baldosa", new Vector3(-2f, 0.425f, -1.2f), new Vector3(0, 0, 0), new Vector3(2.442f, 1, 4.3f), BlancoHumo);
        misObjetos.Add(ColchonCama4);

        // -- Acolchado --

        ObjetoDeLaEscena AcolchadoCama1 = new ObjetoDeLaEscena();
        AcolchadoCama1.SetearFileReader(GetComponent<FileReader>());
        AcolchadoCama1.CrearObjeto("Zocalo1mts", new Vector3(-2.6101f, 0.30f, -0.91f), new Vector3(0, 0, 0), new Vector3(1, 1, 1.6f), colorGrisClarito);
        misObjetos.Add(AcolchadoCama1);

        ObjetoDeLaEscena AcolchadoCama2 = new ObjetoDeLaEscena();
        AcolchadoCama2.SetearFileReader(GetComponent<FileReader>());
        AcolchadoCama2.CrearObjeto("Zocalo1mts", new Vector3(-1.3899f, 0.30f, -0.91f), new Vector3(0, 0, 0), new Vector3(1, 1, 1.6f), colorGrisClarito);
        misObjetos.Add(AcolchadoCama2);

        ObjetoDeLaEscena AcolchadoCama3 = new ObjetoDeLaEscena();
        AcolchadoCama3.SetearFileReader(GetComponent<FileReader>());
        AcolchadoCama3.CrearObjeto("Zocalo1mts", new Vector3(-2f, 0.30f, -0.1099f), new Vector3(0, 90, 0), new Vector3(1, 1, 1.22f), colorGrisClarito);
        misObjetos.Add(AcolchadoCama3);

        ObjetoDeLaEscena AcolchadoCama4 = new ObjetoDeLaEscena();
        AcolchadoCama4.SetearFileReader(GetComponent<FileReader>());
        AcolchadoCama4.CrearObjeto("Baldosa", new Vector3(-2f, 0.4251f, -0.91f), new Vector3(0, 0, 0), new Vector3(2.442f, 1, 3.2f), colorGrisClarito);
        misObjetos.Add(AcolchadoCama4);

        // --Almohada--


        // --Armario--
        ObjetoDeLaEscena armario = new ObjetoDeLaEscena();
        armario.SetearFileReader(GetComponent<FileReader>());
        armario.CrearObjeto("Wardrobe1", new Vector3(-0.5f, 0, -2.1f), new Vector3(0, 270, 0), new Vector3(0.9f, 0.9f, 0.9f), CafeProfundo);
        misObjetos.Add(armario);

        //detalles armario
        Color MarronOscuro = new Color(0.04f, 0.02f, 0.01f, 1f);
        // MANIJA IZQUIERDA
        ObjetoDeLaEscena manijaIzq = new ObjetoDeLaEscena();
        manijaIzq.SetearFileReader(GetComponent<FileReader>());

        float posX_Izq = -0.6f;
        float posY_Izq = 1.1f;
        float posZ_Izq = -1.8f;

        manijaIzq.CrearObjeto("Piso", new Vector3(posX_Izq, posY_Izq, posZ_Izq), new Vector3(90, 0, 180), new Vector3(0.015f, 0.01f, 0.02f), MarronOscuro);
        misObjetos.Add(manijaIzq);

        // MANIJA DERECHA
        ObjetoDeLaEscena manijaDer = new ObjetoDeLaEscena();
        manijaDer.SetearFileReader(GetComponent<FileReader>());

        float posX_Der = -0.4f;
        float posY_Der = 1.1f;
        float posZ_Der = -1.8f;

        manijaDer.CrearObjeto("Piso", new Vector3(posX_Der, posY_Der, posZ_Der), new Vector3(90, 0, 180), new Vector3(0.015f, 0.01f, 0.02f), MarronOscuro);
        misObjetos.Add(manijaDer);



        // --Mesa de Luz--
        ObjetoDeLaEscena mesaLuz = new ObjetoDeLaEscena();
        mesaLuz.SetearFileReader(GetComponent<FileReader>());
        mesaLuz.CrearObjeto("littleOne", new Vector3(-2.95f, 0, -2.2f), new Vector3(0, 270, 0), new Vector3(0.8f, 0.8f, 0.8f), CafeProfundo);
        misObjetos.Add(mesaLuz);

        // Libros 



        // --- LLENADO DE LIBRERÍA ---
        float desplazamientoX = 0.15f; // Lo que se mueve cada libro (ajustalo si quedan muy separados)
        float alturaEstante1 = 1.46f;
        float alturaEstante2 = 1.10f; // Un poco más abajo para el segundo estante

        for (int i = 0; i < 6; i++)
        {
            // --- ESTANTE 1 (ARRIBA) ---
            ObjetoDeLaEscena libroSup = new ObjetoDeLaEscena();
            libroSup.SetearFileReader(GetComponent<FileReader>());

            // Color aleatorio para cada libro
            Color colorLibro = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);

            // Posición: sumamos i * desplazamientoX para que se formen en fila
            Vector3 posSup = new Vector3(-0.6f - (i * desplazamientoX), alturaEstante1, 2.1f);

            libroSup.CrearObjeto("Cubo", posSup, Vector3.zero, new Vector3(0.1f, 0.28f, 0.4f), colorLibro);
            misObjetos.Add(libroSup);

            // --- ESTANTE 2 (ABAJO) ---
            ObjetoDeLaEscena libroInf = new ObjetoDeLaEscena();
            libroInf.SetearFileReader(GetComponent<FileReader>());

            Color colorLibro2 = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);

            // Misma lógica pero en alturaEstante2
            Vector3 posInf = new Vector3(-0.6f - (i * desplazamientoX), alturaEstante2, 2.1f);

            libroInf.CrearObjeto("Cubo", posInf, Vector3.zero, new Vector3(0.1f, 0.28f, 0.4f), colorLibro2);
            misObjetos.Add(libroInf);
        }
        //Libros horizontales
        ObjetoDeLaEscena cubo = new ObjetoDeLaEscena();
        cubo.SetearFileReader(GetComponent<FileReader>());
        cubo.CrearObjeto("Cubo", new Vector3(-0.6f, 0.9f, 2), new Vector3(0, 0, 90), new Vector3(0.04f, 0.28f, 0.4f), Color.blue);
        misObjetos.Add(cubo);
        ObjetoDeLaEscena cubo1 = new ObjetoDeLaEscena();
        cubo1.SetearFileReader(GetComponent<FileReader>());
        cubo1.CrearObjeto("Cubo", new Vector3(-0.9f, 0.9f, 2), new Vector3(0, 0, 90), new Vector3(0.04f, 0.28f, 0.4f), Color.red);
        misObjetos.Add(cubo1);
        ObjetoDeLaEscena cubo2 = new ObjetoDeLaEscena();
        cubo2.SetearFileReader(GetComponent<FileReader>());
        cubo2.CrearObjeto("Cubo", new Vector3(-0.8f, 0.95f, 2), new Vector3(0, 0, 90), new Vector3(0.04f, 0.28f, 0.4f), Color.cyan);
        misObjetos.Add(cubo2);

        //Relleno del ultimo estante

        ObjetoDeLaEscena cubo3 = new ObjetoDeLaEscena();
        cubo3.SetearFileReader(GetComponent<FileReader>());
        cubo3.CrearObjeto("Cubo", new Vector3(-0.7f, 0.2f, 2), new Vector3(0, 0, 0), new Vector3(0.3f, 0.13f, 0.3f), Color.yellow);
        misObjetos.Add(cubo3);
        ObjetoDeLaEscena cubo4 = new ObjetoDeLaEscena();
        cubo4.SetearFileReader(GetComponent<FileReader>());
        cubo4.CrearObjeto("Cubo", new Vector3(-0.9f, 0.2f, 2), new Vector3(0, 0, 0), new Vector3(0.3f, 0.13f, 0.3f), Color.yellow);
        misObjetos.Add(cubo4);

        ObjetoDeLaEscena cubo5 = new ObjetoDeLaEscena();
        cubo5.SetearFileReader(GetComponent<FileReader>());
        cubo5.CrearObjeto("Cubo", new Vector3(-2f, 0.8f, 2), new Vector3(0, 0, 0), new Vector3(0.3f, 0.03f, 0.3f), Color.white);
        misObjetos.Add(cubo5);

        // --Pintura--

            Color Cielo = new Color(0.576f, 0.745f, 0.749f);
            Color Mar = new Color(0.490f, 0.627f, 0.761f);

            ObjetoDeLaEscena Lienzo = new ObjetoDeLaEscena();
            Lienzo.SetearFileReader(GetComponent<FileReader>());
            Lienzo.CrearObjeto("Baldosa", new Vector3(-2f, 1.5f, -2.4999f), new Vector3(90, 0, 0), new Vector3(1.5f, 1f, 2f), BlancoHumo);
            misObjetos.Add(Lienzo);

            ObjetoDeLaEscena Fondo = new ObjetoDeLaEscena();
            Fondo.SetearFileReader(GetComponent<FileReader>());
            Fondo.CrearObjeto("Baldosa", new Vector3(-2f, 1.5f, -2.4998f), new Vector3(90, 0, 0), new Vector3(1f, 1f, 1.5f), Cielo);
            misObjetos.Add(Fondo);

            ObjetoDeLaEscena Oceano = new ObjetoDeLaEscena();
            Oceano.SetearFileReader(GetComponent<FileReader>());
            Oceano.CrearObjeto("Baldosa", new Vector3(-2f, 1.25f, -2.4997f), new Vector3(90, 0, 0), new Vector3(1f, 1f, 0.5f), Mar);
            misObjetos.Add(Oceano);
            
            // -- Velero --

            Color RojoTierra = new Color(0.592f, 0.223f, 0.133f);
            Color RojoOscuro = new Color(0.384f, 0.196f, 0.161f);
            Color Mastil = new Color(0.478f, 0.392f, 0.368f);

            ObjetoDeLaEscena Casco1 = new ObjetoDeLaEscena();
            Casco1.SetearFileReader(GetComponent<FileReader>());
            Casco1.CrearObjeto("Baldosa", new Vector3(-2f, 1.39f, -2.4997f), new Vector3(90, 0, 0), new Vector3(0.5f, 1f, 0.0625f), RojoTierra);
            misObjetos.Add(Casco1);

            ObjetoDeLaEscena Casco2 = new ObjetoDeLaEscena();
            Casco2.SetearFileReader(GetComponent<FileReader>());
            Casco2.CrearObjeto("Baldosa", new Vector3(-2f, 1.41f, -2.4997f), new Vector3(90, 0, 0), new Vector3(0.625f, 1f, 0.0625f), RojoTierra);
            misObjetos.Add(Casco2);

            ObjetoDeLaEscena Casco3 = new ObjetoDeLaEscena();
            Casco3.SetearFileReader(GetComponent<FileReader>());
            Casco3.CrearObjeto("Baldosa", new Vector3(-2f, 1.43f, -2.4997f), new Vector3(90, 0, 0), new Vector3(0.75f, 1f, 0.0625f), RojoTierra);
            misObjetos.Add(Casco3);

            ObjetoDeLaEscena Casco4 = new ObjetoDeLaEscena();
            Casco4.SetearFileReader(GetComponent<FileReader>());
            Casco4.CrearObjeto("Baldosa", new Vector3(-2f, 1.45f, -2.4996f), new Vector3(90, 0, 0), new Vector3(0.875f, 1f, 0.0625f), RojoOscuro);
            misObjetos.Add(Casco4);

            ObjetoDeLaEscena Mastil1 = new ObjetoDeLaEscena();
            Mastil1.SetearFileReader(GetComponent<FileReader>());
            Mastil1.CrearObjeto("Baldosa", new Vector3(-2f, 1.593f, -2.4993f), new Vector3(90, 90, 0), new Vector3(0.510f, 1f, 0.0625f), Mastil);
            misObjetos.Add(Mastil1);

            // -- Velas --

            ObjetoDeLaEscena Vela1 = new ObjetoDeLaEscena();
            Vela1.SetearFileReader(GetComponent<FileReader>());
            Vela1.CrearObjeto("Baldosa", new Vector3(-2.01f, 1.49f, -2.4994f), new Vector3(90, 0, 0), new Vector3(0.247f, 1f, 0.046875f), BlancoHumo);
            misObjetos.Add(Vela1);

            ObjetoDeLaEscena Vela2 = new ObjetoDeLaEscena();
            Vela2.SetearFileReader(GetComponent<FileReader>());
            Vela2.CrearObjeto("Baldosa", new Vector3(-2.02f, 1.51f, -2.4996f), new Vector3(90, 0, 0), new Vector3(0.4345f, 1f, 0.046875f), BlancoHumo);
            misObjetos.Add(Vela2);

            ObjetoDeLaEscena Vela3 = new ObjetoDeLaEscena();
            Vela3.SetearFileReader(GetComponent<FileReader>());
            Vela3.CrearObjeto("Baldosa", new Vector3(-2.02f, 1.53f, -2.4996f), new Vector3(90, 0, 0), new Vector3(0.625f, 1f, 0.046875f), BlancoHumo);
            misObjetos.Add(Vela3);

            ObjetoDeLaEscena Vela4 = new ObjetoDeLaEscena();
            Vela4.SetearFileReader(GetComponent<FileReader>());
            Vela4.CrearObjeto("Baldosa", new Vector3(-2.017f, 1.55f, -2.4996f), new Vector3(90, 0, 0), new Vector3(0.58f, 1f, 0.046875f), BlancoHumo);
            misObjetos.Add(Vela4);

            ObjetoDeLaEscena Vela5 = new ObjetoDeLaEscena();
            Vela5.SetearFileReader(GetComponent<FileReader>());
            Vela5.CrearObjeto("Baldosa", new Vector3(-2.017f, 1.57f, -2.4996f), new Vector3(90, 0, 0), new Vector3(0.519f, 1f, 0.046875f), BlancoHumo);
            misObjetos.Add(Vela5);

            ObjetoDeLaEscena Vela6 = new ObjetoDeLaEscena();
            Vela6.SetearFileReader(GetComponent<FileReader>());
            Vela6.CrearObjeto("Baldosa", new Vector3(-2.017f, 1.59f, -2.4996f), new Vector3(90, 0, 0), new Vector3(0.458f, 1f, 0.046875f), BlancoHumo);
            misObjetos.Add(Vela6);

            ObjetoDeLaEscena Vela7 = new ObjetoDeLaEscena();
            Vela7.SetearFileReader(GetComponent<FileReader>());
            Vela7.CrearObjeto("Baldosa", new Vector3(-2.017f, 1.61f, -2.4996f), new Vector3(90, 0, 0), new Vector3(0.397f, 1f, 0.046875f), BlancoHumo);
            misObjetos.Add(Vela7);

            ObjetoDeLaEscena Vela8 = new ObjetoDeLaEscena();
            Vela8.SetearFileReader(GetComponent<FileReader>());
            Vela8.CrearObjeto("Baldosa", new Vector3(-2.017f, 1.63f, -2.4996f), new Vector3(90, 0, 0), new Vector3(0.336f, 1f, 0.046875f), BlancoHumo);
            misObjetos.Add(Vela8);

            ObjetoDeLaEscena Vela9 = new ObjetoDeLaEscena();
            Vela9.SetearFileReader(GetComponent<FileReader>());
            Vela9.CrearObjeto("Baldosa", new Vector3(-2.017f, 1.65f, -2.4996f), new Vector3(90, 0, 0), new Vector3(0.275f, 1f, 0.046875f), BlancoHumo);
            misObjetos.Add(Vela9);

            ObjetoDeLaEscena Vela10 = new ObjetoDeLaEscena();
            Vela10.SetearFileReader(GetComponent<FileReader>());
            Vela10.CrearObjeto("Baldosa", new Vector3(-2.017f, 1.67f, -2.4996f), new Vector3(90, 0, 0), new Vector3(0.214f, 1f, 0.046875f), BlancoHumo);
            misObjetos.Add(Vela10);

            ObjetoDeLaEscena Vela11 = new ObjetoDeLaEscena();
            Vela11.SetearFileReader(GetComponent<FileReader>());
            Vela11.CrearObjeto("Baldosa", new Vector3(-2.014f, 1.69f, -2.4996f), new Vector3(90, 0, 0), new Vector3(0.130f, 1f, 0.046875f), BlancoHumo);
            misObjetos.Add(Vela11);

            ObjetoDeLaEscena Mastil2 = new ObjetoDeLaEscena();
            Mastil2.SetearFileReader(GetComponent<FileReader>());
            Mastil2.CrearObjeto("Baldosa", new Vector3(-2.023f, 1.528f, -2.4995f), new Vector3(90, 90, 0), new Vector3(0.25f, 1f, 0.03125f), Cielo);
            misObjetos.Add(Mastil2);

            ObjetoDeLaEscena Mastil3 = new ObjetoDeLaEscena();
            Mastil3.SetearFileReader(GetComponent<FileReader>());
            Mastil3.CrearObjeto("Baldosa", new Vector3(-1.977f, 1.543f, -2.4995f), new Vector3(90, 90, 0), new Vector3(0.31f, 1f, 0.03125f), Cielo);
            misObjetos.Add(Mastil3);

            // -- Marco del Cuadro --

            ObjetoDeLaEscena MarcoCuadroIzquierdo = new ObjetoDeLaEscena();
            MarcoCuadroIzquierdo.SetearFileReader(GetComponent<FileReader>());
            MarcoCuadroIzquierdo.CrearObjeto("Baldosa", new Vector3(-2.375f, 1.5f, -2.4991f), new Vector3(90, 0, 0), new Vector3(0.0625f, 0.5f, 2f), CafeProfundo);
            misObjetos.Add(MarcoCuadroIzquierdo);

            ObjetoDeLaEscena MarcoCuadroDerecho = new ObjetoDeLaEscena();
            MarcoCuadroDerecho.SetearFileReader(GetComponent<FileReader>());
            MarcoCuadroDerecho.CrearObjeto("Baldosa", new Vector3(-1.62f, 1.5f, -2.4991f), new Vector3(90, 0, 0), new Vector3(0.0625f, 0.5f, 2f), CafeProfundo);
            misObjetos.Add(MarcoCuadroDerecho);

            ObjetoDeLaEscena MarcoInferior = new ObjetoDeLaEscena();
            MarcoInferior.SetearFileReader(GetComponent<FileReader>());
            MarcoInferior.CrearObjeto("Zocalo1mts", new Vector3(-2f, 1f, -2.4991f), new Vector3(0, 90, 0), new Vector3(1f, 0.25f, 0.75f), CafeProfundo);
            misObjetos.Add(MarcoInferior);

            ObjetoDeLaEscena MarcoSuperior = new ObjetoDeLaEscena();
            MarcoSuperior.SetearFileReader(GetComponent<FileReader>());
            MarcoSuperior.CrearObjeto("Zocalo1mts", new Vector3(-2f, 1.96875f, -2.4991f), new Vector3(0, 90, 0), new Vector3(1f, 0.25f, 0.75f), CafeProfundo);
            misObjetos.Add(MarcoSuperior);

            UnityEngine.Color ColorCristal = new UnityEngine.Color(0.5f, 0.7f, 1.0f, 0.2f);

            ObjetoDeLaEscena Cristal = new ObjetoDeLaEscena();
            Cristal.SetearFileReader(GetComponent<FileReader>());
            Cristal.CrearObjeto("Baldosa", new Vector3(-2f, 1.5f, -2.4992f), new Vector3(90, 0, 0), new Vector3(1.5f, 1f, 2f), ColorCristal );
            misObjetos.Add(Cristal);


        //reloj
        // base
        ObjetoDeLaEscena BaseReloj = new ObjetoDeLaEscena();
        BaseReloj.SetearFileReader(GetComponent<FileReader>());
        BaseReloj.CrearObjeto("Zocalo1mts", new Vector3(1.49f, 1.45f, 0.04f), new Vector3(0, 180, 0), new Vector3(0.5f, 2f, 0.28f), BlancoHumo);
        misObjetos.Add(BaseReloj);

        // aguja larga
        ObjetoDeLaEscena AgujaLarga = new ObjetoDeLaEscena();
        AgujaLarga.SetearFileReader(GetComponent<FileReader>());
        AgujaLarga.CrearObjeto("Zocalo1mts", new Vector3(1.48f, 1.5f,0.038f), new Vector3(0, 0, 0), new Vector3(0.09f, 1.1f, 0.01f), Color.black);
        misObjetos.Add(AgujaLarga);
       

        // aguja corta
        ObjetoDeLaEscena AgujaCorta = new ObjetoDeLaEscena();
        AgujaCorta.SetearFileReader(GetComponent<FileReader>());
        AgujaCorta.CrearObjeto("Zocalo1mts", new Vector3(1.48f, 1.5f, 0.038f), new Vector3(45, 160, 0), new Vector3(0.09f, 0.8f, 0.01f), Color.black);
        misObjetos.Add(AgujaCorta);




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
        if (Input.GetKeyDown(KeyCode.T) && elTecho != null)
        {
            elTecho.activo = !elTecho.activo;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (ObjetoDeLaEscena p in lasParedes)
            {
                p.activo = !p.activo; // Apaga o prende todas juntas
            }
        }

        Matrix4x4 viewMat = cc.ObtenerMatrizVista();

        Matrix4x4 projMat = GL.GetGPUProjectionMatrix(
        Matrices.CalculatePerspectiveProjectionMatrix(fov, aspectRatio, nearClipPlane, farClipPlane),
        true
        );

        // se las pasamos a cada objeto de la lista
       /* foreach (ObjetoDeLaEscena obj in misObjetos)
        {
            if (obj.activo)
            {
                obj.Dibujar(viewMat, projMat);
            }
        }*/
        foreach (ObjetoDeLaEscena obj in misObjetos)
        {
            // Llamamos a todos. El que no esté activo se apagará solo adentro del método Dibujar.
            obj.Dibujar(viewMat, projMat);
        }
    }
}

