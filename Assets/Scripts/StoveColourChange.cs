using UnityEngine;

public class StoveColourChange : MonoBehaviour
{
    public Color newColor = Color.red;
    public float speed = 1f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        float lerp = Mathf.PingPong(Time.time * speed, 1f);
        spriteRenderer.color = Color.Lerp(originalColor, newColor, lerp);
    }
}
