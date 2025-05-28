using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
        public void Replay()
        {
            SceneManager.LoadScene(1);
        }
        public void Exit()
        {
            Application.Quit();
        }
    }
