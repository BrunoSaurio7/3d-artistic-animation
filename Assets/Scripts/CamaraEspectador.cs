using UnityEngine;

public class CamaraEspectador : MonoBehaviour
{
    public float velocidadMovimiento = 10.0f;
    public float velocidadRotacion = 2.0f;

    private float rotacionX = 0.0f;
    private float rotacionY = 0.0f;

    void Start()
    {
        // CAPTURA SEGURA DEL ÁNGULO INICIAL
        // Esto evita que la cámara de un "salto" a 0,0,0 cuando haces clic.
        Vector3 angulosIniciales = transform.localEulerAngles;
        rotacionX = angulosIniciales.x;
        rotacionY = angulosIniciales.y;

        // Corrección técnica: Unity usa 0-360 grados. Si miras un poco abajo, Unity dice 350°.
        // Nosotros queremos leerlo como -10°. Esto lo arregla:
        if (rotacionX > 180) rotacionX -= 360;
        if (rotacionY > 180) rotacionY -= 360;
    }

    void Update()
    {
        // 1. MOVIMIENTO (W, A, S, D, Q, E)
        float movZ = 0; // Adelante/Atras
        float movX = 0; // Izquierda/Derecha
        float movY = 0; // Arriba/Abajo

        if (Input.GetKey(KeyCode.W)) movZ = 1;
        if (Input.GetKey(KeyCode.S)) movZ = -1;
        if (Input.GetKey(KeyCode.A)) movX = -1;
        if (Input.GetKey(KeyCode.D)) movX = 1;
        if (Input.GetKey(KeyCode.Q)) movY = -1;
        if (Input.GetKey(KeyCode.E)) movY = 1;

        // Moverse relativo a hacia donde mira la cámara
        Vector3 movimiento = transform.right * movX + transform.forward * movZ + transform.up * movY;
        transform.position += movimiento * velocidadMovimiento * Time.deltaTime;

        // 2. ROTACIÓN (Solo con Clic Derecho presionado)
        if (Input.GetMouseButton(1)) 
        {
            // Sumamos el movimiento del mouse a nuestras variables
            rotacionY += Input.GetAxis("Mouse X") * velocidadRotacion;
            rotacionX -= Input.GetAxis("Mouse Y") * velocidadRotacion;
            
            // Limitamos para no dar vueltas completas verticales (evita cuello roto)
            rotacionX = Mathf.Clamp(rotacionX, -90, 90);

            // Aplicamos la rotación
            transform.localEulerAngles = new Vector3(rotacionX, rotacionY, 0.0f);
        }
    }
}