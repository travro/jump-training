using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class RobotBoyController : MonoBehaviour
{

    public Rigidbody2D ThisBody = null;
    public BoxCollider2D ThisCollider = null;
    private Animator ThisAnimator;
    private GameObject rockets;
    public bool isGrounded = false;
    public string JumpButton = "Jump";
    public float JumpPower = 200;
    public float RocketPower = 10;
    public float MaxFuelLevel = 100f;
    public float _FuelLevel = 100f;

    public float FuelBurnRate = 35f;
    public float FuelRegenRate = 20f;
    //private bool CanJump = true;
    public static RobotBoyController RBCInstance = null;
    public float FuelLevel
    {
        get{
            return _FuelLevel;
        }
    }

    void Awake()
    {
        ThisBody = GetComponent<Rigidbody2D>();
        ThisCollider = GetComponent<BoxCollider2D>();
        ThisAnimator = GetComponent<Animator>();

        RBCInstance = this;
    }

    // Use this for initialization
    void Start()
    {
        rockets = GameObject.Find("Rockets");
        rockets.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = CheckForGrounded();

        //Fuel Regenerates when grounded because video game logic
        _FuelLevel += (_FuelLevel < MaxFuelLevel && isGrounded) ? FuelRegenRate : 0;
        if (_FuelLevel > MaxFuelLevel) _FuelLevel = MaxFuelLevel;

        //Animation Handler for Jumping
        if (ThisBody.velocity.y > 0 && !isGrounded)
        {
            ThisAnimator.SetInteger("PlayerState", 1);
        }
        else if (ThisBody.velocity.y < 0 && !isGrounded)
        {
            ThisAnimator.SetInteger("PlayerState", 2);
        }
        else
        {
            ThisAnimator.SetInteger("PlayerState", 3);
        }

        //Jump with rockets and burn fuel
        if (CrossPlatformInputManager.GetButton(JumpButton) && !isGrounded && FuelLevel > 0)
        {
            rockets.SetActive(true);
            Jump();
            _FuelLevel -= FuelBurnRate;
        }
        //Normal jump without rockets
        else if (CrossPlatformInputManager.GetButton(JumpButton) && isGrounded)
        {
            Jump();
        }
        //Shutoff Rockets
        else
        {
            rockets.SetActive(false);
        }
    }

    private void Jump()
    {
        if (!isGrounded)
        {
            ThisBody.AddForce(Vector2.up * JumpPower / 100 * RocketPower);
            //return;
        }
        else
        {
            ThisBody.AddForce(Vector2.up * JumpPower);
        }
    }

    private bool CheckForGrounded()
    {
        //check if this collider is touching any other colliders
        return ThisCollider.IsTouchingLayers(Physics2D.AllLayers);
    }

    public static void Die()
    {
        Destroy(RobotBoyController.RBCInstance.gameObject);
    }
}
