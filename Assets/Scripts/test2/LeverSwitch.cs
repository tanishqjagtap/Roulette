using System.Collections;
using UnityEngine;

public class LeverKnobAnimation : MonoBehaviour
{
    private RectTransform rectTransform;

    private Vector2 startPosition;

    private bool isAnimating = false;

    public float pullDistance = 40f;

    public float animationTime = 0.15f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        startPosition = rectTransform.anchoredPosition;
    }

    public void PullLever()
    {
        if (!isAnimating)
        {
            StartCoroutine(AnimateLever());
        }
    }

    IEnumerator AnimateLever()
    {
        isAnimating = true;

        // Move knob down
        rectTransform.anchoredPosition =
            startPosition + new Vector2(0, -pullDistance);

        yield return new WaitForSeconds(animationTime);

        // Return knob back up
        rectTransform.anchoredPosition = startPosition;

        isAnimating = false;
    }
}