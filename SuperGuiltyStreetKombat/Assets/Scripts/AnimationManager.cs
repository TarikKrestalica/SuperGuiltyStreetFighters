using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationControl
{
    public AnimationClip clip;
    public string parameter;
}

public class AnimationManager : MonoBehaviour
{
    [SerializeField] List<AnimationControl> animationInputs;
    private System.Random rnd = new System.Random();

    public bool IsThereAnimationClip(string name)
    {
        for(int i = 0; i < animationInputs.Count; i++)
        {
            if (name == animationInputs[i].clip.name)
                return true;
        }

        return false;
    }


    public AnimationControl GetAnimation(string name)
    {
        for (int i = 0; i < animationInputs.Count; i++)
        {
            if (name == animationInputs[i].clip.name)
                return animationInputs[i];
        }

        return null;
    }

    public string ChooseRandomAnimation()
    {
        int index = rnd.Next(0, animationInputs.Count);
        return animationInputs[index].clip.name;
    }

}
