using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    //instance of this class
    public static Main Instance;

    public Web Web;
    public UserInfo UserInfo;
    public Login Login;


    public GameObject UserProfile;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Web = GetComponent<Web>();
        UserInfo = GetComponent<UserInfo>();
    }

}
