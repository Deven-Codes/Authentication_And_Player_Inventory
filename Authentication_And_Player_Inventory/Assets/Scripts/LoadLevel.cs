using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public GameObject login;
    public GameObject registerUser;
    public GameObject userProfile;

    // Start is called before the first frame update
    void Start()
    {
        login.SetActive(true);
        registerUser.SetActive(false);
        userProfile.SetActive(false);
    }

    
}
