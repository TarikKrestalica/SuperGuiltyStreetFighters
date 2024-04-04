using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
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

    [Header("AnimationManager")]
    private AnimationManager animationManager;

    [Header("Time Delays")]
    [Range(0f, 3f)]
    [SerializeField] float timeDelayBetweenMove;
    [Range(0f, 3f)]
    [SerializeField] float timeDelayBetweenInput;

    float currentTimePassed = 0f;
    bool inWaitingState = false;

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
        animationManager = GetComponent<AnimationManager>();
        if (!animationManager)
        {
            Debug.LogError("Animation Manager component can't be found!");
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
        horVel = this.transform.right * Input.GetAxis("Horizontal");
        transform.Translate(horVel * speed * Time.deltaTime, Space.World);
    }

    void RunJumpingLogic()
    {
        if (Input.GetButtonDown("Jump"))
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

        // Link number to right number
        switch (number)
        {
            case 1:
                animationManager.PlayAnimation(1);
                return 1;
            case 2:
                animationManager.PlayAnimation(2);
                return 2;
            case 3:
                animationManager.PlayAnimation(3);
                return 3;
            case 4:
                animationManager.PlayAnimation(4);
                return 4;
            case 5:
                animationManager.PlayAnimation(5);
                return 5;
            case 6:
                animationManager.PlayAnimation(6);
                return 6;
            case 7:
                animationManager.PlayAnimation(7);
                return 7;
            case 8:
                animationManager.PlayAnimation(8);
                return 8;
            case 9:
                animationManager.PlayAnimation(9);
                return 9;
            default: return -1;
        }
    }

    void PerformMove()
    {
        CombatMove move = GameManager.moveManager.GetCombatMove(moveCombo);
        if (!move)
        {
            return;
        }

        Debug.Log("Move performed is: " + move.name);
        moveCombo = "";
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheckTransform.position, new Vector2(1, 0.08f), 0, playerMask);
    }
}
