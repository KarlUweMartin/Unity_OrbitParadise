using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
[ExecuteInEditMode]
public class ResponsiveCanvas : MonoBehaviour
{
    void Start()
    {
        var canvas = GetComponent<CanvasScaler>();
        var f = Remap(Screen.height, 0, 3840, .8f, 5f);
        canvas.scaleFactor = f;
    }

    private float Remap(float value, float A, float B, float X, float Y)
    {
        return (value - A) / (B - A) * (Y - X) + X;
    }
}
