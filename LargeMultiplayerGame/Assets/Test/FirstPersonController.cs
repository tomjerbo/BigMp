using System;
using System.Collections;
using System.Collections.Generic;
using ClientCode;
using UnityEngine;
using Random = UnityEngine.Random;

public class FirstPersonController : MonoBehaviour
{
    //public bool isMenuOpen = false;

    [Header("Ref.")]
    [SerializeField] CharacterController cc;
    [SerializeField] Transform head;
    [SerializeField] Transform cam;

    [Header("Motion")]
    Vector3 motion;
    Vector3 airMotion;
    Vector3 inputMotion;
    Vector3 inputLook;

    [HideInInspector] public bool isGrounded;
    float stepOffset;

    [SerializeField] float maxSpeed = 5;
    [SerializeField] float accel = 12;
    [SerializeField] float jumpDash = 1;

    [SerializeField, Range(0, 1)] float airControl = 0.1f;

    [SerializeField] float gravity = 12;
    [SerializeField] float jumpHeight = 1;
    [SerializeField] float jumpDivide = 1.7f;

    bool didInputJump = false;
    bool queJump = false;
    bool snapToGround = false;

    // [Header("Animation")]
    // [SerializeField] Animator fpsRig;
    // [SerializeField] public float tiltDiv = 5;
    // [SerializeField] float tiltSmoothing = 2;
    // Vector3 tiltLerp;
    //

    private void Start()
    {
        stepOffset = cc.stepOffset;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void OnRespawn()
    {
        motion = Vector3.zero;
        airMotion = Vector3.zero;
        mY = 0;
        mX = 0;
        queJump = false;
        //fpsRig.Rebind();
    }
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!isGrounded)
        {
            if ((cc.collisionFlags & CollisionFlags.Sides) != 0)
            {
                motion = Vector3.ProjectOnPlane(motion, hit.normal);
            }
        }

        //Hit Head
        if (airMotion.y > 0 && (cc.collisionFlags & CollisionFlags.Above) != 0) { airMotion.y = 0; }
    }

    void Update()
    {
        
        Look();
        Motion();
        
    }
    

    void Motion()
    {
        inputMotion = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 wishMotion = (head.rotation * inputMotion);

        //Gravity
        airMotion.y -= gravity * Time.deltaTime;

        //Check Jump input!
        if (Input.GetButtonDown("Jump"))
        {
            queJump = true;
        }

        if (cc.isGrounded)
        {

            //This acts as a landing event!
            if (!isGrounded)
            {
                //audioSource.PlayOneShot(GameManager.instance.objects.LandSound());
                isGrounded = true;
                snapToGround = true;
                //fpsRig.CrossFade("fpsLand", 0.08f, -1, 0);
            }

            if (snapToGround) airMotion.y = -10;

            if (queJump)
            {
                queJump = false;
                motion += wishMotion * jumpDash;
                DoJump(jumpHeight);
                didInputJump = true;
            }
        }
        else
        {
            //Reset ground snapping!
            if (isGrounded)
            {
                isGrounded = false;
                if (snapToGround)
                {
                    airMotion.y = 0;
                    snapToGround = false;
                }
            }
        }

        //Decrease Jump
        if (Input.GetButtonUp("Jump"))
        {
            queJump = false;
            if (didInputJump)
            {
                if (airMotion.y > 0) airMotion.y /= jumpDivide;
            }

            didInputJump = false;
        }

        CCFix();


        //Animation();


        //Accelerate (makes us be able to GO OVER maxSpeed!)
        var a = (isGrounded ? accel : accel * airControl);
        var drag = (a / maxSpeed);
        motion += ((wishMotion * a) - (motion * drag)) * Time.deltaTime;
        UpdateCC();
    }

    // [SerializeField] AudioSource audioSource => GetComponent<AudioSource>();
    // [SerializeField] AudioSource audioSourceFootsteps;

    void UpdateCC()
    {
        cc.Move((new Vector3(motion.x, airMotion.y, motion.z)) * Time.deltaTime);
    }

    public void FootstepSound()
    {
        if (isGrounded && motion.magnitude / maxSpeed > 0.2f)
        {
            //audioSourceFootsteps.pitch = Random.Range(0.9f, 1.1f);
            //audioSourceFootsteps.PlayOneShot(GameManager.instance.objects.RandomFootstepSound());
        }
    }

    public void QueJump()
    {
        queJump = true;
    }

    public void DoJump(float _amount)
    {
        //audioSource.pitch = Random.Range(.97f, 1.02f);
        //audioSource.PlayOneShot(GameManager.instance.objects.JumpingSound());
        airMotion.y = Mathf.Sqrt(2.0f * gravity * _amount);
        snapToGround = false;

        //fpsRig.CrossFade("fpsJump", 0.08f, -1, 0);
        UpdateCC();
    }

    public void Dash(float _dashHorizontal, float _dashVertical)
    {
        
        isGrounded = false;
        Vector3 wishDir = head.rotation * inputMotion;

        if (motion.magnitude < maxSpeed && inputMotion.magnitude > 0) {
            motion = head.rotation * (inputMotion * maxSpeed);
        }
        
        motion += wishDir * _dashHorizontal;

        snapToGround = false;
        airMotion.y = Mathf.Sqrt(2.0f * gravity * Mathf.Abs(_dashVertical));

        //fpsRig.CrossFade("fpsJump", 0.08f, -1, 0);
        UpdateCC();
    }

    float mX, mY;
    void Look()
    {
        inputLook = new Vector3(Input.GetAxis("Mouse X"), 0, -Input.GetAxis("Mouse Y"));

        mX += inputLook.x;
        mY += inputLook.z;
        mY = Mathf.Clamp(mY, -89, 89);

        head.localEulerAngles = new Vector3(0, mX, 0);
        cam.localEulerAngles = new Vector3(mY, 0, 0);
    }

    void CCFix()
    {
        //Fix Character Controller air-edge check!
        if (cc.isGrounded)
        {
            cc.enabled = false;
            cc.stepOffset = stepOffset;
            cc.enabled = true;
        }
        else
        {
            cc.enabled = false;
            cc.stepOffset = 0;
            cc.enabled = true;
        }
    }

    float animationMotionLerp;
    
}
