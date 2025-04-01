using UnityEngine;
using UnityEngine.UI;

public class Test2 : MonoBehaviour
{
    [SerializeField] InputField test_input;

    private Test test_loader;
    private TestData test;
    private void Start()
    {
        test = new(" ");
        test_loader = GetComponent<Test>();
    }

    public void LoadData()
    {
        test = test_loader.LoadTestData();
        Debug.Log(JsonUtility.ToJson(test));
    }

    public void SaveData() 
    {
        Debug.Log("123");
        test.test_text = test_input.text;
        test_loader.SaveTestData(test);
    }
}
