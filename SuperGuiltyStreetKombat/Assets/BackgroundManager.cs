using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] List<Texture> backgrounds;
    private System.Random rnd = new System.Random();

    [SerializeField] private RawImage m_image;

    private void Start()
    {
        ChooseBackground();
    }

    public bool GetImageComponent()
    {
        if (m_image == null)
        {
            Debug.LogError("No image component");
            return false;
        }

        return true;
    }

    public void ChooseBackground()
    {
        if (!GetImageComponent())
        {
            return;
        }

        int index = rnd.Next(0, backgrounds.Count);
        Texture chosenTexture = backgrounds[index];
        m_image.texture = chosenTexture;
    }
}
