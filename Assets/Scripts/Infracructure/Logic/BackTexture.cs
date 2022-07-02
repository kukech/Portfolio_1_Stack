using UnityEngine;
using Random = UnityEngine.Random;

public class BackTexture : MonoBehaviour
{
    private Texture2D texture;
    private int resolutionX = 1;
    private int resolutionY = 2;

    private float speedSmooting = 4f;

    private Color randomColorOne;
    private Color randomColorTwo;
    private Color secondColorOne;
    private Color secondColorTwo;

    private void Start()
    {
        if (texture == null)
        {
            texture = new Texture2D(resolutionX, resolutionY);
            GetComponent<Renderer>().material.mainTexture = texture;
        }
        texture.wrapMode = TextureWrapMode.Clamp;
        AdjustToCameraSize();

        //init colors
        randomColorTwo = new Color(Random.value * 0.5f + 0.2f, Random.value * 0.5f + 0.2f, Random.value * 0.5f + 0.2f);
        ColorGenerate();
        secondColorOne = randomColorOne;
        secondColorTwo = randomColorTwo;
        RefreshTexture();
    }

    public void ColorGenerate()
    {
        randomColorOne = randomColorTwo;

        float newColorR = Mathf.Clamp(randomColorTwo.r + (Random.value * 0.2f - 0.1f), 0.2f, 0.9f);
        float newColorG = Mathf.Clamp(randomColorTwo.g + (Random.value * 0.2f - 0.1f), 0.2f, 0.9f);
        float newColorB = Mathf.Clamp(randomColorTwo.b + (Random.value * 0.2f - 0.1f), 0.2f, 0.9f);

        randomColorTwo = new Color(newColorR, newColorG, newColorB);
    }

    private void Update()
    {
        RefreshTexture();
    }

    private void RefreshTexture()
    {
        secondColorOne = Color.Lerp(secondColorOne, randomColorOne, Time.deltaTime * speedSmooting);
        secondColorTwo = Color.Lerp(secondColorTwo, randomColorTwo, Time.deltaTime * speedSmooting);
        texture.SetPixel(0, 0, secondColorOne);
        texture.SetPixel(0, 1, secondColorTwo);
        texture.Apply();
    }

    private void AdjustToCameraSize()
    {
        Vector3 offset = Vector3.one;
        offset.x = Camera.main.orthographicSize * 9 / 16 * 2;
        offset.y = Camera.main.orthographicSize * 2;
        transform.localScale = offset;
    }
}
