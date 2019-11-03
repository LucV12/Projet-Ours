using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public float Strenght;
    public float Duration;

    private Vector3 initialCameraPosition;
    private float remainingShakeTime;


    public void Shake()
    {
        remainingShakeTime = Duration;
        enabled = true;
    }

    void Awake()
    {
        initialCameraPosition = transform.localPosition;
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingShakeTime <= 0)
        {
            transform.localPosition = initialCameraPosition;
            enabled = false;
        }

        transform.Translate(Random.insideUnitCircle * Strenght);
        remainingShakeTime -= Time.deltaTime;
    }
}
