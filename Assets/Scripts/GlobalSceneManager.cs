using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManagerProy : MonoBehaviour
{
    // Start is called before the first frame update
    float fov = 90f;
    float aspectRatio = 16f / 9f;
    float nearClipPlane = 0.1f;
    float farClipPlane = 1000f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 1. Calculamos las matrices GLOBALES una sola vez
        Vector3 posCam = new Vector3(0, 40, -100);
        Vector3 target = Vector3.zero;
        Vector3 up = Vector3.up;

        Matrix4x4 viewMat = Matrices.CreateViewMatrix(posCam, target, up);
        Matrix4x4 projMat = GL.GetGPUProjectionMatrix(
            Matrices.CalculatePerspectiveProjectionMatrix(fov, aspectRatio, nearClipPlane, farClipPlane),
            true
        );

        // 2. Se las pasamos a cada objeto
        foreach (Objeto3D obj in misObjetos)
        {
            // El objeto usará estas dos globales + su propia matriz de modelo
            obj.Dibujar(viewMat, projMat);
        }
    }
}
