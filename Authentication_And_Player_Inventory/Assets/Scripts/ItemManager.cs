using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleJSON;



public class ItemManager : MonoBehaviour
{
    Action<string> _createItemsCallback;

    // Start is called before the first frame update
    void Start()
    {
        _createItemsCallback = (jsonArrayString) => {
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
        };

        CreateItems();
    }

    public void CreateItems()
    {

        string userId = Main.Instance.UserInfo.UserID;
        StartCoroutine(Main.Instance.Web.GetItemsIDs(userId, _createItemsCallback));
    }

    IEnumerator CreateItemsRoutine(string jsonArrayString)
    {
        
        //Parsing JSON array string as an array
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

        

        for (int i = 0; i < jsonArray.Count; i++)
        {
            
            //Create local variables
            bool isDone = false;  // are we done downloading
            string itemId = jsonArray[i].AsObject["itemID"];
            string id = jsonArray[i].AsObject["ID"];
            JSONObject itemInfoJson = new JSONObject();

            
            // Create a callback to get the information from Web.cs
            Action<string> getItemInfoCallback = (itemInfo) => {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };        
            //Wait until Web.cs calls the callback we passed as parameter
            StartCoroutine(Main.Instance.Web.GetItem(itemId, getItemInfoCallback));


            //Wait until callback is called from Web (info finished download)
            yield return new WaitUntil(() => isDone == true);

           
            //Instantiate GameObject (item prefab)
            GameObject itemGo = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
            Item item = itemGo.AddComponent<Item>();
            item.ID = id;
            item.itemID = itemId;
            itemGo.transform.SetParent(this.transform);
            itemGo.transform.localScale = Vector3.one;
            itemGo.transform.localPosition = Vector3.zero;


            //Fill infromation 
            itemGo.transform.Find("Name").GetComponent<TMP_Text>().text = itemInfoJson["name"];
            itemGo.transform.Find("Price").GetComponent<TMP_Text>().text = itemInfoJson["price"];
            itemGo.transform.Find("Description").GetComponent<TMP_Text>().text = itemInfoJson["description"];

            int imgVer = itemInfoJson["imgVer"].AsInt;

            byte[] bytes = ImageManager.Instance.LoadImage(itemId, imgVer);

            //Download the image 
            if(bytes.Length == 0)
            {
                //Create a callback function to get the Sprite from Web.cs
                Action<byte[]> getItemIconCallback = (downloadedBytes) =>
                {
                    Sprite sprite = ImageManager.Instance.BytesToSprite(downloadedBytes);
                    itemGo.transform.Find("Image").GetComponent<Image>().sprite = sprite;

                    //Save the downloaded bytes as Image
                    ImageManager.Instance.SaveImage(itemId, downloadedBytes, imgVer);
                    ImageManager.Instance.SaveVersionJson(); 
                };
                StartCoroutine(Main.Instance.Web.GetItemIcon(itemId, getItemIconCallback));
            }
            //Load from device
            else
            {
                Sprite sprite = ImageManager.Instance.BytesToSprite(bytes);
                itemGo.transform.Find("Image").GetComponent<Image>().sprite = sprite;
            }
                        

            //Set the sell button
            itemGo.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() => {
                string usersitemsID = id;
                string usersitemsItemID = itemId;
                string usersitemsUserId = Main.Instance.UserInfo.UserID; 
                StartCoroutine(Main.Instance.Web.SellItem(usersitemsID, usersitemsItemID, usersitemsUserId));
                itemGo.SetActive(false);
            });
        }
    }
}
