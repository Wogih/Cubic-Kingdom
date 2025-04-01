using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private string test_path;

    private void Start()
    {
        test_path = Application.persistentDataPath + "/playerData.dat";
    }

    public void SaveTestData(TestData data)
    {
        BinaryFormatter formatter = new();
        FileStream stream = new(test_path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Данные сохранены!");
    }

    public TestData LoadTestData()
    {
        if (File.Exists(test_path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(test_path, FileMode.Open);

            TestData data = formatter.Deserialize(stream) as TestData;
            stream.Close();
            Debug.Log("Данные загружены!");
            return data;
        }
        else
        {
            Debug.LogError("Файл не найден!");
            return null;
        }
    }
}

[Serializable]
public class TestData
{
    public string test_text;
    
    public TestData(string text)
    {
        test_text = text;
    }
}