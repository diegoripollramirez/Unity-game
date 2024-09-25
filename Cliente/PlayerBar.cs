using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum barType
{
    health,
    mana
}
public class PlayerBar : MonoBehaviour
{
    private Slider slider;
    public barType type;
    
    // Start is called before the first frame update
    void Start()
    {
        this.slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case barType.health:
                this.slider.value = Player_Script.sharedInstance.GetHealth();
                this.slider.maxValue = Player_Script.sharedInstance.maxHealth;
                break;
            case barType.mana:
                this.slider.value = Player_Script.sharedInstance.GetMana();
                this.slider.maxValue = Player_Script.sharedInstance.maxMana;
                break;

        }
    }
}
