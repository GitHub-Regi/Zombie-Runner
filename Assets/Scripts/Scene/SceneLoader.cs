using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    StarterAssetsInputs starterAssetsInputs;

    void Start()
    {
        starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        starterAssetsInputs.isInMenu = true;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(1);
        
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

