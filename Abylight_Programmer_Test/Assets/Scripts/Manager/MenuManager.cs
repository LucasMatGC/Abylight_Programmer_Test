using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Assets.Scripts.Utils;
using System;
using Assets.Scripts.Data;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private TextMeshProUGUI downloadedText;

    public List<Register> registers = new List<Register>();

    public void DownloadData()
    {

        // A non-existing page.
        StartCoroutine(GetRequest(Constants.MenuURL));

    }

    IEnumerator GetRequest(string uri)
    {
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
                    SaveData(webRequest.downloadHandler.text);
                    ManageData();
                    break;
            }
        }
    }

    private void ManageData()
    {

        string data = SaveLoadSystem.LoadData(Constants.DownloadFileName);

        string[] rows = data.Split("\n");

        for (int i = 1; i < rows.Length; i++)
        {
            RegisterType newType;

            int firstDelimiter = rows[i].IndexOf(',');
            int secondDelimiter = rows[i].LastIndexOf(',');
            string firstValue = rows[i][0..firstDelimiter];
            string secondValue = rows[i][(firstDelimiter + 1)..secondDelimiter];
            string thirdValue = rows[i][(secondDelimiter + 1)..(rows[i].Length-1)];

            switch (firstValue)
            {

                case "car":
                    newType = RegisterType.car;
                    break;

                case "character":
                    newType = RegisterType.character;
                    break;

                case "building":
                    newType = RegisterType.building;
                    break;

                default:
                    Debug.LogError("The row " + (i - 1) + " has invalid data in the first column.");
                    continue;

            }
            
            Register newRegister = new Register(newType, secondValue, Int32.Parse(thirdValue));
            registers.Add(newRegister);

        }

        PrintData();

    }

    private void SaveData(string data)
    {

        SaveLoadSystem.SaveData(data);

    }

    private void PrintData()
    {

        string data = "";

        foreach (Register register in registers)
        {

            data = data + register.GetContent();

        }

        downloadedText.text = data;

    }

    public void GotoInit()
    {

        FlowManager._flowManager.GotoInit();

    }
}
