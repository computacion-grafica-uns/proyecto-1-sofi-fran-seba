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


                // Separamos la línea por espacios: "v", "1.0", "2.0", "0.5"
                string[] partes = lines[i].Split(' ');

                // El índice 0 es la "v", los números están en 1, 2 y 3
                // Cambiamos el punto por coma para que tu Windows lo entienda como decimal. ERA ESTO!!!!!!!!!!
                float x = float.Parse(partes[1].Replace(".", ","));
                float y = float.Parse(partes[2].Replace(".", ","));
                float z = float.Parse(partes[3].Replace(".", ","));

                verticesLista.Add(new Vector3(x, y, z));

            }
            else if (lines[i].StartsWith("f "))
            {
                // 1. Spliteamos por espacio pero eliminamos las entradas vacías (por los dobles espacios)
                string[] partes = lines[i].Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

                // El índice 0 es "f". Empezamos desde el 1 al 3 para los 3 vértices del triángulo
                for (int j = 1; j <= 3; j++)
                {
                    // 2. Spliteamos cada parte por la barra '/' (ej: "48/1/5" -> ["48", "1", "5"])
                    string[] subPartes = partes[j].Split('/');

                    // 3. Tomamos solo el primer valor, que es el índice del vértice
                    int indice = int.Parse(subPartes[0]) - 1;

                    carasLista.Add(indice);
                }
            }
        }


        // Al final del método ReadEachLine:
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
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] -= centro;
        }
    }
}