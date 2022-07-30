using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarateJoeAnims : MonoBehaviour
{
    public static bool hardPunchNext = false;

    private float BPM;// = SongManagerTest.BPM;
    private float secondsPerBeat;
    private float timeBetweenBeat = 0;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        BPM = SongManagerTest.BPM;

        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", BPM / 60);

        secondsPerBeat = 60 / BPM;
    }

    private void FixedUpdate()
    {
        timeBetweenBeat += (SongManagerTest.songStarted ? Time.deltaTime : 0);

        if (timeBetweenBeat >= secondsPerBeat && !animator.GetCurrentAnimatorStateInfo(0).IsName("Punch") && !animator.GetCurrentAnimatorStateInfo(0).IsName("PunchHard"))
        {
            float idleBopOffset = timeBetweenBeat - secondsPerBeat;
            timeBetweenBeat = idleBopOffset;
            animator.SetFloat("BopOffset", idleBopOffset);
            animator.SetTrigger("IdleBop");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!hardPunchNext) animator.SetTrigger("Punch");
            else animator.SetTrigger("HardPunch");
        }
    }

    public void ChangeBPM()
    {
        BPM = SongManagerTest.BPM;
        animator.SetFloat("Speed", BPM / 60);
        secondsPerBeat = 60 / BPM;
    }
}
