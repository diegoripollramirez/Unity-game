using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float runningSpeed = 1.5f;
    private Rigidbody2D rigidBody;
    public static bool turnAround;
    private Vector3 startPosition;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        //startPosition = this.transform.position;        
    }
    private void Start()
    {
        //this.transform.position = startPosition; //por algun motivo aparecen en la posicion cero respecto del mapa y no del levelblock al que pertenecen
    }
    private void FixedUpdate()//si le aplico este scipt a multiples enemigos, todos compartiran las variables por lo que cuando uno choque con algo todos lo haran, aunque no hayan chocado los demas
    {
        float currentRunningSpeed = runningSpeed;

        if (turnAround == true)//velocidad negativa
        {
            this.transform.eulerAngles = new Vector3(0, 180f, 0);
            currentRunningSpeed = -runningSpeed;
        }
        else//velocidad positiva
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            currentRunningSpeed = runningSpeed;
        }

        if (GameManager.sharedInstance.currentGameState == GameState.inGame)        
        {
            rigidBody.velocity = new Vector2(currentRunningSpeed, rigidBody.velocity.y);            
        }        
    }
}
