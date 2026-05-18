using UnityEngine;
using System.Collections;

public class ReelLoop : MonoBehaviour
{
    [Header("Spin Settings")]
    public float startSpeed = 1800f;
    public float stopSpeed = 80f;

    private bool spinning = false;

    private int currentIndex = 0;

    // WIN LINE
    private float winY = -300f;

    // SYMBOL POSITIONS
    private float loopSize = 600f;

    void Start()
    {
        // PERFECT START POSITIONS
        transform.GetChild(0).localPosition =
            new Vector3(0, 0, 0);

        transform.GetChild(1).localPosition =
            new Vector3(0, -150, 0);

        transform.GetChild(2).localPosition =
            new Vector3(0, -300, 0);

        transform.GetChild(3).localPosition =
            new Vector3(0, -450, 0);
    }

    void Update()
    {
        if (!spinning) return;

        MoveSymbols(startSpeed);
    }

    void MoveSymbols(float speed)
    {
        foreach (Transform symbol in transform)
        {
            // MOVE DOWN
            symbol.localPosition +=
                Vector3.down *
                speed *
                Time.deltaTime;

            // PERFECT LOOP
            if (symbol.localPosition.y <= -600f)
            {
                symbol.localPosition +=
                    new Vector3(0, loopSize, 0);
            }
        }
    }

    public void StartSpin()
    {
        spinning = true;
    }

    public void StopSpin()
    {
        StartCoroutine(SmoothStop());
    }

    IEnumerator SmoothStop()
    {
        float currentSpeed = startSpeed;

        // FAST → SLOW
        while (currentSpeed > stopSpeed)
        {
            currentSpeed = Mathf.Lerp(
                currentSpeed,
                0f,
                Time.deltaTime * 1.8f
            );

            MoveSymbols(currentSpeed);

            yield return null;
        }

        spinning = false;

        // RANDOM SYMBOL
        currentIndex = Random.Range(0, 4);

        Transform target =
            transform.GetChild(currentIndex);

        // SNAP TARGET TO WIN LINE
        float offset =
            winY - target.localPosition.y;

        // CLEAN FINAL SNAP
        foreach (Transform symbol in transform)
        {
            symbol.localPosition +=
                new Vector3(0, offset, 0);

            // KEEP LOOP CLEAN
            if (symbol.localPosition.y > 0)
            {
                symbol.localPosition +=
                    new Vector3(0, -loopSize, 0);
            }

            if (symbol.localPosition.y <= -600)
            {
                symbol.localPosition +=
                    new Vector3(0, loopSize, 0);
            }
        }
    }

    public string GetCurrentSymbol()
    {
        Transform closest = null;

        float closestDistance = Mathf.Infinity;

        foreach (Transform symbol in transform)
        {
            float distance =
                Mathf.Abs(symbol.localPosition.y - winY);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = symbol;
            }
        }

        return closest.name;
    }
}