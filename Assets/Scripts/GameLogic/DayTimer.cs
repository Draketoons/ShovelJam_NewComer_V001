using System.Collections;
using Unity.Mathematics;
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
    public bool unPaused;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private AudioManager aM;
    [SerializeField] private GameManager gM;
    [SerializeField] private int currentTime;
    [SerializeField] private int currentHour;
    float interval;

    private void Start()
    {
        uIManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();
        aM = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
        gM = GetComponent<GameManager>();
        endDayLimit *= 60.0f;
        interval = endDayLimit / 17;
        currentHour = 6;
    }

    private void Update()
    {
        if (!paused)
        {
            if (!unPaused)
            {
                Debug.Log("DayTimer: starting coroutine...");
                StartCoroutine(CheckTime());
                unPaused = true;
            }
            if (time < endDayLimit)
            {
                time += Time.deltaTime * timeMultiplier;
                percentage = time / endDayLimit;
            }
            if (time >= endDayLimit)
            {
                Debug.Log("End of day");
                ResetDay();
                paused = true;
            }
            uIManager.GetBlackScreen().color = new Color(uIManager.GetBlackScreen().color.r, uIManager.GetBlackScreen().color.g, uIManager.GetBlackScreen().color.b, Mathf.Lerp(0.0f, 100.0f, percentage));
        }
        if (aM.GetAudioSource().clip == aM.loopSound && !aM.GetAudioSource().isPlaying)
        {
            SceneManager.LoadScene(0);
        }
        if (paused)
        {
            unPaused = false;
        }
    }

    public void FindUIManager()
    {
        uIManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();
    }

    public void FindAudioManager()
    {
        aM = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
    }

    public void CheckCurrentTime()
    {
        uIManager.SetTimeText(currentTime, aMPM);
    }

    public void ResetDay()
    {
        aM.PlayLoopSound();
        gM.startingNewDay = true;
        time = 0.0f;
        percentage = 0.0f;
        currentHour = 6;
        gM.loopCount++;
        uIManager.FadeOut();
    }

    IEnumerator CheckTime()
    {
        currentTime = 6;
        Debug.Log($"Time interval: {interval}");
        while (percentage < 1.0f && !paused)
        {
            Debug.Log("Checking time...");
            Debug.Log($"Current Time: {currentTime}");
            currentHour++;
            if (currentHour <= 12)
            {
                aMPM = "AM";
                currentTime = currentHour;
            }
            if (currentHour > 12)
            {
                aMPM = "PM";
                currentTime = currentHour - 12;
            }
            uIManager.SetTimeText(currentTime, aMPM);
            yield return new WaitForSeconds(interval);
        }
    }
}
