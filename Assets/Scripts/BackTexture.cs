using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackTexture : MonoBehaviour, IObserver
{
    private Texture2D texture;
    private int resolutionX = 1;
    private int resolutionY = 2;

    [SerializeField]
    private float speedSmooting = 4f;

    private Color randomColorOne;
    private Color randomColorTwo;
    private Color secondColorOne;
    private Color secondColorTwo;

    private void Awake()
    {

    }
    private void Start()
    {
        if (texture == null)
        {
            texture = new Texture2D(resolutionX, resolutionY);
            this.GetComponent<Renderer>().material.mainTexture = texture;
        }
        texture.wrapMode = TextureWrapMode.Clamp;
        randomColorTwo = new Color(Random.value * 0.5f + 0.2f, Random.value * 0.5f + 0.2f, Random.value * 0.5f + 0.2f);
        ColorGenerate();
        secondColorOne = randomColorOne;
        secondColorTwo = randomColorTwo;
        RefreshTexture();
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
    private void ColorGenerate()
    {
        randomColorOne = randomColorTwo;

        float newColorR = Mathf.Clamp(randomColorTwo.r + (Random.value * 0.2f - 0.1f), 0.2f, 0.9f);
        float newColorG = Mathf.Clamp(randomColorTwo.g + (Random.value * 0.2f - 0.1f), 0.2f, 0.9f);
        float newColorB = Mathf.Clamp(randomColorTwo.b + (Random.value * 0.2f - 0.1f), 0.2f, 0.9f);

        randomColorTwo = new Color(newColorR, newColorG, newColorB);
    }

    public void UpdateData(GameEvent state)
    {
        if (state == GameEvent.TILE_DROP)
            ColorGenerate();
    }
}
