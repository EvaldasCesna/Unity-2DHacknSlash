using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Networking;

public class Stats : MonoBehaviour
{
    public static Stats pStats;
    CanvasGroup canvasGroup;
    private Equipment equipment; 
    private GameObject stats;
    private string data;
    public int currentLevel;
    public int currentXp;
    public int[] levels;
    private Slider hpBar;
    private Text hpText;
    private Slider xpBar;
    private Text xpText;
    private Text level;
    public Text gold;

    Item melee;
    Item ranged;
    Item magic;

    // Use this for initialization
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
     

        gold = GameObject.Find("Gold").GetComponent<Text>();
        level = GameObject.Find("Level").GetComponent<Text>();
        hpBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        hpText = hpBar.GetComponentInChildren<Text>();
        xpBar = GameObject.Find("XpBar").GetComponent<Slider>();
        xpText = xpBar.GetComponentInChildren<Text>();
        stats = GameObject.FindGameObjectWithTag("Stats");
        equipment = GameObject.Find("Inventory").GetComponent<Equipment>();
        Hide();
    }

    private void Update()
    {
        //  UpdateHealthbar();
        //if (!Network.isServer)
        //    CmdUpdateStats();
        //else
        //    RpcUpdateStats();
        //Later only update if something changes but works for now
        UpdateXpBar();
        UpdateStats();
        levelUp();
       // UpdateGold();
    }

    private void Awake()
    {
        if (pStats == null)
            pStats = this;
        else if (pStats != this)
            Destroy(gameObject);
    }

    public void UpdateXpBar()
    {
        xpBar.maxValue = levels[currentLevel + 1];
        xpBar.value = currentXp;
        xpText.text = currentXp + "/" + levels[currentLevel + 1];
        level.text = "Level: " + currentLevel;
    }  

    public void UpdateHealthbar(int max, int health)
    {
        hpBar.maxValue = max;
        hpBar.value = health;
        hpText.text = health + "/" + max;
    } 

    public void UpdateGold(int goldIn)
    {
        gold.text = goldIn.ToString();
    }
    //[Command]
    //public void CmdUpdateStats()
    //{
    //    RpcUpdateStats();
    //}

  //  [ClientRpc]
  //Call this when something in equipment changes
    public void UpdateStats()
    {
        int def = 0, str = 0, vit = 0;
        
        for (int i = 0; i < equipment.equipment.Count; i++)
        {
            def += equipment.equipment[i].Defence;
            str += equipment.equipment[i].Strength + currentLevel;
            vit += equipment.equipment[i].Vitality + currentLevel;
        }

        data = "  Strenght: " + str + "\n\n  Defence: " + def + "\n\n  Vitality: " + vit;
        stats.GetComponent<Text>().text = data;

    }

    public int getBonusHealth()
    {
        int vit = 0;
        for (int i = 0; i < equipment.equipment.Count; i++)
        {
            vit += equipment.equipment[i].Vitality;
        }
        return vit;
    }
    public int getBonusArmor()
    {
        int def = 0;
        for (int i = 0; i < equipment.equipment.Count; i++)
        {
            def += equipment.equipment[i].Defence;
        }
        return def;
    }
    public void levelUp()
    {
        if(currentXp >= levels[currentLevel])
        {
            currentLevel++;
        }
    }

    public void addXp(int xp)
    {
        currentXp += xp;
    }

    public Item getMelee()
    {
        melee = equipment.equipment[3];
        return melee;
    }

    public Item getMagic()
    {
        magic = equipment.equipment[1];
        return magic;
    }

    public Item getRanged()
    {
        ranged = equipment.equipment[5];
        return ranged;
    }
    //pa.setSprites(equipment[1].Sprite, equipment[3].Sprite, equipment[5].Sprite);

    public void Hide()
    {
        canvasGroup.alpha = 0f; //this makes everything transparent
        canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    public void Show()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
