using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] string moveCombo;

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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!rb)
        {
            Debug.LogError("Rigidbody 2D component can't be found!");
            return;
        }
    }

    // Reading input from keyboard: https://docs.unity3d.com/ScriptReference/Input.html
    void Update()
    {
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

        PerformMove(moveCombo);
        moveCombo = "";
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
            case 1: return 1;
            case 2: return 2;
            case 3: return 3;
            case 4: return 4;
            case 5: return 5;
            case 6: return 6;
            case 7: return 7;
            case 8: return 8;
            case 9: return 9;
            case 0: return 0;
            default: return -1;
        }
    }

    void PerformMove(string literal)
    {
        CombatMove move = GameManager.moveManager.GetCombatMove(literal);
        Debug.Log("Move performed is: " + move);
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheckTransform.position, new Vector2(1, 0.08f), 0, playerMask);
    }
}
