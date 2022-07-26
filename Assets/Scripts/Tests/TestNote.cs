using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThrowablesTest
{
    Pot,
    Rock,
    Ball,
    CookingPot,
    LogByBulb
}

public enum EventTypes
{
    Grr,
    Hit2,
    Hit3,
    Hit4,
    BackgroundStart,
    BackgroundStop,
    BackgroundSpeedFast,
    BackgroundSpeedSlow,
    BackgroundSpeedStop
}

public struct TestNote
{
    public TestNote(ThrowablesTest _type, float _beatNumber)
    {
        Type = _type;
        BeatNumber = _beatNumber;
    }

    public ThrowablesTest Type { get; }
    public float BeatNumber { get; }
}

public struct BPMChange
{
    public BPMChange(float _beatNumber, float _newBPM)
    {
        BeatNumber = _beatNumber;
        NewBPM = _newBPM;
    }

    public float BeatNumber { get; }
    public float NewBPM { get; }
}

public struct SongEvent
{
    public SongEvent(EventType _type, float _beatNumber)
    {
        Type = _type;
        BeatNumber = _beatNumber;
    }

    public EventType Type { get; }
    public float BeatNumber { get; }
}