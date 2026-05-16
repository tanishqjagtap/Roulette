using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LeverAnimation : MonoBehaviour
{
    public Image leverImage;

    public Sprite normalSprite;
    public Sprite pulledSprite;

    private bool isAnimating = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayLeverAnimation();
        }
    }

    public void PlayLeverAnimation()
    {
        if (!isAnimating)
        {
            StartCoroutine(AnimateLever());
        }
    }

    IEnumerator AnimateLever()
    {
        isAnimating = true;

        // Pull lever
        leverImage.sprite = pulledSprite;

        yield return new WaitForSeconds(0.15f);

        // Return lever
        leverImage.sprite = normalSprite;

        isAnimating = false;
    }
}