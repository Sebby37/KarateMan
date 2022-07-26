using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 This script is designed to test input leniency. 
 A sound will play each beat and the time in ms from which the beat was hit will be shown.
 */
public class LeniencyTest : MonoBehaviour
{
    public AudioClip beepSFX;
    public SpriteRenderer timingSquare;

    [Range(60, 240)]
    public float BPM = 120;
    public float leniencyMultiplier = 0.25f;

    private float leniency;
    private float secondsPerBeat;
    private float timeSinceStart = 0;
    private float beatCounter = 0;

    private bool canHit = false;
    private float hits = 0;
    private float misses = 0;
    // Start is called before the first frame update
    void Start()
    {
        ChangeBPM(BPM);
    }

    // Update is called once per frame
    void Update()
    {
        // Adding the time to the variables that are frame independant
        timeSinceStart += Time.deltaTime;
        beatCounter += Time.deltaTime;

        // If the current beat can be hit, the square is set to green
        if (CanHitBeat())
        {
            timingSquare.color = Color.green;

            // If the current beat can be hit and space was pressed, a hit is added
            if (Input.GetKeyDown(KeyCode.Space) && canHit)
            {
                hits++;
                canHit = false;
            }
        }
        else
        {
            // If a beat cannot be hit, the timing square is set to red
            timingSquare.color = Color.red;

            // If space was pressed, a miss is added
            if (Input.GetKeyDown(KeyCode.Space)) misses++;
            if (!canHit) canHit = true; // Setting canHit to true for the next hit cycle
        }
        
        // Waiting for the exact next beat to beep
        if (beatCounter >= secondsPerBeat)
        {
            Debug.Log($"{beatCounter} | {timeSinceStart} | {beatCounter - secondsPerBeat} | {timeSinceStart % secondsPerBeat}");
            Beep();
            beatCounter = timeSinceStart % secondsPerBeat; // Accounting for the beat counter slowly moving offbeat due to time between frames
            timingSquare.color = Color.yellow;
        }
    }

    private void OnGUI()
    {
        // Displaying the hits and misses
        GUI.TextField(new Rect(0, 0, 200, 20), $"Hits: {hits} | Misses: {misses}");
    }

    void Beep()
    {
        AudioSource.PlayClipAtPoint(beepSFX, Vector3.zero);
    }

    float Distance1D(float x1, float x2)
    {
        return Mathf.Abs(x1 - x2);
    }

    // Function to check if a value is within a leniency limit
    bool ValueWithinLeniency(float value, float limit, float leniencyLimit)
    {
        float moddedValue = value % limit;

        return (moddedValue <= leniencyLimit || moddedValue >= value - leniencyLimit);
    }

    // Function to change the BPM
    void ChangeBPM(float newBPM)
    {
        BPM = newBPM;
        secondsPerBeat = 60 / BPM;
        leniency = secondsPerBeat * leniencyMultiplier;
    }

    // Simplifying the ValueWithinLeniency call
    bool CanHitBeat()
    {
        return ValueWithinLeniency(timeSinceStart, secondsPerBeat, leniency);
    }
}
