using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UIElements;

public class Player2 : MonoBehaviour
{
    [SerializeField] private string moveCombo;
    string tempCombo;
    [Header("Movement System")]
    Vector3 horVel;
    [Range(1, 100f)]
    [SerializeField] float speed;

    [Header("Jumping System")]
    Rigidbody2D rb;
    [Range(1, 100f)]
    [SerializeField] float jumpPower;

    [SerializeField] private Transform groundCheckTransform;
    public LayerMask playerMask;

    [Header("Time Delays")]
    [Range(0f, 3f)]
    [SerializeField] float timeDelayBetweenMove;
    [Range(0f, 3f)]
    [SerializeField] float timeDelayBetweenInput;

    float currentTimePassed = 0f;
    bool inWaitingState = false;

    [Header("Animation Systems")]
    private Animator system;

    private void Start()
    {
        GetComponents();
    }

    bool GetComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!rb)
        {
            Debug.LogError("Rigidbody 2D component can't be found!");
            return false;
        }
        system = GetComponent<Animator>();
        if (!system)
        {
            Debug.LogError("Animator can't be found!");
            return false;
        }

        return true;
    }

    // Reading input from keyboard: https://docs.unity3d.com/ScriptReference/Input.html
    void Update()
    {
        if (!GetComponents())
        {
            return;
        }

        if (inWaitingState)
        {
            if(currentTimePassed < timeDelayBetweenMove)
            {
                if (currentTimePassed != 0f)
                {
                    moveCombo += Input.inputString;
                }

                currentTimePassed += Time.deltaTime;
                return;
            }

            currentTimePassed = 0f;
            PerformMove();
            inWaitingState = false;
        }

        RunMovementLogic();
        RunJumpingLogic();
        RunInputLogic();
    }

    void RunMovementLogic()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            horVel = -this.transform.right * Input.GetAxis("Horizontal");
            transform.Translate(horVel * speed * Time.deltaTime, Space.World);
        }
    }

    void RunJumpingLogic()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(!IsGrounded())
            {
                return;
            }

            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    void RunInputLogic()
    {
        if (moveCombo.Length > 0)  // Check for time pass in between and constraint with sequence of keys
        {
            if (Input.anyKeyDown)
            {
                currentTimePassed = 0f;
            }
            else
            {
                if (currentTimePassed < timeDelayBetweenInput && moveCombo.Length <= 6)
                {
                    currentTimePassed += Time.deltaTime;
                }
                else if (currentTimePassed >= timeDelayBetweenInput || moveCombo.Length > 6)
                {
                    currentTimePassed = 0f;
                    moveCombo = "";
                }
            }
        }

        if (!Input.anyKeyDown)
            return;

        if (GetKeyPressed(Input.inputString) == -1)
        {
            return;
        }

        moveCombo += Input.inputString;
        if (!GameManager.moveManager.isThereComboMove(moveCombo))
        {
            return;
        }

        inWaitingState = true;
    }


    // Keep track of input as the player performs the combo move
    int GetKeyPressed(string input)
    {
        if(!int.TryParse(input, out int number))
        {
            return -1;
        }

        return number;
    }

    void PerformMove()
    {
        CombatMove move = GameManager.moveManager.GetCombatMove(moveCombo);
        if (!move)
        {
            return;
        }

        Debug.Log("Move performed is: " + move.name);
        if (GetComponent<AnimationManager>().IsThereAnimationClip(move.name))
        {
            AnimationControl animationControl = GetComponent<AnimationManager>().GetAnimation(move.name);
            system.SetBool(animationControl.parameter, true);
            system.Play(move.name);
            system.SetBool(animationControl.parameter, false);
        }
    
        moveCombo = "";
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheckTransform.position, new Vector2(2, 0.1f), 0, playerMask);
    }
}
