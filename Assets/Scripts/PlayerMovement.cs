using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    BoxCollider2D box;
    public float jumpForce;
    public int jumpCount;
    public float moveSpeed;
    float dirX;
    public LayerMask jumpable;
    Animator anim;
    SpriteRenderer sprite;
    [SerializeField] UIManager uiManager;
    public int fruitCollected;
    public int playerHealth = 3;
    public bool tookDamage;
    enum MovementState { idle, running, jumping, falling, doubleJump, hit }
    MovementState state;

    public float timer = 10;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(uiManager.state == UIManager.GameState.InGame)
        {
            dirX = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2 (dirX * moveSpeed,rb.linearVelocity.y);

            if (Input.GetButtonDown("Jump"))
            {
                if((jumpCount < 1) && IsGrounded())
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                    jumpCount++;
                }
                else if (jumpCount < 2)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                    jumpCount++;
                }
            }
        }
        UpdateAnimations();
    }

    void UpdateAnimations()
    {
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.linearVelocity.y > 0.1f)
        {
            if (jumpCount == 1)
                state = MovementState.jumping;
            else if (jumpCount == 2)
                state = MovementState.doubleJump;
        }
        else if (rb.linearVelocity.y < -0.1f)
        {
            state = MovementState.falling;
        }
        else
            jumpCount = 0;

        if (tookDamage && state != MovementState.hit)
        {
            state = MovementState.hit;
        }

        anim.SetInteger("MovementState", (int)state);
    }

    bool IsGrounded()
    {
        return Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, .1f, jumpable);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fruit"))
        {
            fruitCollected++;
            other.GetComponent<Animator>().SetBool("Collected", true);
            uiManager.UpdateCounter(fruitCollected);
        }

        if ((other.CompareTag("Damage") && !tookDamage ))
        {
            TakeDamage();
        }

        if (other.CompareTag("Win"))
            Win();

        if (other.CompareTag("Health"))
        {
            Heal();
            Destroy(other.gameObject);
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Damage"))
        {

            if (timer > 0) 
            {
                timer -= 1 * Time.deltaTime;
            }
            else if((timer <= 0) && !tookDamage)
            {
                TakeDamage();
                timer = 1;
            }
        }

    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Death"))
            Die();
    }

    public void TakeDamage()
    {
        playerHealth--;
        tookDamage = true;
        uiManager.UpdateHealth();
        if (playerHealth == 0)
            Die();
    }
    void Heal()
    {
        if (playerHealth != 0)
        {
            playerHealth++;
        }
        uiManager.UpdateHealth();
    }

    void Die()
    {
        anim.SetTrigger("Death");
        rb.bodyType = RigidbodyType2D.Static;
        uiManager.state = UIManager.GameState.Death;
    }

    void Win()
    {
        //rb.bodyType = RigidbodyType2D.Static;
        uiManager.state = UIManager.GameState.Win;
        anim.SetTrigger("Win");
    }
}
