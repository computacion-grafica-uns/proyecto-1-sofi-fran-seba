using UnityEngine;

public class GlobalSceneManager : MonoBehaviour
{
    private ControladorCamras cc;

    void Start()
    {
        cc = new ControladorCamras();
    }

    void Update()
    {
        // 1. Procesamos el teclado y mouse una sola vez por frame
        cc.ProcesarInput();

        // 2. Obtenemos la Matriz de Vista
        Matrix4x4 viewMat = cc.ObtenerMatrizVista();

        // 3. Calculamos la Matriz de Proyección (usando el FOV dinámico del zoom)
        float fovActual = (cc.modoActual == ControladorCamras.ModoCamara.Orbital) ? 60f : cc.fovActual;
        float aspect = (float)Screen.width / (float)Screen.height;

        // Usamos tu método de la clase Matrices
        Matrix4x4 projMat = Matrices.CalculatePerspectiveProjectionMatrix(fovActual, aspect, 0.3f, 1000f);
        Matrix4x4 gpuProj = GL.GetGPUProjectionMatrix(projMat, true);

        // --- EL SECRETO PARA MUCHOS OBJETOS ---
        // Seteamos las matrices de forma GLOBAL. 
        // Todos los shaders que tengan _ViewMatrix y _ProjectionMatrix se actualizarán solos.
        Shader.SetGlobalMatrix("_ViewMatrix", viewMat);
        Shader.SetGlobalMatrix("_ProjectionMatrix", gpuProj);
    }
}