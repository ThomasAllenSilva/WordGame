using UnityEngine;

public class MoveBackgroundRepeatedly : MonoBehaviour
{
    Material meshRendererMaterial;

    private void Awake()
    {
        meshRendererMaterial = GetComponent<MeshRenderer>().material;
    }

    private void FixedUpdate()
    {
        meshRendererMaterial.mainTextureOffset += Vector2.right * Time.fixedDeltaTime / 16;
    }
}
