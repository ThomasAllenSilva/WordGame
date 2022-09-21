using UnityEngine;


public class MainCameraResolutionScaler : MonoBehaviour
{
    [SerializeField] private float orthoSize;

    [SerializeField] private float aspect;

    private Camera mainCamera;

    private void Awake() => mainCamera = GetComponent<Camera>();

    private void Start()
    {
        mainCamera.projectionMatrix = Matrix4x4.Ortho(-orthoSize * aspect, orthoSize * aspect, -orthoSize, orthoSize, mainCamera.nearClipPlane, mainCamera.farClipPlane);
    }
}
