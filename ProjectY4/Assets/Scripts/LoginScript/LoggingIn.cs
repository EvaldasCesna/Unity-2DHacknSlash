using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoggingIn : MonoBehaviour
{
    private string LoginUrl = "http://188.141.5.218/Login.php";

    public InputField Username;
    public InputField Password;
    public Button Login;
    // Use this for initialization
    void Start()
    {

        // var password = gameObject.GetComponent<InputField>();
        Login = GetComponent<Button>();
        Login.onClick.AddListener(() => { StartCoroutine("LoginAccount");} );


    }


    IEnumerator LoginAccount()
    {
     //   Debug.Log("Attempting Log in");
        WWWForm Form = new WWWForm();
        Form.AddField("Username", Username.text);
        Form.AddField("Password", Password.text);
        WWW LoginAccountWWW = new WWW(LoginUrl, Form);
        yield return LoginAccountWWW;
        if(LoginAccountWWW.error != null)
        {
            Debug.LogError("Cannot Connect to Login");
        }
        else
        {
            string LogText = LoginAccountWWW.text;
          //  Debug.Log(LogText);
            string[] LogTextSplit = LogText.Split(':');
            if (LogTextSplit[0] == "0")
            {
                if(LogTextSplit[1] == "Success")
                {
                   PlayerPrefs.SetString("Player Name", Username.text);
                   SceneManager.LoadScene("Game");
                }
            }
            else
            {
                if(LogTextSplit[1] == "Success")
                {
                    PlayerPrefs.SetString("Player Name", Username.text);
                    SceneManager.LoadScene("Game");
                }
            }
        }
    }

}
