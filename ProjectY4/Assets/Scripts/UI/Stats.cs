using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Networking;

public class Stats : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Equipment equipment;
    private string data;
    private Item melee;
    private Item ranged;
    private Item magic;
    private SaveData save;
    //UI
    public Text stats;
    public Slider hpBar;
    public Text hpText;
    public Slider xpBar;
    public Text xpText;
    public Text level;

    public int enemiesKilled;
    public int bossesKilled;

    public static Stats pStats;
    public int currentLevel;
    public int currentXp;
    public int[] levels;
    public Text gold;
    public bool leveledUp;


    // Use this for initialization
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        equipment = Equipment.pEquipment;
        save = equipment.GetComponent<SaveData>();
        UpdateStats();
        Hide();
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Update()
    {
        UpdateXpBar();
        levelUp();
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
        xpBar.maxValue = levels[currentLevel];
        xpBar.value = currentXp;
        xpText.text = currentXp + "/" + levels[currentLevel];
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
        int temp = 0;
        int.TryParse(gold.text, out temp);
    
        temp = temp + goldIn;

        gold.text = (temp).ToString();
    }

    public void UpdateStats()
    {
        int def = 0, str = 0, vit = 0;
        
        for (int i = 0; i < equipment.equipment.Count; i++)
        {
            def += equipment.equipment[i].Defence;
            str += equipment.equipment[i].Strength;
            vit += equipment.equipment[i].Vitality;
        }
        
        data = "  Strenght: " + str + "\n\n  Defence: " + def + "\n\n  Vitality: " + vit + "\n\n\n  Mob Kills: " + enemiesKilled + "\n\n  Boss Kills: " + bossesKilled;
        stats.text = data;

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
        leveledUp = false;
        if (currentXp >= levels[currentLevel])
        {
            currentXp = 0;
            currentLevel++;
            leveledUp = true;
        }
    }

    public void setLevel(int lvl, int xp)
    {
        currentLevel = lvl;
        currentXp = xp;
    }

    public void addXp(int xp)
    {
        currentXp += xp;
        Invoke("Isave", 0.1f);
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

    public void initialiseKills(int enemy, int boss)
    {
        enemiesKilled = enemy;
        bossesKilled = boss;
    }

    public void addEnemyKill()
    {
        enemiesKilled++;
    }

    public void addBossKill()
    {
        bossesKilled++;
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

    void Isave()
    {
        save.SaveItems();
    }
}
