using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [SerializeField]
    private float topSpeed;  //topspeed for moving 
    [SerializeField]
    private float accelSpeed; //rate to accelerato to the target speed
    [SerializeField]
    private float decelSpeed; //default rate to decellerate to target speed
    [SerializeField]
    private float currentDecelSpeed;  //actual rate to decellerate to target speed can be changed during play for things like jumping
    [SerializeField]
    private float velPower; // speed multiplier
    [SerializeField]
    private float friction; // extra deceliration for when grounded
    [SerializeField]
    private float jumpForce; // force applied when performing a standerd jump
    private bool jumping; // currently jumping?
    [SerializeField]
    private float specialJumpPower; //force applied whem performing a special jump
    private bool specialJumping;
    [SerializeField]
    private Rigidbody2D rb; // players riged boby
    [SerializeField]
    private Camera playerCam; // the main camare attached to the player
    [SerializeField]
    private Transform groundcheckpoint; // gameobject that checks for the ground (at bottem of player)
    [SerializeField]
    private float groundCheckSize; // radious 
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private bool grounded;

    [SerializeField]
    private float standardGravety; //gravety when jumping
    [SerializeField]
    private float fallGravety; // gravety when falling  
    [SerializeField]
    private float apexGravety;  // gravety at the apex of the jump (lower to make it feel more controlled)
    [SerializeField]
    private float apexVelocity; // speed at witch the apex gravety is applied
    [SerializeField]
    private float maxFallSpeed; // the maximum speed for falling

    //grace values
    [SerializeField]
    private float caotyTime; // time to jump after leaving a platform
    private float caotyTimeCounter;


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
        float accelRate;
        if (Mathf.Abs(targetSpeed) > 0.01f)
        {
            accelRate = accelSpeed;
        }
        else
        {
            accelRate = currentDecelSpeed;
        }
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
        #region deceliration
        //slowly return deceleration when it is low. (special jumping sets it to a low value te make horizontal jumps better)
        if(currentDecelSpeed < decelSpeed)
        {
            currentDecelSpeed += 0.2f;
        }
        #endregion

        SetGravety();
    }

    private void CheckGrounded()
    {
        if (rb.velocity.y < 0.1f && Physics2D.OverlapCircle(groundcheckpoint.position, groundCheckSize, groundLayer))
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
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumping = true;
    }

    //jumpo in the direction of the mouse
    private void SpecialJump()
    {
        Vector2 mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 jumpVector = mousePos - rb.position;
        //limits the jump force 
        if (jumpVector.magnitude > 3)
        {
            float scaler = 3 / jumpVector.magnitude;
            jumpVector = jumpVector * scaler;
        }
        rb.velocity = new Vector2(rb.velocity.x,0);
        rb.AddForce(jumpVector * specialJumpPower, ForceMode2D.Impulse);
        specialJumping = true;
        currentDecelSpeed = 0;
    }

    //manages gravety for the player. higher when falling and lowest when at apex of jump. turns gravety off when reaching max fall speed.
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
