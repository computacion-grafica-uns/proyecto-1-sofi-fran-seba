using Assets.Scripts;
using UnityEngine;

public class ControladorCamras
{
    public enum ModoCamara { Orbital, PrimeraPersona }
    public ModoCamara modoActual = ModoCamara.Orbital;

    // Para Primera Persona
    public Vector3 posPersona = new Vector3(0, 1, 10); // Altura de los ojos
    private float yaw = 0f;   // Rotación horizontal (mirar a los lados)
    private float pitch = 0f; // Rotación vertical (mirar arriba/abajo)
    // Variables para el Zoom en Primera Persona
    private float fovActual = 60f;
    private const float fovMin = 20f; // Máximo Zoom
    private const float fovMax = 60f; // Vista normal/amplia

    // Variables de estado
    public float radio = 30f;
    public float anguloH = 0f;
    public float anguloV = 0.5f;
    public bool rotarSola = false;

    // Método para procesar el input (se llama desde el Update del SceneManager)
    public void ProcesarInput()
    {

        // Cambiar de cámara con la tecla C
        if (Input.GetKeyDown(KeyCode.C))
        {
            modoActual = (modoActual == ModoCamara.Orbital) ? ModoCamara.PrimeraPersona : ModoCamara.Orbital;
        }

        if (modoActual == ModoCamara.Orbital)
        {
            ProcesarOrbital(); // Tu lógica que ya funciona
        }
        else
        {
            ProcesarPrimeraPersona();
        }
    }
    private void ProcesarOrbital()
    {
        float vel = 2.0f * Time.deltaTime;

        // Zoom con flechitas
        if (Input.GetKey(KeyCode.UpArrow)) radio -= 10f * Time.deltaTime;
        if (Input.GetKey(KeyCode.DownArrow)) radio += 10f * Time.deltaTime;
        radio = Mathf.Clamp(radio, 5f, 100f);

        // Rotación manual con A/D
        if (Input.GetKey(KeyCode.D)) anguloH += vel;
        if (Input.GetKey(KeyCode.A)) anguloH -= vel;

        // Switch de rotación automática
        if (Input.GetKeyDown(KeyCode.R)) rotarSola = !rotarSola;
        if (rotarSola) anguloH += vel * 0.5f;

    }
    private void ProcesarPrimeraPersona()
    {
        float velMov = 2f * Time.deltaTime;
        float velRot = 2f * Time.deltaTime;

        // ROTACIÓN (A/D para girar, W/S para mirar arriba/abajo)
        if (Input.GetKey(KeyCode.D)) yaw += velRot;
        if (Input.GetKey(KeyCode.A)) yaw -= velRot;
        if (Input.GetKey(KeyCode.W)) pitch -= velRot;
        if (Input.GetKey(KeyCode.S)) pitch += velRot;
        pitch = Mathf.Clamp(pitch, -1.4f, 1.4f); // No dejar que se nuca

        // MOVIMIENTO (Flechas adelante/atrás)
        // Calculamos hacia donde estamos mirando para movernos en esa dirección
        Vector3 direccionFwd = new Vector3(Mathf.Sin(yaw), 0, Mathf.Cos(yaw));

        if (Input.GetKey(KeyCode.UpArrow)) posPersona += direccionFwd * velMov;
        if (Input.GetKey(KeyCode.DownArrow)) posPersona -= direccionFwd * velMov;

        // --- ZOOM EN PRIMERA PERSONA (Cambiando FOV) ---
        if (Input.GetKey(KeyCode.UpArrow)) fovActual -= 30f * Time.deltaTime;   // Zoom In
        if (Input.GetKey(KeyCode.DownArrow)) fovActual += 30f * Time.deltaTime; // Zoom Out

        // Limitamos para que no se de vuelta la imagen
        fovActual = Mathf.Clamp(fovActual, fovMin, fovMax);
    }
    // Método que devuelve la matriz de vista calculada
    public Matrix4x4 ObtenerMatrizVista()
    {
        if (modoActual == ModoCamara.Orbital)
        {
            float x = radio * Mathf.Cos(anguloV) * Mathf.Sin(anguloH);
            float y = radio * Mathf.Sin(anguloV);
            float z = radio * Mathf.Cos(anguloV) * Mathf.Cos(anguloH);

            Vector3 camPos = new Vector3(x, y, z);
            Vector3 target = Vector3.zero; // Posición determinada en el espacio [cite: 4]
            Vector3 up = Vector3.up;

            return Matrices.CreateViewMatrix(camPos, target, up);
        }
        else
        {
            // CALCULO PRIMERA PERSONA
            // El target es un punto justo adelante de la cámara
            Vector3 forward = new Vector3(
                Mathf.Cos(pitch) * Mathf.Sin(yaw),
                -Mathf.Sin(pitch),
                Mathf.Cos(pitch) * Mathf.Cos(yaw)
            );

            Vector3 target = posPersona + forward;
            return Matrices.CreateViewMatrix(posPersona, target, Vector3.up);
        }
    }
}