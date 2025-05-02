using StarterAssets;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        gameOverCanvas.enabled = false;
    }

    public void HandleDeath()
    {
        gameOverCanvas.enabled = true;
        
        audioSource.mute = true;
        Time.timeScale = 0;
        FindFirstObjectByType<FirstPersonController>().enabled = false;
        FindFirstObjectByType<WeaponSwitcher>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
