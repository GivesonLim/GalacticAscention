using UnityEngine;
using System.Collections;  // This is the missing directive

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    public void TriggerShake()
    {
        StopAllCoroutines();  // Stop any ongoing shakes
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.position = originalPosition + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;  // Reset position after shaking
    }
}
