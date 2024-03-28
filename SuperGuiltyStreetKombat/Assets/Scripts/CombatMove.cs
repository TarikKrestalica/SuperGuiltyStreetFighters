using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Help with creating the asset menu: https://www.youtube.com/watch?v=aPXvoWVabPY

[CreateAssetMenu(fileName = "CombatMove", menuName = "Move")]
public class CombatMove : ScriptableObject
{
    [Range(1, 9f)]
    [SerializeField] List<int> keys;

    public bool MovePerformed()
    {
        return true;
    }


}
