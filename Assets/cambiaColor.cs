using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambiaColor : MonoBehaviour
{
    // Este m�todo se llama cuando este objeto colisiona con otro objeto
    void OnCollisionEnter(Collision collision)
    {
        // Comprueba si el objeto con el que colision� es una bala
        if (collision.gameObject.tag == "Proyectil")
        {
            // Genera valores RGB aleatorios
            float r = Random.Range(0f, 1f);
            float g = Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);

            // Aplica el color aleatorio al objeto
            GetComponent<Renderer>().material.color = new Color(r, g, b);
        }
    }
}
