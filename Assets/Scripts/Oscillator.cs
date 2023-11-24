using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0, 1)] float movementFactor;
    [SerializeField] float period = 2f;

    void Start()
    {
      startingPos = transform.position;
    }

    void Update()
    {
      // Prevent a NaN error
      period = Mathf.Clamp(period, 0.0001f, period);
      // An alternative way to prevent NaN error
      // if (period == Mathf.Epsilon) { return; }

      float cycles = Time.time / period;
      const float tau = Mathf.PI * 2; // x2Represents a full circle rotation with radians
      float rawSinWave = Mathf.Sin(cycles * tau);
      // Make sure value of rawSinWave is between 0 and 1

      movementFactor = (rawSinWave + 1f) / 2;

      Vector3 offset = movementVector * movementFactor;
      transform.position = startingPos + offset;
    }
}