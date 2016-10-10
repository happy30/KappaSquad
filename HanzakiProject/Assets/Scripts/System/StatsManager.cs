//StatsManager by Jordi

using UnityEngine;
using System.Collections;

public class StatsManager : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public int health;
    public int maxHealth;

    public int hookParts;
    public bool katanaUnlocked;
    public bool grapplingHookUnlocked;
    public bool shurikenUnlocked;
    public bool smokeBombUnlocked;

    public int shurikenAmount;
    public int smokeBombAmount;

    public void GetLife()
    {
        maxHealth++;
        health++;
    }

    public void AddHookPart()
    {
        hookParts++;
        if(hookParts >= 2)
        {
            grapplingHookUnlocked = true;
        }
    }

    public void GetHealth()
    {
        if(health < maxHealth)
        {
            health++;
        }
    }

    public void AddShuriken()
    {
        shurikenUnlocked = true;
        shurikenAmount++;
    }

    public void AddSmokeBomb()
    {
        smokeBombUnlocked = true;
        smokeBombAmount++;
    }

    public void AddKatana()
    {
        katanaUnlocked = true;
        GameObject.Find("Katana").GetComponent<Katana>().UpgradeWeapon();
    }
}
