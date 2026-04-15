using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileReader : MonoBehaviour
{
    public Vector3[] vertices;
    public int[] triangles;
    public Mesh Aretornar;

    public Mesh ProcesarArchivo(string fileName)
    {

        // Start is called before the first frame update
        string path = "Assets/Modelos3d/" + fileName + ".obj";

        StreamReader reader = new StreamReader(path);
        string fileData = (reader.ReadToEnd());

        reader.Close();
        Debug.Log(fileData);


        List<Vector3> verticesLista = new List<Vector3>();
        List<int> carasLista = new List<int>();


        string[] lines = fileData.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].StartsWith("v "))
            {


                // Separamos la l�nea por espacios: "v", "1.0", "2.0", "0.5"
                string[] partes = lines[i].Split(' ');

                // El �ndice 0 es la "v", los n�meros est�n en 1, 2 y 3
                // Cambiamos el punto por coma para que tu Windows lo entienda como decimal. ERA ESTO!!!!!!!!!!
                float x = float.Parse(partes[1].Replace(".", ","));
                float y = float.Parse(partes[2].Replace(".", ","));
                float z = float.Parse(partes[3].Replace(".", ","));

                verticesLista.Add(new Vector3(x, y, z));

            }
            else if (lines[i].StartsWith("f "))
            {
                // 1. Dividimos la l�nea por espacios
                string[] partes = lines[i].Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

                // 2. Creamos una lista para los �ndices de esta cara
                List<int> faceIndices = new List<int>();

                for (int j = 1; j < partes.Length; j++)
                {
                    // Tomamos solo el n�mero antes de la primera "/"
                    string[] subPartes = partes[j].Split('/');
                    int vIndex = int.Parse(subPartes[0]) - 1; // Ajustamos el �ndice (OBJ empieza en 1, Unity en 0)
                    faceIndices.Add(vIndex);
                }

                // 3. SI ES UN TRI�NGULO (Como la cama)
                if (faceIndices.Count == 3)
                {
                    carasLista.Add(faceIndices[0]);
                    carasLista.Add(faceIndices[1]);
                    carasLista.Add(faceIndices[2]);
                }
                // 4. SI ES UN CUADRADO (Como la mesa)
                else if (faceIndices.Count == 4)
                {
                    // Tri�ngulo A
                    carasLista.Add(faceIndices[0]);
                    carasLista.Add(faceIndices[2]);
                    carasLista.Add(faceIndices[1]);

                    // Tri�ngulo B
                    carasLista.Add(faceIndices[0]);
                    carasLista.Add(faceIndices[3]);
                    carasLista.Add(faceIndices[2]);
                }
            }
        }

        // Al final del m�todo ReadEachLine:
        vertices = verticesLista.ToArray();
        triangles = carasLista.ToArray();

        CalcularCentro();

        Aretornar = new Mesh();
        Aretornar.vertices = vertices;
        Aretornar.triangles = triangles;

        return (Aretornar);

    }
    private void CalcularCentro() //para centarr la cama en el origen, calculo el centro con vertice mas lejano y mas cercano y promedio
    {
        Vector3 min = vertices[0];
        Vector3 max = vertices[0];

        for (int i = 1; i < vertices.Length; i++)
        {
            min = Vector3.Min(min, vertices[i]);
            max = Vector3.Max(max, vertices[i]);
        }
        Vector3 centro = (min + max) / 2f;
        centro.y = min.y ;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] -= centro;
        }
    }
}