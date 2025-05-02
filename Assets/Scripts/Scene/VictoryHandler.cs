using StarterAssets;
using UnityEngine;

public class VictoryHandler : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Canvas victoryCanvas;

    void Start()
    {
        victoryCanvas.enabled = false;
    }

    public void HandleVictory()
    {
        victoryCanvas.enabled = true;
        
        audioSource.mute = true;
        Time.timeScale = 0;
        FindFirstObjectByType<FirstPersonController>().enabled = false;
        FindFirstObjectByType<WeaponSwitcher>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
