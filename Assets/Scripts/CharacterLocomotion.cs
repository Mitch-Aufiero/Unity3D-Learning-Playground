using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    public float jumpHeight;
    public float gravity;
    public float stepDownRate;
    public float airControl;
    public float jumpDamping;
    public float groundSpeed;
    public float rollSpeed;
    public float pushPower;

    Animator animator;
    CharacterController cc;
    Vector2 input;

    Vector3 rootMotion;
    Vector3 velocity;
    bool isJumping;
    bool isRolling;
  

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

  


    public void SetMovementAxes(float horizontal, float vertical)
    {
        input.x = horizontal;
        input.y = vertical;

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);
    }


    private void OnAnimatorMove()
    {
        rootMotion += animator.deltaPosition;

    }

    private void FixedUpdate()
    {
        if (isJumping)// in air state
        {
            UpdateInAir();
        }
       
        else
        { //IsGrounded State
            UpdateOnGround();
        }
    }


    public void Roll()
    {
        if (!cc.isGrounded)
            return;

        isRolling = true;
        Vector3 stepForwardAmout = rootMotion * rollSpeed;
        Vector3 stepDownAmount = Vector3.down * stepDownRate;

        cc.Move(stepForwardAmout + stepDownAmount);

        rootMotion = Vector3.zero;
        //animator.SetFloat("InputY", 1);
        animator.SetTrigger("Roll");
    }


    public void FinishRoll()
    {
        
        isRolling = false;
    }

    private void UpdateOnGround()
    {
        Vector3 stepForwardAmout = rootMotion * groundSpeed;
        Vector3 stepDownAmount = Vector3.down * stepDownRate;

        cc.Move(stepForwardAmout + stepDownAmount);
        rootMotion = Vector3.zero;

        if (!cc.isGrounded)// walking off edge
        {
            SetInAir(0);
        }
    }

    private void UpdateInAir()
    {
        velocity.y -= gravity * Time.fixedDeltaTime;
        Vector3 displacement = velocity * Time.fixedDeltaTime;
        displacement += CalculateAirControl();
        cc.Move(displacement);
        isJumping = !cc.isGrounded;
        rootMotion = Vector3.zero;
        animator.SetBool("isJumping", isJumping);
    }

    Vector3 CalculateAirControl()
    {
        return ((transform.forward * input.y) + (transform.right * input.x)) * (airControl / 100);
    }

    public void Jump()
    {
        if (!isJumping && !isRolling)
        {
            float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
            SetInAir(jumpVelocity);

        }
    }

    private void SetInAir(float jumpVelocity)
    {
        isJumping = true;
        velocity = animator.velocity * jumpDamping * groundSpeed;
        velocity.y = jumpVelocity;
        animator.SetBool("isJumping", true);
    }



    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
            return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }
}
