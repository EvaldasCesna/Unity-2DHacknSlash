using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    private string LoadUrl = "http://188.141.5.218/Leaderboard.php";
    public Text names;
    public Text bosses;
    public Text mobs;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("LoadLeaderBoard");
    }

    public void RefreshLeaderboard()
    {
        StartCoroutine("LoadLeaderBoard");
    }

    IEnumerator LoadLeaderBoard()
    {
        WWWForm Form = new WWWForm();
        Form.AddField("Username", PlayerPrefs.GetString("Player Name"));

        WWW LoadWWW = new WWW(LoadUrl, Form);
        yield return LoadWWW;
        if (LoadWWW.error != null)
        {
            Debug.LogError("Cannot Connect to DB");
        }
        else
        {
            string LogText = LoadWWW.text;
            string[] LogTextSplit = LogText.Split('*');
            names.text = LogTextSplit[0];
            mobs.text = LogTextSplit[1];
            bosses.text = LogTextSplit[2];
        }
    }
}
