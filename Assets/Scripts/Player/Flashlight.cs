using System;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] float lightDecay;
    [SerializeField] float angleDecay;
    [SerializeField] float minAngle;

    Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        DecreaseLightIntensity();
        DecreaseLightAngle();
    }

    public void RestoreLightIntensity(float restoreIntensity)
    {
        myLight.intensity = restoreIntensity;
    }

    public void RestoreLightAngle(float restoreAngle)
    {
        myLight.spotAngle = restoreAngle;
    }

    void DecreaseLightIntensity()
    {
        myLight.intensity -= lightDecay * Time.deltaTime;
    }

    void DecreaseLightAngle()
    {
        if (myLight.spotAngle <= minAngle) return;
        
        myLight.spotAngle -= angleDecay * Time.deltaTime;
    }
}
