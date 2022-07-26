using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotTest : MonoBehaviour
{
    public static float beatsToHit = 1; // 1 beat from creation until being hit
    public static float leniencyMultiplier = 0.25f;

    public AudioSource hitSFX;
    public AudioSource throwOnBeatSFX;
    public AudioSource throwOffBeatSFX;

    public float offset;
    public bool onBeat = true;

    public Sprite hitGraphic;

    private float timeSinceCreation = 0;
    private bool missed = false;

    private float BPM = SongManagerTest.BPM;
    private float secondsPerBeat;
    private bool canBeHit = false;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        secondsPerBeat = 60 / BPM;

        offset = animator.GetFloat("Offset");
        timeSinceCreation = offset;

        onBeat = animator.GetBool("OnBeat");
        if (onBeat) throwOnBeatSFX.Play();
        else throwOffBeatSFX.Play();
    }
    
    // Update is called once per frame
    void Update()
    {
        timeSinceCreation += Time.deltaTime;

        canBeHit = timeSinceCreation >= (secondsPerBeat * beatsToHit) - (secondsPerBeat * leniencyMultiplier)
            && timeSinceCreation <= (secondsPerBeat * beatsToHit) + (secondsPerBeat * leniencyMultiplier);

        missed = timeSinceCreation >= secondsPerBeat * beatsToHit + secondsPerBeat * leniencyMultiplier;

        if (IsDead()) Destroy(gameObject);
    }

    public bool CanBeHit()
    {
        return canBeHit;
    }

    public bool Missed()
    {
        return missed;
    }

    public float Hit()
    {
        // Playing the hit animation and hit sfx
        animator.SetTrigger("HitOnBeat");
        hitSFX.Play();

        // Creating a hit graphic at the center of the screen and destroying it after a small delay
        GameObject hitGraphicObject = new GameObject("Hit Graphic");
        hitGraphicObject.transform.position = new Vector3(0, 0, -0.5f);
        hitGraphicObject.transform.localScale = Vector3.one * 3.5f;

        SpriteRenderer hitRenderer = hitGraphicObject.AddComponent<SpriteRenderer>();
        hitRenderer.sprite = hitGraphic;

        Destroy(hitGraphicObject, 0.1f);

        // Returns the delay from the hit
        return timeSinceCreation - secondsPerBeat * beatsToHit;
    }

    public bool IsDead()
    {
        return timeSinceCreation >= 10 * secondsPerBeat;
    }

    float Distance1D(float x1, float x2)
    {
        return Mathf.Abs(x1 - x2);
    }
}
