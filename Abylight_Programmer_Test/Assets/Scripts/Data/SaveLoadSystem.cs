using Assets.Scripts.Utils;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace Assets.Scripts.Data
{
    public static class SaveLoadSystem
    {

        //Saves the data in a persistent file
        public static void SaveData(string newData)
        {

            string path = Application.persistentDataPath + "/" + Constants.DownloadFileName;
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {

                formatter.Serialize(stream, newData);
                stream.Close();

                Debug.Log("File saved succesfully in route: " + Application.persistentDataPath + "/" + Constants.DownloadFileName);
            }


        }

        //Loads the data from the game.
        public static string LoadData(string filename)
        {

            string path = Application.persistentDataPath + "/" + filename;
            if (File.Exists(path))
            {

                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {

                    string data = formatter.Deserialize(stream) as string;
                    stream.Close();

                    Debug.Log("File loaded succesfully from route: " + Application.persistentDataPath + "/" + Constants.DownloadFileName);

                    return data;

                }

            }
            else
            {

                return null;

            }

        }
    }
}