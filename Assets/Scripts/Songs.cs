using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Song
{
    public Song(List<TestNote> _notes, List<BPMChange> _bpmChanges, List<SongEvent> _songEvents)
    {
        Notes = _notes;
        BpmChanges = _bpmChanges;
        SongEvents = _songEvents;
    }
    
    public List<TestNote> Notes;
    public List<BPMChange> BpmChanges;
    public List<SongEvent> SongEvents;
}

public class Songs
{
    public static Song Song1()
    {
        return new Song();
    }
}