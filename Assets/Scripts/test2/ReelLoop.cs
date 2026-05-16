using UnityEngine;

public class ReelLoop : MonoBehaviour
{
    public float speed = 300f;

    public float spacing = 600f;
    public float bottomPosition = -300f;

    void Update()
    {
        transform.localPosition += Vector3.down * speed * Time.deltaTime;

        if (transform.localPosition.y <= bottomPosition)
        {
            transform.localPosition += new Vector3(0, spacing, 0);
        }
    }
}