using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarateJoeAnims : MonoBehaviour
{
    private float BPM = SongManagerTest.BPM;
    private float secondsPerBeat;
    private float timeBetweenBeat = 0;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", BPM / 60);

        secondsPerBeat = 60 / BPM;
    }

    private void FixedUpdate()
    {
        timeBetweenBeat += (SongManagerTest.songStarted ? Time.deltaTime : 0);

        if (timeBetweenBeat >= secondsPerBeat && !animator.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
        {
            float idleBopOffset = timeBetweenBeat - secondsPerBeat;
            timeBetweenBeat = 0;
            animator.SetFloat("BopOffset", idleBopOffset);
            animator.SetTrigger("IdleBop");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Punch");
        }
    }

    public void ChangeBPM()
    {
        BPM = SongManagerTest.BPM;
        animator.SetFloat("Speed", BPM / 60);
        secondsPerBeat = 60 / BPM;
    }
}
