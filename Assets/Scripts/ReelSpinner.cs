using UnityEngine;

public class ReelSpinner : MonoBehaviour
{
    public float speed = 500f;

    private bool isSpinning = false;

    private float startY = 300f;
    private float resetY = -1100f;

    void Update()
    {
        if (isSpinning)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);

            if (transform.localPosition.y <= resetY)
            {
                transform.localPosition = new Vector3(
                    transform.localPosition.x,
                    startY,
                    transform.localPosition.z
                );
            }
        }
    }

    public void StartSpin()
    {
        isSpinning = true;
    }

    public void StopSpin()
    {
        isSpinning = false;
    }
}