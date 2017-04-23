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
    public Text status;
    string check;
    // Use this for initialization
    void Start()
    {
        Login = GetComponent<Button>();
        Login.onClick.AddListener(() => { StartCoroutine("LoginAccount"); });
    }

    public void updateText()
    {
        status.text = check;
    }

    IEnumerator LoginAccount()
    {
        //   Debug.Log("Attempting Log in");
        WWWForm Form = new WWWForm();
        Form.AddField("Username", Username.text);
        Form.AddField("Password", Password.text);
        WWW LoginAccountWWW = new WWW(LoginUrl, Form);
        yield return LoginAccountWWW;
        if (LoginAccountWWW.error != null)
        {
            check = "Cannot Connect(Server Offline ?)";
            updateText();
        }
        else
        {
            string LogText = LoginAccountWWW.text;
            string[] LogTextSplit = LogText.Split(':');
            if (LogTextSplit[0] == Username.text)
            {
                if (LogTextSplit[1] == "Success")
                {
                    PlayerPrefs.SetString("Player Name", Username.text);
                    SceneManager.LoadScene("MainMenu");
                }
            }

            if (LogText == "WrongPassword")
            {
                check = "Wrong Password";
                updateText();
            }
            if (LogText == "NameDoesNotExist")
            {
                check = "Wrong Name";
                updateText();
            }
        }
    }
}
