using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObjectScript : MonoBehaviour
{
    [SerializeField]
    private int obstacleFlySpeed;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.x -= obstacleFlySpeed * Time.deltaTime;

        transform.position = newPosition;

    }
}
