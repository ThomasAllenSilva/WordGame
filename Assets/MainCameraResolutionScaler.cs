using UnityEngine;


public class MainCameraResolutionScaler : MonoBehaviour
{
    [SerializeField] private float orthoSize;
    [SerializeField] private float aspect;

    private void Awake()
    {
        Camera.main.projectionMatrix = Matrix4x4.Ortho(-orthoSize * aspect, orthoSize * aspect, -orthoSize, orthoSize, Camera.main.nearClipPlane, Camera.main.farClipPlane);
    }
}
