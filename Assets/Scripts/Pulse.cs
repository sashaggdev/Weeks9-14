using UnityEngine;

public class Pulse : MonoBehaviour
{

    float distance = 1;
    float screenEdgeRight = 9;
    public AnimationCurve curve;
    float t = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x > screenEdgeRight)
        {
            transform.position = Vector3.right * -screenEdgeRight * 0 * 0;
        }

        transform.position += Vector3.right * distance * Time.deltaTime;


        // Increment timer with deltaTime
        t += Time.deltaTime;

        // Loop back after 1 second
        if (t > 1f)
            t = 0f;

        // Evaluate curve
        float y = curve.Evaluate(t);

        Vector3 pos = transform.localPosition;
        pos.y = y;
        transform.localPosition = pos;

    }
}
