using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManagerTest : MonoBehaviour
{
    [Header("Debugging + Testing")]
    public AudioClip beepSFX;

    [Header("Song Info")]
    public static float BPM = 119;
    public float throwOffset = -0.13f;
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

    public AudioClip potHitSfx;
    public AudioClip rockHitSfx;
    public AudioClip ballHitSfx;
    public AudioClip cookingPotHitSfx;

    public static bool songStarted = false;

    private float previousHit = 0;
    private float secondsPerBeat;
    private float timeBetweenBeat = -1;
    private List<GameObject> thrownObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        secondsPerBeat = 60 / BPM;

        // Karate man returns from megamix charting
        notes.Add(new TestNote(ThrowablesTest.Pot, 16));
        notes.Add(new TestNote(ThrowablesTest.Pot, 24));
        notes.Add(new TestNote(ThrowablesTest.Pot, 32));
        notes.Add(new TestNote(ThrowablesTest.Pot, 40));
        notes.Add(new TestNote(ThrowablesTest.Pot, 48));
        notes.Add(new TestNote(ThrowablesTest.Rock, 52)); // Rock
        songEvents.Add(new SongEvent(EventTypes.BackgroundStart, 57)); // Background start
        notes.Add(new TestNote(ThrowablesTest.Pot, 60));
        notes.Add(new TestNote(ThrowablesTest.Pot, 64));
        notes.Add(new TestNote(ThrowablesTest.Pot, 68));
        notes.Add(new TestNote(ThrowablesTest.Pot, 72));
        notes.Add(new TestNote(ThrowablesTest.Ball, 76)); // Soccer ball
        songEvents.Add(new SongEvent(EventTypes.Grr, 78)); // Grr for 1 beat
        notes.Add(new TestNote(ThrowablesTest.CookingPot, 81)); // Cooking pot
        notes.Add(new TestNote(ThrowablesTest.Pot, 88));
        notes.Add(new TestNote(ThrowablesTest.Rock, 92)); // Rock
        notes.Add(new TestNote(ThrowablesTest.Pot, 98));
        notes.Add(new TestNote(ThrowablesTest.Pot, 100));
        notes.Add(new TestNote(ThrowablesTest.Pot, 102));
        notes.Add(new TestNote(ThrowablesTest.Pot, 104));
        notes.Add(new TestNote(ThrowablesTest.Ball, 106));; // Soccer ball
        songEvents.Add(new SongEvent(EventTypes.Hit3, 107)); // Hit 3 Sound
        notes.Add(new TestNote(ThrowablesTest.Pot, 110)); // Hit 3
        notes.Add(new TestNote(ThrowablesTest.Pot, 111)); // Hit 3
        notes.Add(new TestNote(ThrowablesTest.Rock, 112)); // Hit 3 (Rock)
        notes.Add(new TestNote(ThrowablesTest.Pot, 114));
        notes.Add(new TestNote(ThrowablesTest.Pot, 116));
        notes.Add(new TestNote(ThrowablesTest.Pot, 118));
        notes.Add(new TestNote(ThrowablesTest.Pot, 120));
        notes.Add(new TestNote(ThrowablesTest.Pot, 122));
        notes.Add(new TestNote(ThrowablesTest.Pot, 124));
        notes.Add(new TestNote(ThrowablesTest.Pot, 126));
        notes.Add(new TestNote(ThrowablesTest.Pot, 128));
        notes.Add(new TestNote(ThrowablesTest.Pot, 130));
        notes.Add(new TestNote(ThrowablesTest.Rock, 132)); // Rock
        songEvents.Add(new SongEvent(EventTypes.Hit3, 131)); // Hit 3 Sound
        notes.Add(new TestNote(ThrowablesTest.Pot, 134)); // Hit 3
        notes.Add(new TestNote(ThrowablesTest.Pot, 135)); // Hit 3
        notes.Add(new TestNote(ThrowablesTest.CookingPot, 136)); // Hit 3 (Cooking pot)
        bpmChanges.Add(new BPMChange(136, 150));
        bpmChanges.Add(new BPMChange(148, 140));
        songEvents.Add(new SongEvent(EventTypes.Grr, 148)); // Grr
        songEvents.Add(new SongEvent(EventTypes.BackgroundStop, 148)); // Background Stop
        notes.Add(new TestNote(ThrowablesTest.Rock, 156)); // Rock
        // HIT x NOTE: The audio and text shows up 1.5 beats before the objects are thrown. The text can disappear 1.5 + x beats after the sfx is played
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

                ThrowPot(offset, ThrowableToSprite(latestNote.Type), ThrowableToHitSFX(latestNote.Type), offBeat: !(latestNote.BeatNumber % 2 == 0));
                notes.RemoveAt(0);

                // Setting the hard punch to be whether the thrown object was a rock or cooking pot
                KarateJoeAnims.hardPunchNext = (latestNote.Type == ThrowablesTest.Rock || latestNote.Type == ThrowablesTest.CookingPot || latestNote.Type == ThrowablesTest.Ball);
            }
        }
        else
        {
            // End of song, go to results screen
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
            timeBetweenBeat = 0 + throwOffset;
            songAudio.Play();
            songStarted = true;
        }

        if (Input.GetKeyDown(KeyCode.Z)) punchSFX.Play();

        if (Input.GetKeyDown(KeyCode.Z) && thrownObjects.Count > 0)
        {
            GameObject currentThrownObject = thrownObjects[0];
            PotTest currentPot = currentThrownObject.GetComponent<PotTest>();

            if (currentPot.CanBeHit())
            {
                previousHit = currentPot.Hit();
                thrownObjects.Remove(currentThrownObject);
            }
            else
            {
                if (currentPot.Missed())
                {
                    thrownObjects.RemoveAt(0);
                }
            }
        }
    }

    private void OnGUI()
    {
        // Displaying the hits and misses
        GUI.TextField(new Rect(0, 0, 500, 20), $"Prev Hit: {previousHit * 1000} ms");
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
}
