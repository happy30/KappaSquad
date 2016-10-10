//Made by Sascha Greve

using UnityEngine;
using System.Collections;

public class PickUpScript : MonoBehaviour
{
    public StatsManager stats;

    public enum PickUpTypes
    {
        HookPart,
        Heart,
        Health,
        Shuriken,
        SmokeBomb,
        Katana
    };

    public PickUpTypes pickUpTypes;

    void Awake()
    {
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(pickUpTypes == PickUpTypes.HookPart)
            {
                stats.AddHookPart();
                Destroy(gameObject);
            }
            if(pickUpTypes == PickUpTypes.HookPart)
            {
                stats.AddHookPart();
                Destroy(gameObject);
            }
            if (pickUpTypes == PickUpTypes.Heart)
            {
                stats.GetLife();
                Destroy(gameObject);
            }
            if(pickUpTypes == PickUpTypes.Health)
            {
                stats.GetHealth();
                Destroy(gameObject);
            }
            if (pickUpTypes == PickUpTypes.Shuriken)
            {
                stats.AddShuriken();
                Destroy(gameObject);
            }
            if (pickUpTypes == PickUpTypes.SmokeBomb)
            {
                stats.AddSmokeBomb();
                Destroy(gameObject);
            }
            if (pickUpTypes == PickUpTypes.Katana)
            {
                stats.AddKatana();
                Destroy(gameObject);
            }
        }
    }
}
