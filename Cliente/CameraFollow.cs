using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow sharedInstance;
    public Transform target;
    public Vector3 offset = new Vector3(0.0f,0.0f,-10.0f);
    public float dampTime = 0.3f;
    public Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        sharedInstance = this;
        Application.targetFrameRate = 60;
    } 
    void Update()
    {
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);//Convierte las coordenadas del personaje en el mundo en coordenadas de la vision de camara
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));//ahora miramos la posicion del mundo respecto a la posicion de la camara
        Vector3 destination = point + delta;
        destination = new Vector3(target.position.x, offset.y+1, offset.z);
        this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity,dampTime);
    }
    public void ResetCameraPosition()
    {
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);//Convierte las coordenadas del personaje en el mundo en coordenadas de la vision de camara
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));//ahora miramos la posicion del mundo respecto a la posicion de la camara
        Vector3 destination = point + delta;
        destination = new Vector3(target.position.x, offset.y + 3, offset.z);
        this.transform.position = destination;
    }
}
