using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneByID : MonoBehaviour
{
    public void LoadScene(int idScene)
    {
        SceneManager.LoadScene(idScene);
    }
}
