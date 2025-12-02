using UnityEngine;

public class SoltarArma : MonoBehaviour
{
    public GameObject arma; // Arrastra aquí el objeto del arma en el Inspector
    public float tiempoParaSoltar = 5.0f; // Segundos antes de soltarla
    private bool yaSolto = false;

    void Update()
    {
        // Cuenta regresiva simple
        if (tiempoParaSoltar > 0)
        {
            tiempoParaSoltar -= Time.deltaTime;
        }
        else if (!yaSolto)
        {
            Soltar();
        }
    }

    void Soltar()
    {
        yaSolto = true;

        // 1. Desvincular el arma del brazo (ya no es hija)
        arma.transform.parent = null; 

        // 2. Activar la gravedad
        Rigidbody rb = arma.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Ahora la física controla el arma
            rb.useGravity = true;
            
            // Opcional: Darle un empujoncito hacia adelante
            rb.AddTorque(Random.insideUnitSphere * 10f);
        }
    }
}