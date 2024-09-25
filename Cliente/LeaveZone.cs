using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveZone : MonoBehaviour
{
    float restTime;
    private void OnTriggerEnter2D(Collider2D collision)//cuando se atraviesa el collider del objeto que tiene este script, la exitZone
    {
        if (collision.tag == "Player")
        {
            if (restTime > 3.0f)
            {
                LevelGenerator.sharedInstance.addLevelBlock();
                LevelGenerator.sharedInstance.removeOldestLevelBlock();
                restTime = 0.0f;
            }
        }        
    }
    private void Update()
    {
        restTime += Time.deltaTime;
    }
}
