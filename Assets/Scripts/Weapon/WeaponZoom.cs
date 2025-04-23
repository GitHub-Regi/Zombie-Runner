using Cinemachine;
using StarterAssets;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera povCamera;
    [SerializeField] FirstPersonController fpController;
    [SerializeField] float zoomedOutFOV;
    [SerializeField] float zoomedInFOV;
    [SerializeField] float regularSens;
    [SerializeField] float zoomedSens;

    StarterAssetsInputs inputs;

    void OnDisable()
    {
        povCamera.m_Lens.FieldOfView = zoomedOutFOV;
        fpController.RotationSpeed = regularSens;
    }

    void Start()
    {
        inputs = FindFirstObjectByType<StarterAssetsInputs>();
        if (inputs == null)
        {
            Debug.LogError("StarterAssetsInputs not found in scene.");
        }
    }

    void Update()
    {
        if (inputs != null && inputs.zoom)
        {
            povCamera.m_Lens.FieldOfView = zoomedInFOV;
            fpController.RotationSpeed = zoomedSens;
        }
        else if (inputs != null && !inputs.zoom)
        {
            povCamera.m_Lens.FieldOfView = zoomedOutFOV;
            fpController.RotationSpeed = regularSens;
        }
    }
}
