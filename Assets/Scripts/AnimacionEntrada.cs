using UnityEngine;

public class SoldadoCaminata : MonoBehaviour
{
    [Header("Configuración General")]
    public Transform centroEscena;   // El objeto vacío en (0,0,0)
    public float velocidad = 3.0f;   // Qué tan rápido caminan
    public float radioCirculo = 7.0f; // El radio pedido de 7 metros

    [Header("Configuración de Órbita")]
    public bool sentidoReloj = true; // Marcar para girar a la derecha, desmarcar para izquierda
    
    private bool enOrbita = false;

    void Update()
    {
        if (centroEscena == null) return;

        // Calculamos la distancia actual al centro
        float distancia = Vector3.Distance(transform.position, centroEscena.position);

        if (!enOrbita)
        {
            // FASE 1: ACERCARSE AL CÍRCULO
            // Si estamos lejos (más de 7 metros), caminamos hacia el borde del círculo
            if (distancia > radioCirculo)
            {
                // Miramos hacia el centro
                transform.LookAt(new Vector3(centroEscena.position.x, transform.position.y, centroEscena.position.z));
                
                // Caminamos hacia adelante
                transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
            }
            else
            {
                // FASE DE TRANSICIÓN: Justo tocamos el radio 7
                EntrarEnOrbita();
            }
        }
        else
        {
            // FASE 2: ORBITAR (DAR VUELTAS)
            // RotateAround mueve el objeto alrededor de un punto eje
            // Fórmula: (Punto, Eje, Grados)
            float direccion = sentidoReloj ? 1.0f : -1.0f;
            
            // Calculamos cuántos grados girar basado en la velocidad lineal
            // Velocidad Angular = (Velocidad / Radio) * Radianes a Grados
            float velocidadAngular = (velocidad / radioCirculo) * Mathf.Rad2Deg;

            transform.RotateAround(centroEscena.position, Vector3.up, direccion * velocidadAngular * Time.deltaTime);
            
            // Aseguramos que el soldado mire hacia adelante en la curva y no se desalinee
            CorregirRotacionOrbita();
        }
    }

    void EntrarEnOrbita()
    {
        enOrbita = true;
        
using UnityEngine;

public class SoldadoCaminata : MonoBehaviour
{
    [Header("Configuración General")]
    public Transform centroEscena;   // El objeto vacío en (0,0,0)
    public float velocidad = 3.0f;   // Qué tan rápido caminan
    public float radioCirculo = 7.0f; // El radio pedido de 7 metros

    [Header("Configuración de Órbita")]
    public bool sentidoReloj = true; // Marcar para girar a la derecha, desmarcar para izquierda
    
    private bool enOrbita = false;

    void Update()
    {
        if (centroEscena == null) return;

        // Calculamos la distancia actual al centro
        float distancia = Vector3.Distance(transform.position, centroEscena.position);

        if (!enOrbita)
        {
            // FASE 1: ACERCARSE AL CÍRCULO
            // Si estamos lejos (más de 7 metros), caminamos hacia el borde del círculo
            if (distancia > radioCirculo)
            {
                // Miramos hacia el centro
                transform.LookAt(new Vector3(centroEscena.position.x, transform.position.y, centroEscena.position.z));
                
                // Caminamos hacia adelante
                transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
            }
            else
            {
                // FASE DE TRANSICIÓN: Justo tocamos el radio 7
                EntrarEnOrbita();
            }
        }
        else
        {
            // FASE 2: ORBITAR (DAR VUELTAS)
            // RotateAround mueve el objeto alrededor de un punto eje
            // Fórmula: (Punto, Eje, Grados)
            float direccion = sentidoReloj ? 1.0f : -1.0f;
            
            // Calculamos cuántos grados girar basado en la velocidad lineal
            // Velocidad Angular = (Velocidad / Radio) * Radianes a Grados
            float velocidadAngular = (velocidad / radioCirculo) * Mathf.Rad2Deg;

            transform.RotateAround(centroEscena.position, Vector3.up, direccion * velocidadAngular * Time.deltaTime);
            
            // Aseguramos que el soldado mire hacia adelante en la curva y no se desalinee
            CorregirRotacionOrbita();
        }
    }

    void EntrarEnOrbita()
    {
        enOrbita = true;
        
        // Al entrar al círculo, giramos 90 grados para no quedar mirando al centro
        // sino mirando hacia el camino del círculo (la tangente)
        float anguloGiro = sentidoReloj ? -90.0f : 90.0f;
        transform.Rotate(0, anguloGiro, 0);
    }

    void CorregirRotacionOrbita()
    {
        // Forzamos al soldado a mirar en la dirección del movimiento circular
        // Obtenemos el vector desde el centro hasta el soldado
        Vector3 radioVector = transform.position - centroEscena.position;
        
        // La tangente (dirección de marcha) es perpendicular al radio
        Vector3 tangente = sentidoReloj ? 
            Vector3.Cross(Vector3.up, radioVector) : 
            Vector3.Cross(radioVector, Vector3.up);

        if (tangente != Vector3.zero)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(tangente);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 5f);
        }
    }

    // NUEVO: Método para detener la animación y resaltar el momento final
    public void Detenerse()
    {
        // Detenemos el movimiento
        velocidad = 0;
        enOrbita = false; // Dejamos de calcular órbita

        // Opcional: Mirar hacia la cámara principal para un final dramático
        if (Camera.main != null)
        {
            // Solo rotamos en Y para que no se incline raro
            Vector3 direccionCamara = Camera.main.transform.position - transform.position;
            direccionCamara.y = 0; 
            if (direccionCamara != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direccionCamara);
            }
        }
    }
}