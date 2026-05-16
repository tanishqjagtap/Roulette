using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReelController : MonoBehaviour
{
    [Header("Symbols")]
    public Sprite[] symbolSprites;

    [Header("Settings")]
    public float cellHeight = 80f;
    public float spinSpeed = 1200f;

    private List<RectTransform> symbols = new List<RectTransform>();
    private List<Image> images = new List<Image>();

    private bool spinning = false;

    private float stripHeight;

    public int CurrentResult { get; private set; }

    void Awake()
    {
        BuildReel();
    }

    void Update()
    {
        if (spinning)
        {
            MoveSymbols();
        }
    }

    void BuildReel()
    {
        symbols.Clear();
        images.Clear();

        int pool = symbolSprites.Length * 5;

        stripHeight = pool * cellHeight;

        for (int i = 0; i < pool; i++)
        {
            GameObject obj = new GameObject("Symbol_" + i);

            obj.transform.SetParent(transform, false);

            Image img = obj.AddComponent<Image>();

            img.sprite = symbolSprites[i % symbolSprites.Length];

            RectTransform rt = obj.GetComponent<RectTransform>();

            rt.sizeDelta = new Vector2(80, 80);

            rt.anchorMin = new Vector2(0.5f, 1);
            rt.anchorMax = new Vector2(0.5f, 1);

            rt.pivot = new Vector2(0.5f, 1);

            rt.anchoredPosition = new Vector2(0, -i * cellHeight);

            symbols.Add(rt);
            images.Add(img);
        }
    }

    void MoveSymbols()
    {
        foreach (RectTransform rt in symbols)
        {
            Vector2 pos = rt.anchoredPosition;

            pos.y -= spinSpeed * Time.deltaTime;

            if (pos.y < -stripHeight)
            {
                pos.y += stripHeight;
            }

            rt.anchoredPosition = pos;
        }
    }

    public void StartSpin()
    {
        spinning = true;
    }

    public void StopSpin()
    {
        spinning = false;

        CurrentResult = Random.Range(0, symbolSprites.Length);
    }
}