using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collective : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entró en el trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Obtener el componente Player del objeto que colisionó
            NewBehaviourScript player = other.GetComponent<NewBehaviourScript>();
            if (player != null)
            {
                // Llamar al método CollectItem del jugador
                player.CollectItem();
                // Destruir el objeto
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Player component not found on the detected object.");
            }
        }
    }
}
