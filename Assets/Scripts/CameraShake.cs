using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.2f;

    private Vector3 initialPosition;
    private float currentShakeTime = 0f;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (currentShakeTime > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            currentShakeTime -= Time.deltaTime;
        }
        else
        {
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake()
    {
        currentShakeTime = shakeDuration;
    }
}
