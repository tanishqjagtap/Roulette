using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeverSpriteSwap : MonoBehaviour
{
    public Image leverImage;

    public Sprite normalSprite;
    public Sprite pulledSprite;

    private RectTransform rectTransform;
    private Vector2 startPos;

    private bool isPulling = false;

    void Start()
    {
        rectTransform = leverImage.GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }

    public void PullLever()
    {
        if (!isPulling)
        {
            StartCoroutine(PullAnimation());
        }
    }

    IEnumerator PullAnimation()
    {
        isPulling = true;

        // Change sprite
        leverImage.sprite = pulledSprite;

        // Move down slightly
        rectTransform.anchoredPosition = startPos + new Vector2(0, -300);

        yield return new WaitForSeconds(0.15f);

        // Return back
        leverImage.sprite = normalSprite;
        rectTransform.anchoredPosition = startPos;

        isPulling = false;
    }
}