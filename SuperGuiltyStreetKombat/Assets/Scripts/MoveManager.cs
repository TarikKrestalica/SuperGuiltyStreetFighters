using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class MoveManager : MonoBehaviour
{
    public List<CombatMove> moves;

    // Was a move performed?
    public bool isThereComboMove(string input)
    {
        for(int i = 0; i < moves.Count; i++)
        {
            if (moves[i].MovePerformed(input))
            {
                return true;
            }
        }

        return false;
    }

    public bool isThereSpecialComboMove(string input)
    {
        for (int i = 0; i < moves.Count; i++)
        {
            if (moves[i].SpecialMovePerformed(input))
            {
                return true;
            }
        }

        return false;
    }

    // Fighter performs the specified move
    public CombatMove GetCombatMove(string literal)
    {
        for (int i = 0; i < moves.Count; i++)
        {
            if (moves[i].MovePerformed(literal) || moves[i].SpecialMovePerformed(literal))
            {
                return moves[i];
            }

        }

        return null;
    }

    public CombatMove GetMove(string name)
    {
        for (int i = 0; i < moves.Count; i++)
        {
            if (moves[i].name == name )
            {
                return moves[i];
            }

        }
        return null;
    }
}
