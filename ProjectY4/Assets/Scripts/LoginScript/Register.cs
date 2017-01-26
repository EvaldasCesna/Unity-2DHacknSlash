using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour {

    public InputField Username;
    public InputField Password;
    public InputField cPassword;
    public InputField Email;
   // string name;
   // string pass;
  //  string email;
    public Button Reg;
    private string CreateAccountUrl = "http://188.141.5.218/Register.php";

    // Use this for initialization
    void Start()
    {

        // var password = gameObject.GetComponent<InputField>();
        Reg = GetComponent<Button>();
        Reg.onClick.AddListener(() => { if (Password.text == cPassword.text) { Debug.Log("Success0"); StartCoroutine("CreateAccount"); } else { cPassword.text = ""; } });


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

        if(CreateAccountWWW.error != null)
        {
            Debug.LogError("Cannot Connect to acc creation");
        }
        else
        {
            Debug.Log(CreateAccountWWW.text);
            string CreateAccountReturn = CreateAccountWWW.text;
            if(CreateAccountReturn == "Success")
            {
                Debug.Log("Success: Account created");
            }
        }

    }


}
