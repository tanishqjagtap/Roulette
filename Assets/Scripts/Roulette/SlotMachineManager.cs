using UnityEngine;
using TMPro;
using System.Collections;

public class SlotMachineManager : MonoBehaviour
{
    [Header("Reels")]
    public ReelLoop reel1;
    public ReelLoop reel2;
    public ReelLoop reel3;

    [Header("UI")]
    public TMP_Text moneyText;
    public TMP_InputField betInput;

    [Header("Money")]
    public int startingMoney = 1000;

    private int currentMoney;

    private bool spinning = false;

    void Start()
    {
        currentMoney = startingMoney;

        // DEFAULT BET
        betInput.text = "50";

        UpdateUI();
    }

    public void Spin()
    {
        if (spinning) return;

        int bet = GetBetAmount();

        // NOT ENOUGH MONEY
        if (bet > currentMoney)
        {
            Debug.Log("NOT ENOUGH MONEY");
            return;
        }

        spinning = true;

        // REMOVE BET
        currentMoney -= bet;

        UpdateUI();

        StartCoroutine(SpinRoutine());
    }

    IEnumerator SpinRoutine()
    {
        // START REELS
        reel1.StartSpin();
        reel2.StartSpin();
        reel3.StartSpin();

        // SPIN TIME
        yield return new WaitForSeconds(0.8f);

        // STOP REELS
        reel1.StopSpin();

        yield return new WaitForSeconds(0.18f);

        reel2.StopSpin();

        yield return new WaitForSeconds(0.18f);

        reel3.StopSpin();

        // WAIT FINAL STOP
        yield return new WaitForSeconds(0.35f);

        // CHECK WIN
        CheckWin();

        spinning = false;
    }

    void CheckWin()
    {
        // GET SYMBOLS ON WIN LINE
        string s1 = reel1.GetCurrentSymbol();
        string s2 = reel2.GetCurrentSymbol();
        string s3 = reel3.GetCurrentSymbol();

        int bet = GetBetAmount();

        Debug.Log(s1 + " | " + s2 + " | " + s3);

        // 3 SAME SYMBOLS = x3
        if (s1 == s2 && s2 == s3)
        {
            int win = bet * 3;

            currentMoney += win;

            Debug.Log("JACKPOT x3 +" + win);
        }

        // ANY 2 SAME SYMBOLS = x2
        else if (
            s1 == s2 ||
            s2 == s3 ||
            s1 == s3
        )
        {
            int win = bet * 2;

            currentMoney += win;

            Debug.Log("WIN x2 +" + win);
        }

        // ALL DIFFERENT = LOSE
        else
        {
            Debug.Log("LOSE");
        }

        UpdateUI();
    }

    int GetBetAmount()
    {
        int bet;

        bool valid =
            int.TryParse(betInput.text, out bet);

        // INVALID INPUT
        if (!valid)
        {
            bet = 50;
        }

        // MIN BET
        if (bet < 1)
        {
            bet = 1;
        }

        return bet;
    }

    void UpdateUI()
    {
        moneyText.text =
            "Money:\n$" + currentMoney;
    }
}