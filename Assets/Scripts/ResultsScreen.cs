using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ResultsScreen : MonoBehaviour
{
    public static int notesHit;
    public static int totalNotes;
    public static string previousLevelScene;
    public static string titleScreenScene = "TitleScreen";
    public static string resultsSceneName = "ResultsScreen";

    [Header("Player Messages")]
    public string tryAgainMessage = "I must train harder.";
    public string okMessage = "I must focus to become the best.";
    public string superbMessage = "This is it, Karate Perfection!";

    [Header("Results Graphics")]
    public Sprite tryAgainSprite;
    public Sprite okSprite;
    public Sprite superbSprite;

    [Header("Screen Text")]
    public TextMeshProUGUI hitText;
    public TextMeshProUGUI missedText;
    public TextMeshProUGUI percentageHitText;
    public TextMeshProUGUI resultsText;
    public Image resultsImage;
    public TextMeshProUGUI resultsCaptionText;

    [Header("Results Music")]
    public AudioSource audioSource;
    public AudioClip tryAgainAudio;
    public AudioClip okAudio;
    public AudioClip superbAudio;

    private int notesMissed;
    private float percentageOfNotesHit;
    // Start is called before the first frame update
    void Start()
    {
        notesMissed = totalNotes - notesHit;
        percentageOfNotesHit = (notesHit / totalNotes) * 100;
        UpdateResultsScreen();
        UpdateFlow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateResultsScreen()
    {
        hitText.text = $"Hit: {notesHit}";
        missedText.text = $"Missed: {notesMissed}";
        percentageHitText.text = $"Percentage Hit: {percentageOfNotesHit}%";

        string result = PercentageToResult(percentageOfNotesHit);
        resultsText.text = $"Result: {result}";

        // Could be made more efficient with the use of enums maybe but this works for now
        if (result == "Try Again!")
        {
            resultsImage.sprite = tryAgainSprite;
            resultsCaptionText.text = $"\"{tryAgainMessage}\"";
            audioSource.clip = tryAgainAudio;
        }
        else if (result == "Ok.")
        {
            resultsImage.sprite = okSprite;
            resultsCaptionText.text = $"\"{okMessage}\"";
            audioSource.clip = okAudio;
        }
        else
        {
            resultsImage.sprite = superbSprite;
            resultsCaptionText.text = $"\"{superbMessage}\"";
            audioSource.clip = superbAudio;
        }

        audioSource.Play();
    }

    void UpdateFlow()
    {
        int flow = GlobalUtils.GetFlow();
        flow += Mathf.RoundToInt(notesHit - (totalNotes * 0.8f));
        GlobalUtils.SetFlow(flow);
    }

    string PercentageToResult(float percentage)
    {
        if (percentage < 60) return "Try Again!";
        else if (percentage < 80) return "Ok.";
        else return "Superb!";
    }

    public static void ToResults(int _notesHit, int _totalNotes, string _previousLevelScene)
    {
        notesHit = _notesHit;
        totalNotes = _totalNotes;
        previousLevelScene = _previousLevelScene;

        SceneManager.LoadScene(resultsSceneName);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(previousLevelScene);
    }

    public static void ToTitleScreen()
    {
        SceneManager.LoadScene(titleScreenScene);
    }
}
