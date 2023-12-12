using System.Collections;
using UnityEngine;

public class MultiTest : MonoBehaviour
{
    public Transform spinner;
    public int numberOfSegments = 8;
    public float spinSpeed = 200f;
    public bool isSpinning = false;
    public int predefinedResult = 0;
    public float lineLength = 443f; 

    private void Start()
    {
        predefinedResult = Random.Range(0, numberOfSegments);
        Debug.Log("KQ: " + predefinedResult);
        StartSpin();
    }

    private void Update()
    {
        if (isSpinning)
        {
            //spinner.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
            Debug.Log("move");
        }
    }

    public void StartSpin()
    {
        if (!isSpinning)
        {
            StartCoroutine(SpinForDuration(5f));
        }
    }

    IEnumerator SpinForDuration(float duration)
    {
        isSpinning = true;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isSpinning = false;
        CalculateResult();
    }

    void CalculateResult()
    {
        float segmentSize = 360f / numberOfSegments;
        float targetAngle = predefinedResult * segmentSize;

        float currentRotation = spinner.eulerAngles.z;
        float difference = 360f - (currentRotation % 360f) + targetAngle;

        StartCoroutine(SpinToResult(difference, 3f));
    }

    IEnumerator SpinToResult(float targetAngle, float duration)
    {
        float startTime = Time.time;
        float startRotation = spinner.eulerAngles.z;

        while (Time.time - startTime < duration)
        {
            float angle = Mathf.Lerp(0, targetAngle, (Time.time - startTime) / duration);
            spinner.RotateAround(spinner.position, Vector3.forward, angle);
            yield return null;
        }

        Debug.Log("Kết quả: " + predefinedResult);

        float stopAngle = predefinedResult * (360f / numberOfSegments);
        Vector3 stopPosition = spinner.position + (Quaternion.Euler(0, 0, stopAngle) * (Vector3.right * (lineLength / 2f)));

        spinner.position = stopPosition;
    }
}
