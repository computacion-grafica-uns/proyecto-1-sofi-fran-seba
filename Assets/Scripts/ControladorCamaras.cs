
using UnityEngine;

public class ControladorCamras
{
    public enum ModoCamara { Orbital, PrimeraPersona }
    public ModoCamara modoActual = ModoCamara.Orbital;

    // Para Primera Persona
    public Vector3 posPersona = new Vector3(0, 1, 7); // Altura de los ojos
    private float yaw = 180f;   // Rotaci�n horizontal (mirar a los lados)
    private float pitch = 0f; // Rotaci�n vertical (mirar arriba/abajo)
    // Variables para el Zoom en Primera Persona
    private float fovActual = 40f;
    private const float fovMin = 5f; // M�ximo Zoom
    private const float fovMax = 60f; // Vista normal/amplia
    private float offsetRotacion = 0f;

    // Variables de estado
    public float radio = 30f;
    public float anguloH = 0f;
    public float anguloV = 0.5f;
    public bool rotarSola = false;

    // M�todo para procesar el input (se llama desde el Update del SceneManager)
    public void ProcesarInput()
    {

        // Cambiar de c�mara con la tecla C
        if (Input.GetKeyDown(KeyCode.C))
        {
            modoActual = (modoActual == ModoCamara.Orbital) ? ModoCamara.PrimeraPersona : ModoCamara.Orbital;
        }

        if (modoActual == ModoCamara.Orbital)
        {
            ProcesarOrbital(); // Tu l�gica que ya funciona
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

        // Rotaci�n manual con A/D
        if (Input.GetKey(KeyCode.D)) anguloH += vel;
        if (Input.GetKey(KeyCode.A)) anguloH -= vel;

        // Switch de rotaci�n autom�tica
        if (Input.GetKeyDown(KeyCode.R)) rotarSola = !rotarSola;
        if (rotarSola) anguloH += vel * 0.5f;

    }

   private void ProcesarPrimeraPersona()
{
    // 1. CONFIGURACIÓN
    float velMov = 2f * Time.deltaTime;
    float sensibilidadMouse = 0.1f; //----------------------------------------------- SENSIBILIDAD DE LA CAMARAAAAAAAAAAAAAAAA

    // 2. ROTACIÓN (Corregido: += para que sea intuitivo)
    // Cambiamos el signo aquí para que Mouse Derecha sea Mirar Derecha
    yaw += - Input.GetAxis("Mouse X") * sensibilidadMouse;
    
    // El pitch suele estar bien restando (Mouse arriba = Mirar arriba)
    pitch -= Input.GetAxis("Mouse Y") * sensibilidadMouse; 
    pitch = Mathf.Clamp(pitch, -1.4f, 1.4f); 

    // 3. CÁLCULO DE EJES
    // Aseguramos que el movimiento sea relativo a la nueva rotación
    Vector3 forward = new Vector3(Mathf.Sin(yaw), 0, Mathf.Cos(yaw));
    Vector3 right = new Vector3(Mathf.Cos(yaw), 0, -Mathf.Sin(yaw));

    // 4. MOVIMIENTO WASD
    if (Input.GetKey(KeyCode.W)) posPersona += forward * velMov;
    if (Input.GetKey(KeyCode.S)) posPersona -= forward * velMov;
    
    // Corregimos también la lateralidad aquí
    if (Input.GetKey(KeyCode.D)) posPersona += - right * velMov; // D ahora es Derecha
    if (Input.GetKey(KeyCode.A)) posPersona -= - right * velMov; // A ahora es Izquierda

    // 5. BLOQUEO FORZADO DEL CURSOR
    // En el editor de Unity, a veces hace falta reforzar el bloqueo
    if (Cursor.lockState != CursorLockMode.Locked)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; //para que no se vea el cursor en unity
    }
}

    // M�todo que devuelve la matriz de vista calculada
    public Matrix4x4 ObtenerMatrizVista()
    {
        if (modoActual == ModoCamara.Orbital)
        {
            float x = radio * Mathf.Cos(anguloV) * Mathf.Sin(anguloH);
            float y = radio * Mathf.Sin(anguloV);
            float z = radio * Mathf.Cos(anguloV) * Mathf.Cos(anguloH);

            Vector3 camPos = new Vector3(x, y, z);
            Vector3 target = Vector3.zero; // Posici�n determinada en el espacio [cite: 4]
            Vector3 up = Vector3.up;

            return Matrices.CreateViewMatrix(camPos, target, up);
        }
        else
        {
            // CALCULO PRIMERA PERSONA
            // El target es un punto justo adelante de la c�mara
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