using UnityEngine;

public class ControladorEscena : MonoBehaviour
{
    // REFERENCIAS A LAS LUCES (Arrastrar desde la Jerarquía al Inspector)
    public Light luzOmniTeclado;    // Requisito: se mueve con teclado
    public Light luzReflectorMouse; // Requisito: se mueve con mouse
    
    // VELOCIDAD
    public float velocidadTeclado = 5.0f;

    void Update()
    {
        MoverLuzTeclado();
        MoverLuzMouse();
        SalirAplicacion();
    }

    // LÓGICA CORREGIDA: Usamos KeyCode para que SOLO responda a las Flechas
    void MoverLuzTeclado()
    {
        float movX = 0;
        float movZ = 0;

        if (Input.GetKey(KeyCode.RightArrow)) movX = 1;
        if (Input.GetKey(KeyCode.LeftArrow))  movX = -1;
        if (Input.GetKey(KeyCode.UpArrow))    movZ = 1;
        if (Input.GetKey(KeyCode.DownArrow))  movZ = -1;
        
        // Movemos la luz manteniendo su altura Y original
        if (luzOmniTeclado != null)
        {
            luzOmniTeclado.transform.Translate(new Vector3(movX, 0, movZ) * velocidadTeclado * Time.deltaTime);
        }
    }

    // LÓGICA: Mover Reflector Superior con el Mouse
    void MoverLuzMouse()
    {
        if (luzReflectorMouse == null) return;

        // Lanzamos un rayo desde la cámara hacia donde apunta el mouse
        Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit golpe;

        // Si choca con el suelo (que debe tener un Collider), movemos la luz ahí
        if (Physics.Raycast(rayo, out golpe))
        {
            Vector3 nuevaPos = golpe.point;
            nuevaPos.y = luzReflectorMouse.transform.position.y; // Mantiene su altura
            luzReflectorMouse.transform.position = nuevaPos;
        }
    }
    
    // Requisito: Cerrar aplicación con ESC
    void SalirAplicacion()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Saliendo de la aplicación..."); 
        }
    }

    // Requisito: Menú en ventana de interacción
    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 300, 150), "Instrucciones de Control");
        
        GUI.Label(new Rect(20, 40, 280, 20), "• CAMARA: W-A-S-D (Mover) + Clic Der (Rotar)");
        GUI.Label(new Rect(20, 60, 280, 20), "• LUZ 1: Flechas del Teclado");
        GUI.Label(new Rect(20, 80, 280, 20), "• LUZ 2: Puntero del Mouse");
        GUI.Label(new Rect(20, 100, 280, 20), "• SALIR: Tecla ESC");
        
        // Requisito: Mostrar coordenadas de la luz móvil
        if(luzOmniTeclado != null) {
            string coords = luzOmniTeclado.transform.position.ToString("F2");
            GUI.Label(new Rect(20, 120, 280, 20), $"Pos. Luz Teclado: {coords}");
        }
    }
}