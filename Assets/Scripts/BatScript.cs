using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;
    [SerializeField]
    private float amplitude = 1.0f;
    [SerializeField]
    private float frequency = 1.0f;

    private float startingY;

    // Start is called before the first frame update
    void Start()
    {
        startingY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float newX = transform.position.x - speed * Time.deltaTime;

        // Oscillate up and down using a sine wave
        float newY = startingY + Mathf.Sin(Time.time * frequency) * amplitude;

        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}

