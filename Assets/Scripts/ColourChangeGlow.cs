using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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
    public bool isPlayer;
    public float damageDelay = 1f;
    public float timer = 3f;
  
    public PlayerMovement player;

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
        while (t < 1f)
        {
            t += Time.deltaTime / transitionDuration;
            spriteRenderer.color = Color.Lerp(from, to, t);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayer = true;
        }
        if (isPlayer == true && isOriginalColor == false)
        {
            player.TakeDamage();
      
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isPlayer == true)
        {
            if (timer > 0)
            {
                timer -= 1 * Time.deltaTime;
            }

            else if ((timer <= 0) && isOriginalColor == false)
            {
                player.TakeDamage();
                timer = 3;
            }

        

           
             
        }
    }

}