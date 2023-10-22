using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionesMovimiento : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 90.0f;
    public float suavizado = 0.1f;
    Animator anim;
    Vector3 direccionMovimiento;

    // Variable para rastrear si se está presionando el botón izquierdo del mouse
    bool estaPresionandoMouse;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 direccionDeseada = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);
        direccionMovimiento = Vector3.Lerp(direccionMovimiento, direccionDeseada, suavizado);

        if (direccionMovimiento.magnitude > 0.1f)
        {
            Quaternion nuevaRotacion = Quaternion.LookRotation(direccionMovimiento);
            transform.rotation = Quaternion.Slerp(transform.rotation, nuevaRotacion, velocidadRotacion * Time.deltaTime);
            transform.Translate(direccionMovimiento.normalized * velocidadMovimiento * Time.deltaTime, Space.World);

            float yRotation = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, yRotation, 0);

            anim.SetFloat("movimientos", Mathf.Lerp(anim.GetFloat("movimientos"), 0.5f, 0.05f));
        }
        else
        {
            // Si se está presionando el botón izquierdo del mouse, establece "movimientos" en 1.0
            if (estaPresionandoMouse)
            {
                anim.SetFloat("movimientos", 1.0f);

                // Actualiza la rotación hacia la posición del mouse
                ApuntarAlMouse();
            }
            else
            {
                // De lo contrario, suaviza la transición de la animación hacia 0
                float movimientosAnterior = anim.GetFloat("movimientos");
                float movimientosSuavizado = Mathf.Lerp(movimientosAnterior, 0.0f, 0.05f);
                anim.SetFloat("movimientos", movimientosSuavizado);
            }
        }

        // Verifica si se está presionando el botón izquierdo del mouse (botón 0)
        estaPresionandoMouse = Input.GetMouseButton(0);
    }

    void ApuntarAlMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direccionMira = hit.point - transform.position;
            direccionMira.y = 0.0f; // Asegura que la mira esté en el plano horizontal
            Quaternion rotacionMira = Quaternion.LookRotation(direccionMira);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionMira, velocidadRotacion * Time.deltaTime);
        }
    }
}
