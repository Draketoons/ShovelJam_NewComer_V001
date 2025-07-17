using Unity.VisualScripting;
using UnityEngine;

public class DayTimer : MonoBehaviour
{
    [Header("Time Settings")]
    public float time;
    public float endDayLimit;
    public float timeMultiplier;
    public float percentage;

    private void Update()
    {
        time += Time.deltaTime * timeMultiplier;
        percentage = time / endDayLimit;

        if (time >= endDayLimit)
        {
            Debug.Log("End of day");
        }
    }
}
