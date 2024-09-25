using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tipoColeccionable
{
    pocionVida,
    pocionMana,
    moneda,
    bastoncillo
}
public class CoinScript : MonoBehaviour
{
    public tipoColeccionable type;
    public int value;    

    public AudioClip collectCoins;//variables publicas a las que asociarles los sonidos
    public AudioClip collectBastoncillo;//variables publicas a las que asociarles los sonidos
    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (other.tag == "Player")
        {            
            switch (this.type)
            {
                case tipoColeccionable.moneda:
                    GameManager.sharedInstance.coins += value;
                    if (audio != null && collectCoins != null)
                    {
                        audio.clip = collectCoins;                        
                        audio.Play();                                             
                    }                    
                    break;
                //case tipoColeccionable.bastoncillo:
                //    GameManager.sharedInstance.coins += value;
                    
                //    if (audio != null && collectBastoncillo != null)
                //    {                        
                //        audio.clip = collectBastoncillo;
                //        audio.Play();
                //    }            
                //    break;
                case tipoColeccionable.pocionVida://los bastoncillos van a ser pociones de vida
                    Player_Script.sharedInstance.CollectHealth(value);
                    if (audio != null && collectBastoncillo != null)
                    {
                        audio.clip = collectBastoncillo;
                        audio.Play();
                    }
                    break;
                //case tipoColeccionable.pocionMana:                    
                //    Player_Script.sharedInstance.CollectMana(value);
                //    break;
            }            
            Destroy(this.gameObject,audio.clip.length);//si destruyo el gameobject detiene el sonido por lo que no llega a escucharse, para no cambiar todos los metodos al ocultar del profesor, coloco un audioSource en el player
            //Collect();
        }
    }

    //bool isCollected;
    //void Show()
    //{
        //    this.GetComponent<SpriteRenderer>().enabled = true;
        //    this.GetComponent<Animation>().enabled = true;
        //    isCollected = false;
    //    GameManager.sharedInstance.CollectObjects(value);
    //}
    //void Hide()
    //{
    //    this.GetComponent<SpriteRenderer>().enabled = false;
    //    this.GetComponent<Animation>().enabled = false;
    //}
    //void Collect()
    //{
    //    isCollected = false;
    //    Hide();
    //}
}
