using UnityEngine;

public class SineOscillation : MonoBehaviour
{
    Vector3 startingPosition;

    [SerializeField] Vector3 movementVector;
    [SerializeField] Vector3 offsetVector;
    [SerializeField][Range(0, 1)] float movementFactor;

    [SerializeField] float period = 2f;
    [SerializeField] float rotationSpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period;                      // Continually growing over time

        const float tau = Mathf.PI * 2;                         // Constant value of 6.283 (2pi)
        float rawSinWave = Mathf.Sin(cycles * tau);             // Going from -1 to 1, multiply by amplitude

        movementFactor = (rawSinWave + 1f) / 2f;                // Recalculated to go from 0 to 1 so its cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.localPosition = startingPosition + offset + offsetVector;

        transform.Rotate(-Vector3.up * rotationSpeed);
    }
}