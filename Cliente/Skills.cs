using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    private static int level=1;
    private static int healthBonus;
    private static int healthBonusCost=10;
    private static int manaBonus;
    private static int manaBonusCost=10;
    private static float coinResistance;
    private static int coinResistanceCost =10;
    private static float runningSpeedBonus;
    private static int runningSpeedBonusCost =10;
    private static float jumpForceBonus;
    private static int jumpForceBonusCost =10;
    private static int superJumpCost;
    private static int superJumpCostCost=10;
    private static int candyStikCure;
    private static int candyStikCureCost=10;
    private static int skillsDiscount;
    private static int skillsDiscountCost=10;
    public static string deviceUniqueIdentifier;

    public static string getDeviceUniqueIdentifier()
    {
        return deviceUniqueIdentifier;
    }
    public static void setDeviceUniqueIdentifier()
    {
        deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
    }


    public static int getLevel()
    {
        return level;
    }
    public static void setLevel(int lvl)
    {
         level=lvl;
    }
    public static void levelUp()
    {
        level++;        
    }    


    public static int getHealthBonus()
    {
        return healthBonus;
    }
    public static void setHealthBonus(int hb)
    {
        healthBonus=hb;
    }
    public static int getHealthBonusCost()
    {
        return healthBonusCost;
    }
    public static void setHealthBonusCost(int hbc)
    {
        healthBonusCost=hbc;
    }


    public static int getManaBonus()
    {
        return manaBonus;
    }
    public static void setManaBonus(int mb)
    {
        manaBonus=mb;
    }
    public static int getManaBonusCost()
    {
        return manaBonusCost;
    }
    public static void setManaBonusCost(int mbc)
    {
        manaBonusCost=mbc;
    }


    public static float getCoinResistance()
    {
        return coinResistance;
    }
    public static void setCoinResistance(float cr)
    {
        coinResistance=cr;
    }
    public static float getCoinResistanceCost()
    {
        return coinResistanceCost;
    }
    public static void setCoinResistanceCost(int crc)
    {
        coinResistanceCost=crc;
    }


    public static float getRunningSpeedBonus()
    {
        return runningSpeedBonus;
    }
    public static void setRunningSpeedBonus(float rsb)
    {
        runningSpeedBonus=rsb;
    }
    public static float getRunningSpeedBonusCost()
    {
        return runningSpeedBonusCost;
    }
    public static void setRunningSpeedBonusCost(int rsbc)
    {
        runningSpeedBonusCost=rsbc;
    }


    public static float getJumpForceBonus()
    {
        return jumpForceBonus;
    }
    public static void setJumpForceBonus(float jfb)
    {
        jumpForceBonus=jfb;
    }
    public static float getJumpForceBonusCost()
    {
        return jumpForceBonusCost;
    }
    public static void setJumpForceBonusCost(int jfbc)
    {
        jumpForceBonusCost=jfbc;
    }


    public static int getSuperJumpCost()
    {
        return superJumpCost;
    }
    public static void setSuperJumpCost(int sjc)
    {
        superJumpCost=sjc;
    }
    public static int getsuperJumpCostCost()
    {
        return superJumpCostCost;
    }
    public static void setSuperJumpCostCost(int sjcc)
    {
        superJumpCostCost=sjcc;
    }


    public static int getCandyStikCure()
    {
        return candyStikCure;
    }
    public static void setCandyStikCure(int csc)
    {
         candyStikCure=csc;
    }
    public static int getCandyStikCureCost()
    {
        return candyStikCureCost;
    }
    public static void setCandyStikCureCost(int cscc)
    {
        candyStikCureCost=cscc;
    }


    public static int getSkillsDiscount()
    {
        return skillsDiscount;
    }
    public static void setSkillsDiscount(int sdc)
    {
        skillsDiscountCost=sdc;
    }
    public static int getSkillsDiscountCost()
    {
        return skillsDiscountCost;
    }
    public static void setSkillsDiscountCost(int sdcc)
    {
        skillsDiscountCost=sdcc;
    }

    //Metodos de las habilidades
    public void jumpHigher()
    {
        if (GameManager.sharedInstance.coins >= jumpForceBonusCost - skillsDiscount)
        {
            jumpForceBonus += 0.1f;           
            GameManager.sharedInstance.coins -= jumpForceBonusCost - skillsDiscount;
            jumpForceBonusCost +=10;
        }
    }
    public void runFaster()
    {
        if (GameManager.sharedInstance.coins >= runningSpeedBonusCost - skillsDiscount)
        {
            runningSpeedBonus += 0.1f;            
            GameManager.sharedInstance.coins -= runningSpeedBonusCost - skillsDiscount;
            runningSpeedBonusCost += 10;
        }
    }
    public void moreCoins()
    {
        if (GameManager.sharedInstance.coins >= coinResistanceCost - skillsDiscount)
        {
            coinResistance += 1f;
            GameManager.sharedInstance.coins -= coinResistanceCost - skillsDiscount;
            coinResistanceCost += 10;
        }
    }
    public void candyCure()
    {
        if (GameManager.sharedInstance.coins >= candyStikCureCost - skillsDiscount)
        {
            candyStikCure += 2;
            GameManager.sharedInstance.coins -= candyStikCureCost - skillsDiscount;
            candyStikCureCost += 10;
        }
    }
    public void moreHealth()
    {
        if (GameManager.sharedInstance.coins >= healthBonusCost - skillsDiscount)
        {
            healthBonus+=10;
            GameManager.sharedInstance.coins -= healthBonusCost - skillsDiscount;
            healthBonusCost += 10;
        }
    }
    public void moreMana()
    {        
        if (GameManager.sharedInstance.coins >= manaBonusCost - skillsDiscount)
        {           
            manaBonus+=5;
            GameManager.sharedInstance.coins -= manaBonusCost - skillsDiscount;
            manaBonusCost += 10;
        }
    }
    public void superJump()
    {        
        if (GameManager.sharedInstance.coins >= superJumpCostCost - skillsDiscount)
        {
            superJumpCost += 5;
            GameManager.sharedInstance.coins -= superJumpCostCost - skillsDiscount;
            superJumpCostCost += 10;
        }
    }
    public void cheaperSkills()
    {
        if (GameManager.sharedInstance.coins >= skillsDiscountCost - skillsDiscount)
        {
            GameManager.sharedInstance.coins -= skillsDiscountCost - skillsDiscount;
            skillsDiscount += 1;//El descuento se aplica tras comprar la habilidad, no en la propia compra   
            skillsDiscountCost += 10;
        }
    }
}
