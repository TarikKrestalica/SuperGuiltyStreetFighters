using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] string moveCombo;

    // Reading input from keyboard: https://docs.unity3d.com/ScriptReference/Input.html
    void Update()
    {
        if (!Input.anyKeyDown)
            return;

        if(GetKeyPressed(Input.inputString) == -1)
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
}
