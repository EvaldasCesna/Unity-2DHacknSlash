using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    private string CreateAccountUrl = "http://188.141.5.218/Register.php";
    private string check;

    public InputField Username;
    public InputField Password;
    public InputField cPassword;
    public InputField Email;
    public Text status;
    public Button Reg;

    // Use this for initialization
    void Start()
    {
        check = "";
        // var password = gameObject.GetComponent<InputField>();
        Reg = GetComponent<Button>();
        Reg.onClick.AddListener(() =>
        {
            if (Password.text == cPassword.text) { Debug.Log("Success0"); StartCoroutine("CreateAccount"); }
            else { check = "Wrong password"; cPassword.text = ""; }
        });


    }

    public void CheckSucces()
    {
        status.text = check;
    }

    IEnumerator CreateAccount()
    {
        Debug.Log("Success1");
        WWWForm Form = new WWWForm();

        Form.AddField("Username", Username.text);
        Form.AddField("Email", Email.text);
        Form.AddField("Password", Password.text);

        WWW CreateAccountWWW = new WWW(CreateAccountUrl, Form);

        yield return CreateAccountWWW;

        if (CreateAccountWWW.error != null)
        {
            check = "Cant Connect To Account Creation";
            Debug.LogError("Cannot Connect to acc creation");
        }
        else
        {
            Debug.Log(CreateAccountWWW.text);
            string CreateAccountReturn = CreateAccountWWW.text;
            if (CreateAccountReturn == "Success")
            {
                check = "Success";
                Username.text = " ";
                Email.text = " ";
                Password.text = " ";
                cPassword.text = " ";
                Debug.Log("Success: Account created");
            }
            else
            {
                check = "Username Exists";
            }
        }
        CheckSucces();
    }
 

}
