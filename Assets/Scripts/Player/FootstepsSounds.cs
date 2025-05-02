using StarterAssets;
using UnityEngine;

public class FootstepsSounds : MonoBehaviour
{
    [SerializeField] AudioClip walkingSound;
    [SerializeField] AudioClip runningSound;

    AudioSource audioSource;

    StarterAssetsInputs inputs;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();   
        inputs = FindFirstObjectByType<StarterAssetsInputs>(); 
    }

    void Update()
    {
        if (inputs.move.magnitude > 0.1f && !inputs.sprint) 
        {

            if (!audioSource.isPlaying) 
            {
                audioSource.PlayOneShot(walkingSound, 0.3f);

            }
        }
        else if (inputs.move.magnitude > 0.1f && inputs.sprint) 
        {
            if (!audioSource.isPlaying) 
            {
                audioSource.PlayOneShot(runningSound, 0.4f);
 
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
