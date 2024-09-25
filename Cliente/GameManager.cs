using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Listado deposibles estados del videojuego
public enum GameState
{
    menu,
    inGame,
    gameOver,
    skillsMenu,
    levelEnd
}
public class GameManager : MonoBehaviour
{
    //Variable para saber en que estado del juego nos encontramos
    //Iniciará en el menu principal
    public GameState currentGameState = GameState.menu;
    public static GameManager sharedInstance;//singleton para evitar que el GameManager se pueda instanciar mas de una vez. Creamos un objeto estatico de la clase GameManager y todos usaran el objeto, no la clase
    public Canvas menuCanvas, gameCanvas, gameOverCanvas, skillsCanvas, levelEndCanvas;
    public int coins;
    private void Awake()
    {
        sharedInstance = this;//asignamos la variable estatica a la clase en el Awake
        
    }
    private void Start()
    {        
        BackToMenu();
        coins = 0;        
    }

    private void Update()
    {
      
    }
    public void StartGame()
    {        
        SetGameState(GameState.inGame);//fija el estadop del juego en cada uno de los elementos del enumerado        
        if (Player_Script.sharedInstance.transform.position.x > 20)
        {
            LevelGenerator.sharedInstance.removeAllBlocks();
            LevelGenerator.sharedInstance.addInitialBlocks();
        }
        Player_Script.sharedInstance.StartGame();//devuelve al personame a la posicion inicial
        CameraFollow.sharedInstance.ResetCameraPosition();
        coins = 0;

        //al iniciar el juego cargamos los datos
        try
        {
            SocketClient.LoadGame(Skills.getDeviceUniqueIdentifier());
            Debug.Log("se han cargado los datos");
        }
        catch
        {
            Debug.Log("No ha sido posible cargar la informacion en el servidor");
        }
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);//fija el estadop del juego en cada uno de los elementos del enumerado        
    }

    public void BackToMenu()
    {                
        if (GameManager.sharedInstance.currentGameState == GameState.skillsMenu)
        {
            try
            {
                string info = Skills.getDeviceUniqueIdentifier() + ";" + Skills.getLevel() + ";" + Skills.getHealthBonus() + ";" + Skills.getManaBonus() + ";" + Skills.getCoinResistance() + ";"
                           + Skills.getRunningSpeedBonus() + ";" + Skills.getJumpForceBonus() + ";" + Skills.getSuperJumpCost() + ";" + Skills.getCandyStikCure() + ";" + Skills.getSkillsDiscount() + ";"
                           + Skills.getHealthBonusCost() + ";" + Skills.getManaBonusCost() + ";" + Skills.getCoinResistanceCost() + ";" + Skills.getRunningSpeedBonusCost() + ";" + Skills.getJumpForceBonusCost() + ";"
                           + Skills.getsuperJumpCostCost() + ";" + Skills.getCandyStikCureCost() + ";" + Skills.getSkillsDiscountCost();
                SocketClient.SaveGame(info);
                Debug.Log("Info guardada con exito");
            }
            catch
            {
                Debug.Log("No ha sido posible guardar la informacion");
            }
        }
        SetGameState(GameState.menu);//fija el estadop del juego en cada uno de los elementos del enumerado
    }
    public void SkillsMenu()
    {
        SetGameState(GameState.skillsMenu);//fija el estadop del juego en cada uno de los elementos del enumerado
    }

    public void exitGame()//el asterisco delante del if sirve para que el programa reconozca que tipo de dispositivo se esta usando y haga la instruccion en base a eso
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit(); //Apple no permite poner boton para cerrar la aplicacion, android no lo revisa pero tiene la tecla home para salir
        #endif        
    }   
    public void endlevel()
    {
        SetGameState(GameState.levelEnd);        
    }

    void SetGameState(GameState newGameState)
    {
        this.currentGameState = newGameState;

        if (newGameState == GameState.menu)
        {
            menuCanvas.enabled = true;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
            skillsCanvas.enabled = false;
            levelEndCanvas.enabled = false;
            Player_Script.sharedInstance.health = 1;
        }
        if (newGameState == GameState.inGame)
        {
            menuCanvas.enabled = false;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
            skillsCanvas.enabled = false; 
            levelEndCanvas.enabled = false;
        }
        if (newGameState == GameState.gameOver)
        {
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = true;
            skillsCanvas.enabled = false;
            levelEndCanvas.enabled = false;
        }
        if (newGameState == GameState.skillsMenu)
        {
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
            skillsCanvas.enabled = true;
            levelEndCanvas.enabled = false;
        }
        if (newGameState == GameState.levelEnd)
        {
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
            skillsCanvas.enabled = false;
            levelEndCanvas.enabled = true;
        }
    }
}