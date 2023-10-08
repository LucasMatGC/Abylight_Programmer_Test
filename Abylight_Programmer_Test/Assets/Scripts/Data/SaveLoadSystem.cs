using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace Assets.Scripts.Data
{
    public static class SaveLoadSystem
    {

        /// <summary>
        ///Saves the data in a persistent file
        /// </summary>
        public static void SaveData(string newData, string filename)
        {

            string path = Application.persistentDataPath + "/" + filename;
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {

                formatter.Serialize(stream, newData);
                stream.Close();

                Debug.Log("File saved succesfully in route: " + Application.persistentDataPath + "/" + filename);
            }


        }

        /// <summary>
        ///Loads the data from the game.
        /// OnEnable.
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

                    Debug.Log("File loaded succesfully from route: " + Application.persistentDataPath + "/" + filename);

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