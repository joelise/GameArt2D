using System.Collections;
using UnityEngine;

public class ColourChangeGlow : MonoBehaviour
{
    public Color newColor = Color.red;
    public float transitionDuration = 1f;

    public float delayOnOriginal = 0.5f;
    public float delayOnNewColor = 1.5f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public bool isOriginalColor = true;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        StartCoroutine(ColorLoop());
    }

    IEnumerator ColorLoop()
    {
        while (true)
        {
            isOriginalColor = false;
            yield return StartCoroutine(FadeColor(originalColor, newColor));
            yield return new WaitForSeconds(delayOnNewColor);

            yield return StartCoroutine(FadeColor(newColor, originalColor));
            isOriginalColor = true;
            yield return new WaitForSeconds(delayOnOriginal);

        }
    }

    IEnumerator FadeColor(Color from, Color to)
    {
       
        float t = 0f;
        while(t < 1f)
        {
            t += Time.deltaTime / transitionDuration;
            spriteRenderer.color = Color.Lerp(from, to, t);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
