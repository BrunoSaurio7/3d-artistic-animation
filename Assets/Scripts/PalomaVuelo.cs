using UnityEngine;

public class PalomaOrbitaManual : MonoBehaviour
{
    [Header("Configuración del Círculo")]
    public Transform centroEscena;    
    [Range(1f, 50f)] 
    public float radio = 10.0f;       
    public float velocidad = 2.0f;    

    [Header("Corrección Manual de Rotación")]
    // DESLIZA ESTO HASTA QUE MIRE BIEN
    [Range(0f, 360f)]
    public float rotacionManual = 0.0f; 

    // Variables internas
    private float alturaFija;
    private float anguloActual = 0f;

    // Aleteo
    private Vector3 escalaOriginal;
    public Transform cuerpoPaloma; 

    void Start()
    {
        alturaFija = transform.position.y;
        if (cuerpoPaloma != null) escalaOriginal = cuerpoPaloma.localScale;
    }

    void Update()
    {
        // 1. MOVIMIENTO (Seno y Coseno)
        anguloActual += velocidad * Time.deltaTime;
        
        float x = Mathf.Cos(anguloActual) * radio;
        float z = Mathf.Sin(anguloActual) * radio;

        Vector3 centroPos = (centroEscena != null) ? centroEscena.position : Vector3.zero;
        transform.position = new Vector3(centroPos.x + x, alturaFija, centroPos.z + z);

        // 2. ROTACIÓN MATEMÁTICA (Sin LookAt)
        // Convertimos el ángulo del círculo (radianes) a grados
        // El signo negativo es porque Unity rota en sentido horario vs trigonométrico
        float rotacionDelCirculo = -anguloActual * Mathf.Rad2Deg;

        // Sumamos TU ajuste manual. 
        // Si el modelo viene al revés (180), tú lo arreglas con el slider.
        transform.rotation = Quaternion.Euler(0, rotacionDelCirculo + rotacionManual, 0);

        // 3. ALETEO
        if (cuerpoPaloma != null)
        {
            float aleteo = Mathf.Sin(Time.time * 20f) * 0.2f;
            Vector3 nuevaEscala = escalaOriginal;
            nuevaEscala.y += aleteo;
            nuevaEscala.x -= aleteo * 0.5f;
            cuerpoPaloma.localScale = nuevaEscala;
        }
    }
}