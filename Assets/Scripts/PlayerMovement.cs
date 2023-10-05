using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [SerializeField]
    private float topSpeed;
    [SerializeField]
    private float accelSpeed;
    [SerializeField]
    private float decelSpeed;
    [SerializeField]
    private float velPower;
    [SerializeField]
    private float friction;
    [SerializeField]
    private float jumpForce;
    private bool jumping;
    [SerializeField]
    private float specialJumpPower;
    private bool specialJumping;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Camera playerCam;
    [SerializeField]
    private Transform groundcheckpoint;
    [SerializeField]
    private float groundCheckSize;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private bool grounded;

    [SerializeField]
    private float standardGravety;
    [SerializeField]
    private float fallGravety;
    [SerializeField]
    private float apexGravety;
    [SerializeField]
    private float apexVelocity;
    [SerializeField]
    private float maxFallSpeed;

    //grace values
    [SerializeField]
    private float caotyTime;
    private float caotyTimeCounter;
    [SerializeField]
    private float bufferTime;
    private float bufferTimeCounter;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        #region jump
        CheckGrounded();
        if (grounded)
        {
            caotyTimeCounter = caotyTime;
        }
        else
        {
            caotyTimeCounter -= Time.deltaTime;
        }

        if (caotyTimeCounter > 0 && !jumping && !specialJumping)
        {
            if (Input.GetAxisRaw("Vertical") == 1)
            {
                Jump();

            }
            if (Input.GetMouseButtonDown(0))
            {
                SpecialJump();

            }
        }
        if (jumping && Input.GetAxisRaw("Vertical") < 1)
        {
            jumping = false;
            if (rb.velocity.y > 0)
            {
                Vector2 jumpCancel = new Vector2(0, rb.velocity.y * -0.5f);
                rb.AddForce(jumpCancel, ForceMode2D.Impulse);
            }
            caotyTimeCounter = 0;
        }
        if (specialJumping && Input.GetMouseButtonUp(0))
        {
            specialJumping = false;
            caotyTimeCounter = 0;
        }
        #endregion
    }

    private void FixedUpdate()
    {

        #region run
        // handles movement in the x axis.
        float targetSpeed = Input.GetAxis("Horizontal") * topSpeed;
        float speedDiff = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accelSpeed : decelSpeed;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velPower) * Mathf.Sign(speedDiff);
        rb.AddForce(movement * Vector2.right);
        #endregion
        #region Friction
        //applies friction when grounded and no input is given.(wanting to stop)
        if (grounded == true && Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 0)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(friction));
            amount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
        #endregion

        SetGravety();
    }

    private void CheckGrounded()
    {
        if (Physics2D.OverlapCircle(groundcheckpoint.position, groundCheckSize, groundLayer) && rb.velocity.y < 0.1f)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumping = true;
    }

    private void SpecialJump()
    {
        Vector2 mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 jumpVector = mousePos - rb.position;
        if (jumpVector.magnitude > 3)
        {
            float scaler = 3 / jumpVector.magnitude;
            jumpVector = jumpVector * scaler;
        }
        rb.AddForce(jumpVector * specialJumpPower, ForceMode2D.Impulse);
        specialJumping = true;
    }

    private void SetGravety()
    {
        if (rb.velocity.y < -apexVelocity)
        {
            rb.gravityScale = fallGravety;
        }
        else if (rb.velocity.y > apexVelocity)
        {
            rb.gravityScale = standardGravety;
        }
        else
        {
            rb.gravityScale = apexGravety;
        }

        if (rb.velocity.y < maxFallSpeed)
        {
            rb.gravityScale = 0;
        }
    }
}
