using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    private static string _filePath;
    private static DataBase _instanceDB;
    public static GameData gameDataGD;

    private void Awake()
    {
        if (_instanceDB == null)
        {
            _instanceDB = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        _filePath = Application.persistentDataPath + "/playerData.dat";
    }

    private void Start()
    {
        gameDataGD = LoadData();
    }

    public void SaveData(GameData data)
    {
        BinaryFormatter formatter = new();
        FileStream stream = new(_filePath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Данные сохранены!");
    }

    public static GameData LoadData()
    {
        if (File.Exists(_filePath))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(_filePath, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            Debug.Log("Данные загружены!");
            return data;
        }
        else
        {
            Debug.LogError("Файл не найден! Создан новый экземпляр");
            return new GameData();
        }
    }
}

[Serializable]
public class GameData
{

}
