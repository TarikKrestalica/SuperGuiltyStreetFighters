using System.Collections;
using System.Collections.Generic;
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

    // Fighter performs the specified move
    public CombatMove GetCombatMove(string literal)
    {
        for (int i = 0; i < moves.Count; i++)
        {
            if (moves[i].MovePerformed(literal))
            {
                return moves[i];
            }
        }

        return null;
    }
}
