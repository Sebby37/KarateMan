using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControllerTest : MonoBehaviour
{
    public static bool isFlashing = false;
    public Color backgroundColour;
    public List<Sprite> backgroundSprites = new List<Sprite>();
    public Sprite noFlashBackground;

    public Vector3 boxSizeNoBackground;
    public Vector3 boxSizeNoBorder;

    private int backgroundIndex = 0;
    private float timeBetweenBeat = 0;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlashing)
        {
            timeBetweenBeat += Time.deltaTime;
            transform.localScale = boxSizeNoBorder;
            spriteRenderer.color = Color.white;
            Flash();
        }
        else
        {
            transform.localScale = boxSizeNoBackground;
            spriteRenderer.sprite = noFlashBackground;
            spriteRenderer.color = backgroundColour;
        }
    }

    void Flash()
    {
        if (backgroundIndex >= backgroundSprites.Count) backgroundIndex = 0;
        spriteRenderer.sprite = backgroundSprites[backgroundIndex];

        float secondsPerBeat = 60 / SongManagerTest.BPM;

        if (timeBetweenBeat >= secondsPerBeat)
        {
            timeBetweenBeat -= secondsPerBeat;
            backgroundIndex++;
        }
    }
}
