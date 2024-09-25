using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Player_Script : MonoBehaviour
{
    public static Player_Script sharedInstance;   //el singleton para que solo haya un personaje 
    public int health;    
    public int mana;    
    public float jumpForce = 3f;//esta variable la hacemos publica para que sea visible desde unity y asi poder ir manipulandola
    public float currentJump;   
    public float runningSpeed = 2f;
    public float currentSpeed;
    public float coinsWeight;
    private Rigidbody2D rigidbody;
    public Animator animator;
    private Vector3 startPosition;//para resetear al personaje a la posicion inicial al empezar    
    public const int initialHealth = 150, initialMana = 0; 
    public int maxHealth, maxMana;//para simplificar la edicion de los parametros, es mejor tenerlos todos arriba como constantes en vez de meter el valor en el metodo
    public const float superJumpForce = 2f;       
    public bool isMoovingLeft;
    public bool isMoovingRight;
    float travelledDistance;


    void Awake()
    {
        sharedInstance = this;
        rigidbody = GetComponent<Rigidbody2D>();//fija el rigidbody2d del componente donde este este script a la variable rigidbody
        startPosition = this.transform.position;//nada mas iniciar el juego, guardamos en esta variable la posicion del personaje, para recuperarla tras el game over    
        this.health = initialHealth;
        maxHealth = 150;
        maxMana = 50;      
    }
    
    public void StartGame()//como el metodo start se llama automaticamente al empezar le cambiamos el nombre para llamarlo nosotros desde el gameManager, por esto debe ser publico
    {
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
        animator.SetBool("isRunning", false);
        this.transform.position = startPosition;//cada vez que reiniciamos ponemos al pj en la startPosition        
        this.health = initialHealth;
        this.mana = initialMana;
        StartCoroutine("Manarest");
        maxHealth += Skills.getHealthBonus();
        maxMana += Skills.getManaBonus();        
    }


    //Una corrutina es un bucle que se va ejecutando hasta que el usuario lo detenga
    IEnumerator Manarest()
    {
        while (this.mana <= maxMana+1)
        {
            this.mana++;
            if (this.mana > maxMana)
            {
                this.mana = maxMana;
            }
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }
        
    void Update()
    {        
        animator.SetBool("isGrounded",isTouchingGround());
        animator.SetBool("isRunning",isRunning());
        currentSpeed = runningSpeed * health / 100 + Skills.getRunningSpeedBonus() - slowCoins();
        currentJump = jumpForce + Skills.getJumpForceBonus() - slowCoins();
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)//para que solo salte y pause en modo de juego.GameManager es la clase, sharedinstance es el objeto estatico que instancia la clase y currentGameState es la variable GameState que define el estado del juego
        {            
            if (isMoovingRight)
            {
                sharedInstance.moveRight();
            }
            else if (isMoovingLeft)
            {
                sharedInstance.moveLeft();
            }
            else
            {
                sharedInstance.dontMove();
            }
        }            
        
        if (health <= 0)
        {
            sharedInstance.kill();
        }        
        if(sharedInstance.GetDistance() / Skills.getLevel() >= 100)
        {
            Skills.levelUp();
            GameManager.sharedInstance.endlevel();           
        }
    }
    void FixedUpdate()  //el FixedUpdate garantiza que las acciones sean contantes, util para el movimiento.
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)//para que solo se mueva en modo de juego
        {
            
        }
    }
    float slowCoins()
    {
        coinsWeight = GameManager.sharedInstance.coins / 100 - Skills.getCoinResistance();
        if (coinsWeight > 0)
        {
            return coinsWeight;
        }
        return 0;
    }
    public void jump()
    {             
            if (isTouchingGround())
            {
                rigidbody.AddForce(Vector2.up * currentJump, ForceMode2D.Impulse);
            } 
    }
    public void superJump()
    {        
            if (isTouchingGround())
            {
                if (this.mana >= 50 - Skills.getSuperJumpCost())
                {
                    rigidbody.AddForce(Vector2.up * currentJump * superJumpForce, ForceMode2D.Impulse);
                    mana -= 50 - Skills.getSuperJumpCost();                    
                }
            }       
    }
    public LayerMask groundLayer;//creamos una capa que el personaje pueda reconocer donde colocamos el suelo
    public bool isTouchingGround()//cuando el pj esta cerca de la capa de suelo puede saltar, sino no
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.5f, groundLayer))
        //si lanzando un rayo desde nuestra pasicion, hacia abajo, hasta una distancia de 0,2, encontramos la capa del suelo
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool isRunning()
    {
        if (rigidbody.velocity.x != 0f)
        {
            return true;
        }
        else
        {
           return false;
        }
    }
    public void moveRight()
    {        
        isMoovingRight = true;
            if (rigidbody.velocity.x < runningSpeed)
            {
                rigidbody.velocity = new Vector2(currentSpeed, rigidbody.velocity.y);
                transform.localRotation = Quaternion.Euler(0, 0, 0);//para que el sprite mire a la derecha                
            }       
    }
    public void moveLeft()
    {        
        sharedInstance.isMoovingLeft = true;
            if (rigidbody.velocity.x > -runningSpeed)
            {
                rigidbody.velocity = new Vector2(-currentSpeed, rigidbody.velocity.y);
                transform.localRotation = Quaternion.Euler(0, 180, 0);//para que el prite mire a la izquierda                
            }       
    }
    public void dontMove()
    {
        sharedInstance.isMoovingLeft = false;
        sharedInstance.isMoovingRight = false;
    }     
    public void kill()
    {
        GameManager.sharedInstance.GameOver();
        this.animator.SetBool("isAlive",false);
       
        //Para guardar la puntuacion en un fichero que guarda en memoria no volatil usamos las PlayerPrefs. En ellas gardaremos y recuperaremos con get y set. Esto sera aplicable a las habilidades elegidas, inventario, etc
        float currentMaxScore = PlayerPrefs.GetFloat("maxScore", 0);//recupera el valor asociado al codigo maxScore y si no hay dame el valor por defecto, en este caso 0
        if (currentMaxScore < GameManager.sharedInstance.coins)
        {
            PlayerPrefs.SetFloat("maxScore", GameManager.sharedInstance.coins);
        }
        StopCoroutine("HealthLoss");
    }

    //con este metodo podemos medir la distancia que ha recorrido el personaje y se muestra en pantalla. 
    //Puedo usarlo para elegir cuando termina el nivel tras recorrer una distancia determinada.
    //En este ejemplo se vuelca el dato en el script del canvas como puntuacion
    public float GetDistance()
    {
        travelledDistance = Vector2.Distance(new Vector2(startPosition.x,0), new Vector2(this.transform.position.x,0));
        return travelledDistance;
    }

    public int GetHealth()
    {
        return this.health;
    }
    public void CollectHealth(int value)
    {        
        this.health += value + Skills.getCandyStikCure();        
        if (this.health > maxHealth)
        {
            this.health = maxHealth;
        }
    }
    public int GetMana()
    {
        return this.mana;
    }
    public void CollectMana(int value)
    {
        this.mana += value;
        if (this.mana >maxMana)
        {
            this.mana = maxMana;
        }
    }
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "EnemyTriggerMovement")
        {
            this.health -= 10;            
        }

        if (otherCollider.tag == "Enemy")
        {
            Destroy(otherCollider.gameObject);
        }
    }    
}

    

