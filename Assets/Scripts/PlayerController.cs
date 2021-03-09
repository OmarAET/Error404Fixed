using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public bool hideCursor = true;

    const float speed = 35;
    const float jumpSpeed = 25;
    const float gravityScale = 10;
    const float airControl = 12;
    const float groundControl = 10;
    const float maxSpeed = 40;

    float h, flip, velocityX, lerp;
    Vector2 velocity;
    Vector3 flipScale = new Vector3();
    RaycastHit2D groundHit;
    Rigidbody2D rb;
    PhysicsMaterial2D mat;
    LayerMask mask = 1;
    SpriteRenderer spriteRenderer;

    Animator anim;

    void Awake()
    {
        Time.fixedDeltaTime = 1 / 100f;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        mat = new PhysicsMaterial2D();
        mat.friction = 0;
        rb.sharedMaterial = mat;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Cursor.visible = !hideCursor;

        InvokeRepeating("Clock", 1, 1);
    }

    void Update()
    {
        // input
        h = Input.GetAxisRaw("Horizontal");

        groundHit = Physics2D.CircleCast(rb.position, 0.6f, Vector2.zero, 0, mask.value);

        lerp = airControl; // air control
        mat.friction = 0;
        if (groundHit) // grounded
        {
            lerp = groundControl;
            mat.friction = 1;
            if (Input.GetButtonDown("Jump")) { rb.velocity += Vector2.up * jumpSpeed; }
        }

        // flip sprite
        if (Mathf.Abs(h) > 0.01f)
        {
            spriteRenderer.flipX = h < 0f;
        }
    }

    void FixedUpdate()
    {
        if (Physics2D.Linecast(rb.position - Vector2.right * 0.7f, rb.position + Vector2.right * 0.7f, mask.value)) { h *= 0.2f; }

        velocity.Set((velocityX = Mathf.Lerp(velocityX, h, Time.deltaTime * lerp)) * speed, rb.velocity.y);
        rb.velocity = velocity;

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);

        if (groundHit)
        {
            Rigidbody2D r = groundHit.collider.GetComponentInParent<Rigidbody2D>();
            if (r != null) { rb.AddForceAtPosition(r.velocity * 0.5f, groundHit.point, ForceMode2D.Force); } // stick to stuffs
        }
    }

    void Clock()
    {
        if (transform.position.y < -170) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    }
}
