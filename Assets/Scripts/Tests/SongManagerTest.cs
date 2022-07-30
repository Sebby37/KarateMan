using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Levels
{
    Level1,
    Level2,
    Level3
}

public class SongManagerTest : MonoBehaviour
{
    [Header("Debugging + Testing")]
    public AudioClip beepSFX;

    [Header("Song Info")]
    public Levels level;
    public static float BPM;// = 119;
    public float throwOffset;// = -0.13f;
    public List<TestNote> notes = new List<TestNote>();
    public List<BPMChange> bpmChanges = new List<BPMChange>();
    public List<SongEvent> songEvents = new List<SongEvent>();
    public AudioSource songAudio;
    public AudioSource punchSFX;

    [Header("Text + Positioning")]
    public Vector3 textPosition;
    public Sprite grrSprite;
    public Sprite hitSprite;
    public Sprite threeSprite;
    public float hitNumberingOffset = 3;

    [Header("Objects")]
    public GameObject potPrefab;
    public Sprite potSprite;
    public Sprite rockSprite;
    public Sprite ballSprite;
    public Sprite cookingPotSprite;
    public Sprite lightBulbSprite;

    public AudioClip potHitSfx;
    public AudioClip rockHitSfx;
    public AudioClip ballHitSfx;
    public AudioClip cookingPotHitSfx;
    public AudioClip lightBulbHitSFX;
    public AudioClip lightBulbThrowSFX;

    public static bool songStarted = false;

    private int totalNotes;
    private int notesHit = 0;
    private float previousHit = 0;
    private float secondsPerBeat;
    private float timeBetweenBeat = -1;
    private List<GameObject> thrownObjects = new List<GameObject>();

    // Start is called before the first frame update
    // Using awake as to load the song before any other gameobjects are loaded
    void Awake()//Start()
    {
        songStarted = false;
        Song song;// = Songs.Song2();
        switch (level)
        {
            case Levels.Level1:
                song = Songs.Song1();
                break;
            case Levels.Level2:
                song = Songs.Song2();
                break;
            case Levels.Level3:
                song = Songs.Song3();
                break;
            default:
                song = Songs.Song2();
                break;
        }

        BPM = song.BPM;
        notes = song.Notes;
        bpmChanges = song.BpmChanges;
        songEvents = song.SongEvents;

        secondsPerBeat = 60 / BPM;

        throwOffset = song.SongOffset + GlobalUtils.GetInputOffsetInSeconds();
        totalNotes = notes.Count;
        // HIT x NOTE: The audio and text shows up 1.5 beats before the objects are thrown. The text can disappear 1.5 + x beats after the sfx is played
    }

    private void Start()
    {
        timeBetweenBeat = -1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Incrementing the time between beats + logic for that
        timeBetweenBeat += (timeBetweenBeat > -1 ? Time.deltaTime : 0) / secondsPerBeat;
        Debug.Log($"Current beat: {Mathf.Round(timeBetweenBeat)}\nBPM: {BPM} | Seconds per beat: {secondsPerBeat}");

        // Throw pots with the beat of the song
        if (notes.Count > 0)
        {
            TestNote latestNote = notes[0];
            if (timeBetweenBeat >= latestNote.BeatNumber - 2)
            {
                float offset = (timeBetweenBeat) - (latestNote.BeatNumber - 2); // Calculating hit offset
                Debug.Log($"Pot thrown with offset of {offset}\nPot was thrown on beat {(timeBetweenBeat)}, should have been thrown on beat {(latestNote.BeatNumber - 2)}\nBPM: {BPM}, Notes Left: {notes.Count - 1}");

                ThrowPot(offset, ThrowableToSprite(latestNote.Type), ThrowableToHitSFX(latestNote.Type), offBeat: !(Mathf.Approximately(latestNote.BeatNumber, Mathf.RoundToInt(latestNote.BeatNumber))/*latestNote.BeatNumber % 2 == 0*/));
                notes.RemoveAt(0);

                // Setting the hard punch to be whether the thrown object was a rock or cooking pot
                KarateJoeAnims.hardPunchNext = (latestNote.Type == ThrowablesTest.Rock || latestNote.Type == ThrowablesTest.CookingPot || latestNote.Type == ThrowablesTest.Ball);
            }
        }
        else
        {
            // End of song, go to results screen
            songStarted = false;
            StartCoroutine(WaitToLoadTitleScreen(secondsPerBeat * 4));
        }

        if (bpmChanges.Count > 0)
        {
            BPMChange nextBPM = bpmChanges[0];

            if (timeBetweenBeat >= nextBPM.BeatNumber)
            {
                ChangeBPM(nextBPM.NewBPM);
                bpmChanges.RemoveAt(0);
            }
        }

        if (songEvents.Count > 0)
        {
            SongEvent songEvent = songEvents[0];

            if (timeBetweenBeat >= songEvent.BeatNumber)
            {
                HandleSongEvent(songEvent.Type);
                songEvents.RemoveAt(0);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !songStarted)
        {
            timeBetweenBeat = throwOffset;
            songAudio.Play();
            songStarted = true;
        }

        if (Input.GetKeyDown(KeyCode.Z)) punchSFX.Play();

        if (thrownObjects.Count > 0)
        {
            GameObject currentThrownObject = thrownObjects[0];
            PotTest currentPot = currentThrownObject.GetComponent<PotTest>();

            if (currentPot.CanBeHit())
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    previousHit = currentPot.Hit();
                    thrownObjects.Remove(currentThrownObject);
                    notesHit++;
                }
            }
            else if (currentPot.Missed())
            {
                thrownObjects.RemoveAt(0);
            }
        }

        // Escape key returns to title screen
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            songStarted = false;
            ResultsScreen.ToTitleScreen();
        }
    }

    private void OnGUI()
    {
        // Displaying the hits and misses
        //GUI.TextField(new Rect(0, 0, 500, 20), $"Prev Hit: {previousHit * 1000} ms");
    }

    /*
    TODO:
    - Add in other objects to hit (light bulb)
    - Rewrite this code once it works to be more documented and efficient
    - Add in background and word events (Hit x)
    - Create a file that stores the song information (If I don't need to have 3 different scenes for levels)
    - Chart all songs or only the ones I need (karate man gba, karate man ds, karate man wii) + extras (karate man megamix prequel, karate man senior)
    - Create a title screen and level selection menu
    - Add the flow system, a gauge/number that tracks how well you hit the notes. It goes up when you hit a note and down when you miss. How much it goes up by will depend on how accurately the note was hit
    */

    void Beep()
    {
        AudioSource.PlayClipAtPoint(beepSFX, Vector3.zero);
    }

    void ThrowPot(float offset, Sprite sprite, AudioClip hitSfx, bool offBeat = false)
    {
        GameObject pot = Instantiate(potPrefab);
        Animator potAnimator = pot.GetComponent<Animator>();
        
        potAnimator.SetBool("OnBeat", !offBeat);
        potAnimator.SetFloat("Speed", BPM / 60);
        potAnimator.SetFloat("Offset", offset);
        potAnimator.SetTrigger(offBeat ? "ThrowOffBeat" : "ThrowOnBeat");

        SpriteRenderer renderer = potAnimator.GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;

        PotTest potTest = pot.GetComponent<PotTest>();
        potTest.hitSFX.clip = hitSfx;

        if (sprite == lightBulbSprite)
        {
            potTest.throwOnBeatSFX.clip = lightBulbThrowSFX;
            potTest.throwOffBeatSFX.clip = lightBulbThrowSFX;
        }

        thrownObjects.Add(pot);
    }

    void ChangeBPM(float newBpm)
    {
        BPM = newBpm;
        secondsPerBeat = 60 / BPM;

        GameObject.FindGameObjectWithTag("Player").GetComponent<KarateJoeAnims>().ChangeBPM();
    }

    Sprite ThrowableToSprite(ThrowablesTest throwable)
    {
        switch (throwable)
        {
            case ThrowablesTest.Rock:
                return rockSprite;
            case ThrowablesTest.Ball:
                return ballSprite;
            case ThrowablesTest.CookingPot:
                return cookingPotSprite;
            case ThrowablesTest.LightBulb:
                return lightBulbSprite;
            default:
                return potSprite;
        }
    }

    AudioClip ThrowableToHitSFX(ThrowablesTest throwable)
    {
        switch (throwable)
        {
            case ThrowablesTest.Rock:
                return rockHitSfx;
            case ThrowablesTest.Ball:
                return ballHitSfx;
            case ThrowablesTest.CookingPot:
                return cookingPotHitSfx;
            case ThrowablesTest.LightBulb:
                return lightBulbHitSFX;
            default:
                return potHitSfx;
        }
    }

    void HandleSongEvent(EventTypes type)
    {
        switch (type)
        {
            // Text Events
            case EventTypes.Grr:
                GameObject grrText = new GameObject("Grr");
                grrText.transform.position = textPosition;
                grrText.transform.localScale = Vector3.one * 5;

                SpriteRenderer spriteRenderer = grrText.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = grrSprite;

                Destroy(grrText, secondsPerBeat);
                break;
            case EventTypes.Hit3:
                // Creating a parent to store the hit and 3
                GameObject hitParent = new GameObject("Hit 3");

                // Creating a gameobject to show the hit text
                GameObject hitText = new GameObject("Hit");
                hitText.transform.SetParent(hitParent.transform);
                hitText.transform.localScale = Vector3.one * 5;

                // Setting the sprite of the newly created gameobject to the hit text
                SpriteRenderer hitSpriteRenderer = hitText.AddComponent<SpriteRenderer>();
                hitSpriteRenderer.sprite = hitSprite;

                // Creating a gameobject to show the 3
                GameObject threeText = new GameObject("3");
                threeText.transform.SetParent(hitParent.transform);
                threeText.transform.position = new Vector3(hitNumberingOffset, 0, 0);
                threeText.transform.localScale = Vector3.one * 5;

                // Setting the sprite to the 3 sprite
                SpriteRenderer threeSpriteRenderer = threeText.AddComponent<SpriteRenderer>();
                threeSpriteRenderer.sprite = threeSprite;

                // Setting the hit parent to be at the text position
                hitParent.transform.position = textPosition;

                // Setting the parent object to be destroyed after a set amount of time
                Destroy(hitParent, secondsPerBeat * 4);

                break;

            // Background Events
            case EventTypes.BackgroundStart:
                BackgroundControllerTest.isFlashing = true;
                break;
            case EventTypes.BackgroundStop:
                BackgroundControllerTest.isFlashing = false;
                break;
        }
    }

    IEnumerator WaitToLoadTitleScreen(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ResultsScreen.ToResults(notesHit, totalNotes, SceneManager.GetActiveScene().name);
    }
}
