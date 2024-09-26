using UnityEngine;
using UnityEngine.UI;

public class GradientTexture : MonoBehaviour
{
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _image; 
    public int textureWidth = 256;

    void Start()
    {
        Texture2D gradientTexture = new Texture2D(textureWidth, 1);

        for (int x = 0; x < textureWidth; x++)
        {
            float t = (float)x / (textureWidth - 1);
            gradientTexture.SetPixel(x, 0, _gradient.Evaluate(t));
        }

        gradientTexture.Apply();

        // Apply the texture to the material
        _image.material.mainTexture = gradientTexture;
    }
}