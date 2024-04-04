using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationInput
{
    [Range(1, 9)]
    public int key;
    public Animation animation;
}
public class AnimationManager : MonoBehaviour
{
    [SerializeField] List<AnimationInput> animationInputs;

    public void PlayAnimation(int index)
    {
        
    }
}
