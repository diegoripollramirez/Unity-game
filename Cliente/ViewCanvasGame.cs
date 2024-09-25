using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewCanvasGame : MonoBehaviour
{
    public Text collectableLabel;
    public Text scoreLabel;
    public Text maxScoreLabel;
    public Text skillJump;
    public Text skillRun;
    public Text skillStrenght;
    public Text skillCandyCure;
    public Text skillHealth;
    public Text skillMana;
    public Text skillDiscount;
    public Text skillSuperjump;
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame|| 
            GameManager.sharedInstance.currentGameState == GameState.gameOver)
        {            
            collectableLabel.text = "Coins\n" + GameManager.sharedInstance.coins.ToString();
        }
       
        //Volcamos la distancia recorrida como porcentaje del nivel completo en el estado inGame.
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {            
            this.scoreLabel.text = "Level "+Skills.getLevel()+"\n" + (Player_Script.sharedInstance.GetDistance()/(Skills.getLevel())).ToString("f0")+"%";//dentro de la funcion toString se pueden definir los decimales que queremos al convertir un float.

            //float maxScore = PlayerPrefs.GetFloat("maxScore", 0);            
            this.maxScoreLabel.text = "MaxScore\n" + PlayerPrefs.GetFloat("maxScore", 0).ToString("f0"); 
        }
        if (GameManager.sharedInstance.currentGameState == GameState.skillsMenu)
        {            
            collectableLabel.text = "Coins\n" + GameManager.sharedInstance.coins.ToString();            
            skillHealth.text= Skills.getHealthBonusCost().ToString();
            skillMana.text = Skills.getManaBonusCost().ToString();
            skillStrenght.text = Skills.getCoinResistanceCost().ToString();
            skillRun.text = Skills.getRunningSpeedBonusCost().ToString();
            skillJump.text = Skills.getJumpForceBonusCost().ToString();
            skillCandyCure.text = Skills.getCandyStikCureCost().ToString();
            skillDiscount.text = Skills.getSkillsDiscountCost().ToString();
            skillSuperjump.text = Skills.getsuperJumpCostCost().ToString();
        }
    }
}
