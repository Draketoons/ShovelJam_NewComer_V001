using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private UIManager uIManager;
    [SerializeField] private GameManager gM;
    [SerializeField] private int currentTime;
    [SerializeField] private int currentHour;
    float interval;

    private void Start()
    {
        uIManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();
        gM = GetComponent<GameManager>();
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
                ResetDay();
            }
        }
    }

    public void FindUIManager()
    {
        uIManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();
    }

    public void CheckCurrentTime()
    {
        uIManager.SetTimeText(currentTime, aMPM);
    }

    public void ResetDay()
    {
        gM.startingNewDay = true;
        time = 0.0f;
        currentHour = 0;
        gM.loopCount++;
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator CheckTime()
    {
        currentTime = 0;
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
