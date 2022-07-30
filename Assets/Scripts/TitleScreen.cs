using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleScreen : MonoBehaviour
{
    [Header("Title Screen Objects")]
    public List<GameObject> karateJoes = new List<GameObject>();

    [Header("UI Text")]
    public TextMeshProUGUI flowTextBox;
    public TextMeshProUGUI offsetTextBox;

    [Header("Level Scenes")]
    public string levelOneScene;
    public string levelTwoScene;
    public string levelThreeScene;

    // For animating the Karate Joes on the title screen
    private float titleScreenBPM = 125;
    private float timeBetweenBeat = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Updating the text boxes
        UpdateFlowText();
        UpdateInputOffsetText();
    }

    // FixedUpdate used for BPM calculations
    void FixedUpdate()
    {
        timeBetweenBeat += Time.deltaTime;

        // If one beat has passed, all karate joes bop
        if (timeBetweenBeat >= 60 / titleScreenBPM)
        {
            timeBetweenBeat -= 60 / titleScreenBPM;
            BopKarateJoes(timeBetweenBeat);
        }
    }

    private void Update()
    {
        // Changing the input offset with the arrow keys
        if (Input.GetKeyDown(KeyCode.LeftArrow)) DecrementInputOffset();
        if (Input.GetKeyDown(KeyCode.RightArrow)) IncrementInputOffset();
    }

    // Button functions
    public void ToLevel1()
    {
        SceneManager.LoadScene(levelOneScene);
    }

    public void ToLevel2()
    {
        SceneManager.LoadScene(levelTwoScene);
    }

    public void ToLevel3()
    {
        SceneManager.LoadScene(levelThreeScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetSave()
    {
        GlobalUtils.SetFlow(50);
        GlobalUtils.SetInputOffset(0);
        UpdateFlowText();
        UpdateInputOffsetText();
    }

    // Input offset changing functions
    void IncrementInputOffset()
    {
        int currentInputOffset = GlobalUtils.GetInputOffset();
        GlobalUtils.SetInputOffset(currentInputOffset + 1);
        
        UpdateInputOffsetText();
    }

    void DecrementInputOffset()
    {
        int currentInputOffset = GlobalUtils.GetInputOffset();
        GlobalUtils.SetInputOffset(currentInputOffset - 1);
        
        UpdateInputOffsetText();
    }

    // Text updating functions
    void UpdateInputOffsetText()
    {
        int inputOffset = GlobalUtils.GetInputOffset();
        offsetTextBox.text = $"Note Offset: {inputOffset}ms (Arrow Keys)";
    }

    void UpdateFlowText()
    {
        int flow = GlobalUtils.GetFlow();
        flowTextBox.text = $"Flow: {flow}";
    }

    // Function to have every Karate Joe bop
    void BopKarateJoes(float offset)
    {
        foreach (GameObject joe in karateJoes)
        {
            Animator joeAnimator = joe.GetComponent<Animator>();
            if (joeAnimator == null) continue;

            joeAnimator.SetFloat("BopOffset", offset);
            joeAnimator.SetTrigger("IdleBop");
        }
    }
}
