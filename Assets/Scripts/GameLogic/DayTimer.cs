using System.Collections;
using UnityEngine;

public class DayTimer : MonoBehaviour
{
    [Header("Time Settings")]
    public float time;
    public float percentage;
    public float endDayLimit;
    public float timeMultiplier;
    public int currentDisplayedTime;
    public string aMPM;
    public bool paused;
    private UIManager uIManager;
    float interval;
    int currentHour;

    private void Start()
    {
        uIManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();
        endDayLimit *= 60.0f;
        interval = endDayLimit / 24;
        StartCoroutine(CheckTime());
    }

    private void Update()
    {
        if (!paused)
        {
            if (time < endDayLimit)
            {
                time += Time.deltaTime * timeMultiplier;
                percentage = time / endDayLimit;
            }
            if (time >= endDayLimit)
            {
                Debug.Log("End of day");
            }
        }
    }

    IEnumerator CheckTime()
    {
        int currentTime = 0;
        Debug.Log($"Time interval: {interval}");
        while (percentage < 1.0f && !paused)
        {
            Debug.Log("Checking time...");
            Debug.Log($"Current Time: {currentTime}");
            currentHour++;
            if (percentage < 0.5f)
            {
                aMPM = "AM";
                currentTime = currentHour;
            }
            if (percentage > 0.5f)
            {
                aMPM = "PM";
                currentTime = currentHour - 12;
            }
            uIManager.SetTimeText(currentTime, aMPM);
            yield return new WaitForSeconds(interval);
        }
    }
}
