using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Assets.Scripts.Utils;
using System;
using Assets.Scripts.Data;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI downloadedText;

    public List<Register> registers = new List<Register>();

    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private FlowManager _flowManager;

    /// <summary>
    /// Start. Ensures the exit button is properly linked
    /// </summary>
    void Start()
    {
        _flowManager = FlowManager._flowManager;
        exitButton.onClick.AddListener(_flowManager.GotoInit);
    }

    /// <summary>
    /// Downloads the data.
    /// </summary>
    public void DownloadData()
    {

        // A non-existing page.
        StartCoroutine(GetRequest(Constants.MenuURL));

    }

    /// <summary>
    /// Sends the web request. If successful, saves the data and proceeds to manage the data
    /// </summary>
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

    /// <summary>
    /// Reads the data from the persistent file and prints it in the text label
    /// </summary>
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

    /// <summary>
    /// Saves the data in a persistent file
    /// </summary>
    private void SaveData(string data)
    {

        SaveLoadSystem.SaveData(data, Constants.DownloadFileName);

    }

    /// <summary>
    /// Prints the data in the text label
    /// </summary>
    private void PrintData()
    {

        string data = "";

        foreach (Register register in registers)
        {

            data = data + register.GetContent();

        }

        downloadedText.text = data;

    }
}
