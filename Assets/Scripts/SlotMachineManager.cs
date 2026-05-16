using System.Collections;
using UnityEngine;

public class SlotMachineManager : MonoBehaviour
{
    [Header("Reels")]
    public ReelController reel1;
    public ReelController reel2;
    public ReelController reel3;

    [Header("Lever")]
    public Transform lever;

    private bool spinning = false;

    public void Spin()
    {
        if (spinning)
            return;

        StartCoroutine(SpinRoutine());
    }

    IEnumerator SpinRoutine()
    {
        spinning = true;

        // Pull lever down
        lever.rotation = Quaternion.Euler(0, 0, -25);

        yield return new WaitForSeconds(0.15f);

        // Return lever
        lever.rotation = Quaternion.Euler(0, 0, 0);

        // Start reels
        reel1.StartSpin();
        reel2.StartSpin();
        reel3.StartSpin();

        // Spin duration
        yield return new WaitForSeconds(2f);

        // Stop reels one by one
        reel1.StopSpin();

        yield return new WaitForSeconds(0.4f);

        reel2.StopSpin();

        yield return new WaitForSeconds(0.4f);

        reel3.StopSpin();

        spinning = false;
    }
}