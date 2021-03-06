﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This handles the player avatar behavior (physics + input)
/// </summary>
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(LayerFixed))]
[RequireComponent(typeof(HealthAndRespawn))]
public class PlayerController : MonoBehaviour {

    /// <summary>
    /// Provides easy, singleton-like access to the player.
    /// This assumes we will only ever have one PlayerController.
    /// If we add multiplayer, we will have to redesign this.
    /// </summary>
    public static PlayerController player { get; private set; }

    private PlayerState state;

    /// <summary>
    /// A reference to a prefab canvas that instanciates when the player is spawned
    /// </summary>
    public HUDController prefabHUD;
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
    /// Maximum horizontal speed.
    /// </summary>
    public float maxSpeedHorizontal = 10;
    /// <summary>
    /// Maximum vertical speed.
    /// </summary>
    public float maxSpeedVertical = 10;
    /// <summary>
    /// The height of the player's jump, in meters.
    /// </summary>
    public float jumpHeight = 5;
    /// <summary>
    /// The amount of time (in seconds) that it should take the player to reach the peak of their jump arc.
    /// </summary>
    public float jumpTime = 1;
    /// <summary>
    /// The acceleration to use for gravity. This will be set by the DeriveJumpValues() function.
    /// </summary>
    private float gravity = 20;
    /// <summary>
    /// The take-off velocity to use when the player jumps. This will be set by the DeriveJumpValues() function.
    /// </summary>
    private float jumpVelocity = 1;
    /// <summary>
    /// How many air-jumps does the player have left?
    /// </summary>
    public int airJumpsCount { private set; get; }
    /// <summary>
    /// How many air-jumps should the player get?
    /// </summary>
    public int airJumpsMax = 2;
    /// <summary>
    /// This variable is used to adjust jump height. When true, less gravity is applied.
    /// </summary>
    bool isJumping = false;
    /// <summary>
    /// This variable tracks whether or not the player is on the ground.
    /// </summary>
    public bool isGrounded { get; private set; }
    /// <summary>
    /// Reference to the dead player avatar.
    /// </summary>
    public Transform ragdoll;
    /// <summary>
    /// Reference to the alive player avatar.
    /// </summary>
    public Transform billboard;
    /// <summary>
    /// Ref to the LayerFixed object that allows the player to phase jump.
    /// </summary>
    private LayerFixed layerController;
    /// <summary>
    /// This is how long it will take (in seconds) for this object to realize it's not on the ground.
    /// </summary>
    public float groundTimeAmount = .2f;
    /// <summary>
    /// The timer that counts down before forgetting the ground.
    /// </summary>
    float forgetTheGroundTimer = 0;
    /// <summary>
    /// The velocity we want to move the player. This is calculated each frame.
    /// </summary>
    private Vector3 velocity;
    /// <summary>
    /// Initializes the object. Called when spawning.
    /// </summary>
    void Start() {
        PlayerController.player = this;
        pawn = GetComponent<CharacterController>();
        layerController = GetComponent<LayerFixed>();
        DeriveJumpValues();
        if (prefabHUD)
        {
            HUDController playerHUD = Instantiate(prefabHUD);
            playerHUD.SetPlayer(this);
        }
    }
    /// <summary>
    /// This message is called when values are updated in the inspector.
    /// </summary>
    void OnValidate() {
        DeriveJumpValues();
    }
    /// <summary>
    /// Calculates the gravity and jump takeoff velocity,
    /// based on desired jump height and jump time.
    /// </summary>
    void DeriveJumpValues() {
        gravity = (jumpHeight * 2) / (jumpTime * jumpTime);
        jumpVelocity = gravity * jumpTime;
    }
    /// <summary>
    /// Changes the player state.
    /// </summary>
    /// <param name="nextState">The state to change to.</param>
    void ChangeState(PlayerState nextState)
    {
        if (nextState == null) return;
        if (state != null) state.Exit();
        state = nextState;
        state.Enter(this);
    }
    /// <summary>
    /// The game ticks forward one frame.
    /// </summary>
    void Update() {

        if (Px2.paused) return; // do nothing if game is paused...

        //if (state != null) ChangeState(state.Update());

        PhaseJump((int)Input.GetAxisRaw("Vertical"));
        GroundDetection();
        Move();
        //if (jetpackFuel <= 0) jetpackFumes.SetActive(false);
        
    }
    /// <summary>
    /// Checks input, asks the LayerFixed script to phase jump.
    /// </summary>
    /// <param name="dir">If greater than 0, moves away from camera. If less than 0, comes towards camera.</param>
    private void PhaseJump(int dir) {
        if (dir > 0) layerController.GoBack();
        if (dir < 0) layerController.ComeForward();
        if (dir == 0) layerController.CancelTimer();
    }

    /// <summary>
    /// Creates a timing window for late jump presses.
    /// </summary>
    private void GroundDetection() {
        if (pawn.isGrounded) // ground is detected, so do this stuff:
        {
            isGrounded = true; // set our grounded flag to true
            forgetTheGroundTimer = groundTimeAmount; // start the countdown timer...
            airJumpsCount = airJumpsMax;
        } else // ground is NOT detected, so do this stuff:
        {
            if (forgetTheGroundTimer > 0) // if there is a countdown timer...
            {
                forgetTheGroundTimer -= Time.deltaTime; // count down
                if (forgetTheGroundTimer <= 0) isGrounded = false; // when it hits 0, set our grounded flag to false
            }
        }
    }


    /// <summary>
    /// Handles all player movement physics: calculating velocity, moving, collision detection / response
    /// </summary>
    private void Move() {
        // calculate total velocity:
        MoveHorizontal();
        MoveVertical();

        // move the player:
        CollisionFlags flags = pawn.Move(velocity * Time.deltaTime); // does collision detection for us :D

        // collisions affect velocity:
        if ((flags & CollisionFlags.Sides) > 0) velocity.x = 0;

        if ((flags & CollisionFlags.Above) > 0) {
            if (velocity.y > 0) velocity.y = 0;
            isJumping = false;
        }
        if ((flags & CollisionFlags.Below) > 0) velocity.y = 0;

    }

    /// <summary>
    /// Handles horizontal player movement: acceleration, deceleration, maxspeed
    /// </summary>
    private void MoveHorizontal() {
        // acceleration:
        float h = Input.GetAxisRaw("Horizontal");
        velocity += Time.deltaTime * new Vector3(h, 0, 0) * acceleration;
        // decleration:
        if (h == 0) {
            if (velocity.x > 0) {
                velocity.x -= deceleration * Time.deltaTime;
                if (velocity.x < 0) velocity.x = 0;
            }
            if (velocity.x < 0) {
                velocity.x += deceleration * Time.deltaTime;
                if (velocity.x > 0) velocity.x = 0;
            }
        }
        // clamp to maxSpeed:
        if (velocity.x > maxSpeedHorizontal) velocity.x = maxSpeedHorizontal;
        if (velocity.x < -maxSpeedHorizontal) velocity.x = -maxSpeedHorizontal;
    }

    /// <summary>
    /// Handles vertical player movement: gravity, jumping 
    /// </summary>
    private void MoveVertical() {
        if (isJumping) {
            if (!Input.GetButton("Jump")) isJumping = false; // cancel jump
            if (velocity.y < 0) isJumping = false;
        }

        float gravityMultiplier = isJumping ? 1 : 2;
        velocity += Time.deltaTime * new Vector3(0, -gravity, 0) * gravityMultiplier;

        if (Input.GetButtonDown("Jump")) {
            if (isGrounded) // if on the ground, jump:
            {
                velocity.y = jumpVelocity;
                isJumping = true;
                isGrounded = false;
            } else if (airJumpsCount > 0) // otherwise, if has airjumps left, do an airjump:
            {
                velocity.y = jumpVelocity;
                isJumping = true;
                airJumpsCount--;
            }
        }
        // clamp to maxSpeed:
        if (velocity.y > maxSpeedVertical) velocity.y = maxSpeedVertical;
        if (velocity.y < -maxSpeedVertical) velocity.y = -maxSpeedVertical;
    }

    /// <summary>
    /// Activates the ragdoll physics.
    /// </summary>
    void Ragdoll(Vector3 fromHere) {
        // swap avatars:
        ragdoll.transform.parent = transform.parent;
        ragdoll.gameObject.SetActive(true);

        // apply force to ragdoll:
        Rigidbody body = ragdoll.GetComponentInChildren<Rigidbody>();
        Vector3 dis = fromHere - transform.position;
        body.AddForce(-dis.normalized * 10, ForceMode.Impulse);
        body.AddTorque(Random.onUnitSphere * 10, ForceMode.Impulse);

        Destroy(gameObject);
    }
    /// <summary>
    /// Accelerates the player by adding to the velocity.
    /// </summary>
    /// <param name="amount">The amount to add to velocity.</param>
    /// <param name="applyDeltaTime">Whether or not to apply delta-time to the amount.</param>
    public void Impulse(Vector3 amount, bool applyDeltaTime = false)
    {
        if (applyDeltaTime) amount *= Time.deltaTime;
        velocity += amount;
    }
    /// <summary>
    /// Get the player's current velocity.
    /// </summary>
    /// <returns>The player's current velocity vector.</returns>
    public Vector3 GetVelocity()
    {
        return velocity;
    }
    /// <summary>
    /// Resets the player's velocity back to zero
    /// </summary>
    public void ResetVelocity() {
        velocity = Vector3.zero;
    }
}
