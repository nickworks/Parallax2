using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    /// <summary>
    /// A reference to the CharacterController component.
    /// </summary>
    CharacterController pawn;

    /// <summary>
    /// Horizontal acceleration used when the user presses left or right.
    /// </summary>
    public float acceleration = 10;
    /// <summary>
    /// Horizontal deceleration used when the user is NOT pressing left or right).
    /// </summary>
    public float deceleration = 10;
    /// <summary>
    /// The acceleration to use for gravity.
    /// </summary>
    public float gravity = 10;
    /// <summary>
    /// The take-off velocity to use when the player jumps.
    /// </summary>
    public float jumpVelocity = 100;

    /// <summary>
    /// This variable is used to adjust jump height. When true, less gravity is applied.
    /// </summary>
    bool isJumping = false;
    /// <summary>
    /// This variable tracks whether or not the player is on the ground.
    /// </summary>
    bool isGrounded = false;

    /// <summary>
    /// This is how long it will take (in seconds) for this object to realize it's not on the ground.
    /// </summary>
    public float groundTimeAmount = .2f;
    /// <summary>
    /// The timer that counts down before forgetting the ground.
    /// </summary>
    float forgetTheGroundTimer = 0;


    void Start () {
        pawn = GetComponent<CharacterController>();
	}
	
	void Update ()
    {
        GroundDetection();
        Move();
    }

    private void GroundDetection()
    {
        if (pawn.isGrounded)
        {
            isGrounded = true;
            forgetTheGroundTimer = groundTimeAmount;
        }
        else
        {
            if (forgetTheGroundTimer > 0)
            {
                forgetTheGroundTimer -= Time.deltaTime;
                if (forgetTheGroundTimer <= 0) isGrounded = false;
            }
        }
    }

    private void Move()
    {
        Vector3 velocity = pawn.velocity;
        //print(velocity);
        MoveHorizontal(ref velocity);
        MoveVertical(ref velocity);
        pawn.Move(velocity * Time.deltaTime);
    }

    private void MoveHorizontal(ref Vector3 velocity)
    {
        float h = Input.GetAxisRaw("Horizontal");
        velocity += Time.deltaTime * new Vector3(h, 0, 0) * acceleration;

        if (h == 0)
        {
            if (velocity.x > 0)
            {
                velocity.x -= deceleration * Time.deltaTime;
                if (velocity.x < 0) velocity.x = 0;
            }
            if (velocity.x < 0)
            {
                velocity.x += deceleration * Time.deltaTime;
                if (velocity.x > 0) velocity.x = 0;
            }
        }
    }

    private void MoveVertical(ref Vector3 velocity)
    {
        if (isJumping)
        {
            if (!Input.GetButton("Jump")) isJumping = false; // cancel jump
            if (velocity.y < 0) isJumping = false;
        }

        float gravityMultiplier = isJumping ? .5f : 1;
        velocity += Time.deltaTime * new Vector3(0, -gravity, 0) * gravityMultiplier;

        if (Input.GetButtonDown("Jump"))
        {
            print("jump pressed");
            if (isGrounded)
            {
                velocity.y = jumpVelocity;
                isJumping = true;
                isGrounded = false;
                
            } else
            {
                print("Not grounded and yet pawn.isGrounded is " + pawn.isGrounded);
            }
        }
    }
}
