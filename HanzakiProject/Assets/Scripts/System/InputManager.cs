//InputManager by Jordi

using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public InputManager instance;

    public static KeyCode Slash = KeyCode.Z;
    public static KeyCode Shuriken = KeyCode.X;
    public static KeyCode Hook = KeyCode.C;
    public static KeyCode SmokeBomb = KeyCode.V;
    public static KeyCode Jump;
    public static KeyCode JumpSS = KeyCode.UpArrow;
    public static KeyCode JumpTD = KeyCode.Space;


    void Start()
    {
        if (instance)
        {
            return;
        }

        if (PlayerPrefs.HasKey("keya"))
        {
            Slash = (KeyCode)PlayerPrefs.GetInt("keya");
        }
        if (PlayerPrefs.HasKey("keyb"))
        {
            Shuriken = (KeyCode)PlayerPrefs.GetInt("keyb");
        }
        if (PlayerPrefs.HasKey("keyc"))
        {
            Hook = (KeyCode)PlayerPrefs.GetInt("keyc");
        }
        if (PlayerPrefs.HasKey("keyd"))
        {
            SmokeBomb = (KeyCode)PlayerPrefs.GetInt("keyd");
        }
        if(PlayerPrefs.HasKey("keyup"))
        {
            JumpSS = (KeyCode)PlayerPrefs.GetInt("keyup");
        }
        if (PlayerPrefs.HasKey("keyspace"))
        {
            JumpTD = (KeyCode)PlayerPrefs.GetInt("keyspace");
        }

        instance = this;
    }

    public static void SaveKeys()
    {
        PlayerPrefs.SetString("keya", Slash.ToString());
        PlayerPrefs.SetString("keyb", Shuriken.ToString());
        PlayerPrefs.SetString("keyc", Hook.ToString());
        PlayerPrefs.SetString("keyd", SmokeBomb.ToString());
        PlayerPrefs.SetString("keyspace", JumpTD.ToString());
    }
}

