using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private AnimationManager animManager;

    [Header("HealthSystem")]
    float startingHealth = 100;
    float currentHealth;
    [SerializeField] TMP_Text health_Text;

    [Header("MoveRandomizerSystem")]
    [SerializeField] TMP_Text move_Text;
    [SerializeField] TMP_Text curTyped_Text;
    [SerializeField] CombatMove chosen;
    [SerializeField] string hashed;
    [SerializeField] string moveName;

    private void Start()
    {
        GetComponents();
        SetHealth(startingHealth);
        SetUpMoveToType();
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

        animManager = GetComponent<AnimationManager>();
        if (!animManager)
        {
            Debug.LogError("Animation Manager can't be found!");
            return false;
        }

        return true;
    }

    void SetUpMoveToType()
    {
        if (!animManager)
        {
            Debug.LogError("Animation Manager can't be found!");
            return;
        }

        move_Text.gameObject.SetActive(true);
        moveName = animManager.ChooseRandomAnimation();
        chosen = GameManager.moveManager.GetMove(moveName);
        hashed = chosen.GetMoveInString();
        move_Text.text = $"{moveName} : {hashed}";
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

        RunInputLogic();
    }

    void RunInputLogic()
    {
        curTyped_Text.text = $"Typed: {moveCombo}";
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

        if (moveCombo.Length < hashed.Length - 1)
        {
            int key = GetKeyPressed(Input.inputString);
            if (key == -1)
            {
                return;
            }
            moveCombo += key;
        }
        else
        {
            moveCombo += Input.inputString;
        }

        if (moveCombo == hashed)
        {
            inWaitingState = true;
        }

        
    }


    // Keep track of input as the player performs the combo move
    int GetKeyPressed(string input)
    {
        switch (input)
        {
            case "q":
            case "Q":
                return 1;

            case "w":
            case "W":
                return 2;

            case "e":
            case "E":
                return 3;

            case "r":
            case "R":
                return 4;

            case "t":
            case "T":
                return 5;

            case "y":
            case "Y":
                return 6;

            case "u":
            case "U":
                return 7;

            case "i":
            case "I":
                return 8;

            case "o":
            case "O":
                return 9;

            default:
                return -1;
        }
    }

    void PerformMove()
    {
        CombatMove move = GameManager.moveManager.GetCombatMove(moveCombo);
        if (!move)
        {
            return;
        }

        move_Text.gameObject.SetActive(false);
        Debug.Log("Move performed is: " + move.name);
        if (GetComponent<AnimationManager>().IsThereAnimationClip(move.name))
        {
            AnimationControl animationControl = GetComponent<AnimationManager>().GetAnimation(move.name);
            system.SetBool(animationControl.parameter, true);
            system.Play(move.name);
            system.SetBool(animationControl.parameter, false);
        }

        Hit();
        moveCombo = "";
        SetUpMoveToType();
    }

    void Hit()
    {
        Player attackedPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (!attackedPlayer)
        {
            Debug.LogError("No player component found!");
            return;
        }

        attackedPlayer.TakeDamage(10);
    }

    public void TakeDamage(float value)
    {
        currentHealth -= value;
        SetHealth(currentHealth);
    }

    public void SetHealth(float currentValue)
    {
        currentHealth = currentValue;
        health_Text.text = $"Health: {currentHealth}";
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheckTransform.position, new Vector2(2, 0.1f), 0, playerMask);
    }
}
