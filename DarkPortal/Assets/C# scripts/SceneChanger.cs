using UnityEngine;
using UnityEngine.SceneManagement;

namespace C__scripts
{
    public class SceneChanger : MonoBehaviour
    {
        public void PlayPressed()
        {
            SceneManager.LoadScene("Game");
        }

        public void ExitPressed()
        {
            Debug.Log("Exit pressed!");
            Application.Quit();
        }
    }
}
