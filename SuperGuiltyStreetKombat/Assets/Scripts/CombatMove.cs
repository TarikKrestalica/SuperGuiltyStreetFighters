using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

// Help with creating the asset menu: https://www.youtube.com/watch?v=aPXvoWVabPY

// Each move will have an associated animation in sequential order.
[CreateAssetMenu(fileName = "CombatMove", menuName = "Move")]
public class CombatMove : ScriptableObject
{
    [Range(1, 9f)]
    public List<int> keys;

    [SerializeField] char character;

    public bool MovePerformed(string input)
    {
        string combo = "";
        for (int i = 0; i < keys.Count; i++)
        {
            combo += keys[i];
        }

        if (input == combo)  // Check for match, perform some animation
            return true;

        return false;
    }

    public bool SpecialMovePerformed(string input)
    {
        string combo = "";
        for (int i = 0; i < keys.Count; i++)
        {
            combo += keys[i];
        }

        combo += character;
        if (input == combo)  // Check for match, perform some animation
            return true;

        return false;
    }

    public string GetMoveInString()
    {
        string combo = "";
        for (int i = 0; i < keys.Count; i++)
        {
            combo += keys[i];
        }

        return combo;
    }
}
