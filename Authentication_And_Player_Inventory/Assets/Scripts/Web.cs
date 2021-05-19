using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    void Start()
    {
        // Get Date Request.
        //StartCoroutine(GetRequest());

        // Get Users Request.
        //StartCoroutine(GetRequest());

        // Get Users Request.
        //StartCoroutine(Login("testuser", "123456"));

        //Create user
        //StartCoroutine(RegisterUser("testuser3", "123456"));

    }

    /*public void GetItemData()
    {
        StartCoroutine(GetItemsIDs(Main.Instance.UserInfo.UserID));
    }*/

    public IEnumerator GetDate()
    {
        string uri = "http://localhost/UnityBackend/GetDate.php";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    public IEnumerator GetUsers()
    {
        string uri = "http://localhost/UnityBackend/GetUsers.php";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }


    public IEnumerator GetItemIcon(string itemID, System.Action<byte[]> callback)
    {

        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);

        string uri = "http://localhost/UnityBackend/GetItemIcon.php";
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    
                    // results as byte array
                    byte[] bytes = webRequest.downloadHandler.data;

                    
                    callback(bytes);

                    break;
            }
        }
    }


    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                Main.Instance.UserInfo.SetCredentials(username, password);
                Main.Instance.UserInfo.SetID(www.downloadHandler.text);

                if(www.downloadHandler.text.Contains("Wrong Credentials.") || www.downloadHandler.text.Contains("Username does not exists."))
                {
                    Debug.Log("Try Again");
                }
                else
                {
                    //If we login correctly
                    Main.Instance.UserProfile.SetActive(true);
                    Main.Instance.Login.gameObject.SetActive(false);
                }

            }
        }
    }

    public IEnumerator RegisterUser(string usename, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", usename);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }


    public IEnumerator GetItemsIDs(string userID, System.Action<string> callback)
    {

        WWWForm form = new WWWForm();
        form.AddField("userID", userID);

        string uri = "http://localhost/UnityBackend/GetItemsIDs.php";
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Show results as text
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    if(webRequest.downloadHandler.text != "0")
                    {
                        //Call callback function to pass results
                        string jsonArray = webRequest.downloadHandler.text;

                        callback(jsonArray);
                    }
                    

                    break;
            }
        }
    }


    public IEnumerator GetItem(string itemID, System.Action<string> callback)
    {

        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);

        string uri = "http://localhost/UnityBackend/GetItem.php";
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Show results as text
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                    //Call callback function to pass results
                    string jsonArray = webRequest.downloadHandler.text;

                    callback(jsonArray);

                    break;
            }
        }
    }


    public IEnumerator SellItem(string id, string itemID, string userID) //, System.Action<string> callback
    {

        WWWForm form = new WWWForm();
        form.AddField("ID", id);
        form.AddField("itemID", itemID);
        form.AddField("userID", userID);

        string uri = "http://localhost/UnityBackend/SellItem.php";
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Show results as text
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                    //Call callback function to pass results
                    //callback();

                    break;
            }
        }
    }

}
