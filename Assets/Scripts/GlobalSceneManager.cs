using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManagerProy : MonoBehaviour
{
    private List<objeto> misObjetos = new List<objeto>();

    float fov = 90f;
    float aspectRatio = 16f / 9f;
    float nearClipPlane = 0.1f;
    float farClipPlane = 1000f;
    void Start()
    {
        //pruebo con la cama:
        objeto cama = gameObject.AddComponent<objeto>();
        cama.CrearObjeto("Bed1", new Vector3(0, 0, 0),new Vector3(0,0,0), new Vector3(1,1,1), "ShaderBasico");
        misObjetos.Add(cama);
    }

    void Update()
    {
        // calculamos las matrices GLOBALES una sola vez
        Vector3 posCam = new Vector3(0, 40, -100);
        Vector3 target = Vector3.zero;
        Vector3 up = Vector3.up;

        Matrix4x4 viewMat = Matrices.CreateViewMatrix(posCam, target, up);
        Matrix4x4 projMat = GL.GetGPUProjectionMatrix(
            Matrices.CalculatePerspectiveProjectionMatrix(fov, aspectRatio, nearClipPlane, farClipPlane),
            true
        );

        // se las pasamos a cada objeto de la lista
        foreach (objeto obj in misObjetos)
        {
            obj.Dibujar(viewMat, projMat);
        }
    }
}
