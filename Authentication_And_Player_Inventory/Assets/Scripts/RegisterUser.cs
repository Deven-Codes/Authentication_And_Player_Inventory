using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegisterUser : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;
    public Button submitButton;
    public Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        submitButton.onClick.AddListener(() =>
        {
            if(passwordInput.text == confirmPasswordInput.text)
            {
                StartCoroutine(Main.Instance.Web.RegisterUser(usernameInput.text, passwordInput.text));
            }
        });


        backButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

    }
}
