using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float maxLeft = -250f;
    public float maxRight = 250f;

    public float moveSpeed = 250f;
    public float changeFrequency = 0.01f;

    public float targetPosition;
    public bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = Random.Range(maxLeft, maxRight);
    }

    // Update is called once per frame
    void Update()
    {
        // Move fish towards the target position
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(targetPosition, transform.localPosition.y, transform.localPosition.z), moveSpeed * Time.deltaTime);

        // Check if the fish reached the target position
        if (Mathf.Approximately(transform.localPosition.x, targetPosition))
        {
            // Choose new position
            targetPosition = Random.Range(maxLeft, maxRight);
        }

        // Change direction randomly
        if (Random.value < changeFrequency)
        {
            movingRight = !movingRight;
            targetPosition = movingRight ? maxRight : maxLeft;
        }
    }
}
