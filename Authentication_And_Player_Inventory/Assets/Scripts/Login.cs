using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button createUserButton;
    public GameObject registerUserWindow;

    // Start is called before the first frame update
    void Start()
    {
        loginButton.onClick.AddListener(() =>
        {
            StartCoroutine( Main.Instance.Web.Login(usernameInput.text, passwordInput.text));
        });

        createUserButton.onClick.AddListener(() =>
        {
            registerUserWindow.SetActive(true);
        });

    }

}