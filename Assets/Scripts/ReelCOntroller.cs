using UnityEngine;

public class ReelController : MonoBehaviour
{
    public float speed = 500f;

    public float resetThreshold = -500f;
    public float offsetY = 800f;

    void Update()
    {
        transform.localPosition += Vector3.down * speed * Time.deltaTime;

        if (transform.localPosition.y <= resetThreshold)
        {
            transform.localPosition += new Vector3(0, offsetY, 0);
        }
    }
}