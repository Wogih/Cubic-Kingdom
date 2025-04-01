using UnityEngine;
using UnityEngine.SceneManagement;

public class Play_button : MonoBehaviour
{
    public void StartGame(int game_scene_id)
    {
        SceneManager.LoadScene(game_scene_id);
    }
}
