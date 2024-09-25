using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Para crear la zona de muerte creamos un objeto vacio y le damos un collider con la casilla de trigger marcado para que no sea una superficie solida sino que desencadene algo cuando se atraviese.
//Debemos asegurarnos que el jugador tiene puesta la etiqueta player para que la zona solo mate cuando el objeto player la atraviese y no otras cosas
public class KillTrigger : MonoBehaviour
{
       private void OnTriggerEnter2D(Collider2D other)
       {
            if (other.tag == "Player")
            {            
                Player_Script.sharedInstance.kill();
            }
       }
}
