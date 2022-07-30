using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Song
{
    public Song(float _bpm, List<TestNote> _notes, List<BPMChange> _bpmChanges, List<SongEvent> _songEvents, float _songOffset)
    {
        BPM = _bpm;
        Notes = _notes;
        BpmChanges = _bpmChanges;
        SongEvents = _songEvents;
        SongOffset = _songOffset;
    }

    public float BPM;
    public List<TestNote> Notes;
    public List<BPMChange> BpmChanges;
    public List<SongEvent> SongEvents;
    public float SongOffset;
}

public class Songs
{
    // The song for level 1 (Karate Man Megamix Story)
    public static Song Song1()
    {
        List<TestNote> notes = new List<TestNote>();
        List<BPMChange> bpmChanges = new List<BPMChange>();
        List<SongEvent> songEvents = new List<SongEvent>();

        float bpm = 120;
        float songOffset = 0;

        notes.Add(new TestNote(ThrowablesTest.Pot, 16));
        notes.Add(new TestNote(ThrowablesTest.Pot, 24));
        notes.Add(new TestNote(ThrowablesTest.Pot, 28));
        notes.Add(new TestNote(ThrowablesTest.Pot, 32));
        notes.Add(new TestNote(ThrowablesTest.Pot, 34));
        notes.Add(new TestNote(ThrowablesTest.Pot, 36));
        notes.Add(new TestNote(ThrowablesTest.Rock, 40)); // Rock
        songEvents.Add(new SongEvent(EventTypes.BackgroundStart, 40)); // Background start
        notes.Add(new TestNote(ThrowablesTest.Pot, 48));
        notes.Add(new TestNote(ThrowablesTest.Pot, 56));
        notes.Add(new TestNote(ThrowablesTest.Pot, 60));
        notes.Add(new TestNote(ThrowablesTest.Pot, 64));
        notes.Add(new TestNote(ThrowablesTest.Pot, 66));
        notes.Add(new TestNote(ThrowablesTest.Pot, 68));
        songEvents.Add(new SongEvent(EventTypes.Grr, 68)); // Grr for 1 beat
        notes.Add(new TestNote(ThrowablesTest.Ball, 71)); // Soccer ball
        songEvents.Add(new SongEvent(EventTypes.Hit3, 74.5f)); // Hit 3 Sound
        notes.Add(new TestNote(ThrowablesTest.Pot, 76));
        notes.Add(new TestNote(ThrowablesTest.Pot, 78));
        notes.Add(new TestNote(ThrowablesTest.Pot, 79));
        notes.Add(new TestNote(ThrowablesTest.Rock, 80));
        notes.Add(new TestNote(ThrowablesTest.Rock, 86));
        songEvents.Add(new SongEvent(EventTypes.BackgroundStop, 85)); // Background Stop

        return new Song(bpm, notes, bpmChanges, songEvents, songOffset);
    }

    // The song for level 2 (Karate Man 1 GBA / Returns Megamix)
    public static Song Song2()
    {
        List<TestNote> notes = new List<TestNote>();
        List<BPMChange> bpmChanges = new List<BPMChange>();
        List<SongEvent> songEvents = new List<SongEvent>();

        float bpm = 119;
        float songOffset = -0.13f;

        notes.Add(new TestNote(ThrowablesTest.Pot, 16));
        notes.Add(new TestNote(ThrowablesTest.Pot, 24));
        notes.Add(new TestNote(ThrowablesTest.Pot, 32));
        notes.Add(new TestNote(ThrowablesTest.Pot, 40));
        notes.Add(new TestNote(ThrowablesTest.Pot, 48));
        notes.Add(new TestNote(ThrowablesTest.Rock, 52)); // Rock
        songEvents.Add(new SongEvent(EventTypes.BackgroundStart, 56)); // Background start (Originally 57 but it should probably be 56)
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
        notes.Add(new TestNote(ThrowablesTest.Ball, 106)); ; // Soccer ball
        songEvents.Add(new SongEvent(EventTypes.Hit3, 106.5f)); // Hit 3 Sound
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
        songEvents.Add(new SongEvent(EventTypes.Hit3, 130.5f)); // Hit 3 Sound
        notes.Add(new TestNote(ThrowablesTest.Pot, 134)); // Hit 3
        notes.Add(new TestNote(ThrowablesTest.Pot, 135)); // Hit 3
        notes.Add(new TestNote(ThrowablesTest.CookingPot, 136)); // Hit 3 (Cooking pot)
        bpmChanges.Add(new BPMChange(136, 150));
        bpmChanges.Add(new BPMChange(148, 140));
        songEvents.Add(new SongEvent(EventTypes.Grr, 148)); // Grr
        songEvents.Add(new SongEvent(EventTypes.BackgroundStop, 148)); // Background Stop
        notes.Add(new TestNote(ThrowablesTest.Rock, 156)); // Rock

        return new Song(bpm, notes, bpmChanges, songEvents, songOffset);
    }

    // The song for level 3 (Karate Man 1 DS / Kicks Megamix)
    public static Song Song3()
    {
        List<TestNote> notes = new List<TestNote>();
        List<BPMChange> bpmChanges = new List<BPMChange>();
        List<SongEvent> songEvents = new List<SongEvent>();

        float bpm = 128;
        float songOffset = 0f;

        notes.Add(new TestNote(ThrowablesTest.Pot, 17)); // Punch
        notes.Add(new TestNote(ThrowablesTest.Pot, 17.5f)); // Kick!
        notes.Add(new TestNote(ThrowablesTest.Pot, 21));
        notes.Add(new TestNote(ThrowablesTest.Pot, 23));
        notes.Add(new TestNote(ThrowablesTest.Pot, 25));
        notes.Add(new TestNote(ThrowablesTest.Pot, 27));
        notes.Add(new TestNote(ThrowablesTest.Pot, 29));
        notes.Add(new TestNote(ThrowablesTest.LightBulb, 31)); // Lightbulb
        notes.Add(new TestNote(ThrowablesTest.Pot, 33)); // Punch
        notes.Add(new TestNote(ThrowablesTest.Pot, 33.5f)); // Kick!
        notes.Add(new TestNote(ThrowablesTest.Pot, 35));
        notes.Add(new TestNote(ThrowablesTest.Pot, 37));
        notes.Add(new TestNote(ThrowablesTest.Pot, 39));
        notes.Add(new TestNote(ThrowablesTest.Pot, 41));
        notes.Add(new TestNote(ThrowablesTest.LightBulb, 43)); // Lightbulb
        notes.Add(new TestNote(ThrowablesTest.Pot, 45)); // Punch
        notes.Add(new TestNote(ThrowablesTest.Pot, 45.5f)); // Kick!
        notes.Add(new TestNote(ThrowablesTest.Pot, 49));
        notes.Add(new TestNote(ThrowablesTest.Pot, 51));
        notes.Add(new TestNote(ThrowablesTest.Pot, 53));
        notes.Add(new TestNote(ThrowablesTest.Pot, 55));
        notes.Add(new TestNote(ThrowablesTest.Pot, 57));
        notes.Add(new TestNote(ThrowablesTest.Pot, 59));
        notes.Add(new TestNote(ThrowablesTest.Pot, 61));
        notes.Add(new TestNote(ThrowablesTest.Pot, 63));
        notes.Add(new TestNote(ThrowablesTest.Pot, 65));
        notes.Add(new TestNote(ThrowablesTest.Pot, 67));
        notes.Add(new TestNote(ThrowablesTest.Pot, 69));
        notes.Add(new TestNote(ThrowablesTest.Pot, 71));
        notes.Add(new TestNote(ThrowablesTest.Pot, 73));
        notes.Add(new TestNote(ThrowablesTest.LightBulb, 75)); // Lightbulb
        notes.Add(new TestNote(ThrowablesTest.Pot, 77)); // Punch
        notes.Add(new TestNote(ThrowablesTest.Pot, 77.5f)); // Kick!
        notes.Add(new TestNote(ThrowablesTest.Pot, 81));
        notes.Add(new TestNote(ThrowablesTest.Pot, 83));
        notes.Add(new TestNote(ThrowablesTest.Pot, 85));
        notes.Add(new TestNote(ThrowablesTest.Pot, 87));
        notes.Add(new TestNote(ThrowablesTest.Pot, 89));
        notes.Add(new TestNote(ThrowablesTest.Pot, 91));
        notes.Add(new TestNote(ThrowablesTest.Pot, 93));
        notes.Add(new TestNote(ThrowablesTest.Pot, 95));
        notes.Add(new TestNote(ThrowablesTest.Pot, 97));
        notes.Add(new TestNote(ThrowablesTest.Pot, 99));
        notes.Add(new TestNote(ThrowablesTest.Pot, 101));
        notes.Add(new TestNote(ThrowablesTest.Pot, 103));
        notes.Add(new TestNote(ThrowablesTest.Pot, 105));
        notes.Add(new TestNote(ThrowablesTest.LightBulb, 107)); // Lightbulb
        notes.Add(new TestNote(ThrowablesTest.Pot, 109)); // Punch
        notes.Add(new TestNote(ThrowablesTest.Pot, 109.5f)); // Kick!
        notes.Add(new TestNote(ThrowablesTest.Pot, 113));
        notes.Add(new TestNote(ThrowablesTest.Pot, 115));
        notes.Add(new TestNote(ThrowablesTest.Pot, 117));
        notes.Add(new TestNote(ThrowablesTest.Pot, 119));
        notes.Add(new TestNote(ThrowablesTest.Pot, 121));
        notes.Add(new TestNote(ThrowablesTest.Pot, 123));
        notes.Add(new TestNote(ThrowablesTest.Pot, 125.5f)); // Offbeat
        notes.Add(new TestNote(ThrowablesTest.Pot, 127.5f)); // Offbeat
        notes.Add(new TestNote(ThrowablesTest.Pot, 129.5f)); // Offbeat
        notes.Add(new TestNote(ThrowablesTest.Pot, 131));
        songEvents.Add(new SongEvent(EventTypes.Hit3, 131.5f)); // Hit 3 SFX
        notes.Add(new TestNote(ThrowablesTest.Pot, 133));
        notes.Add(new TestNote(ThrowablesTest.Pot, 135)); // Hit 3
        notes.Add(new TestNote(ThrowablesTest.Pot, 136)); // Hit 3
        notes.Add(new TestNote(ThrowablesTest.Rock, 137)); // Hit 3
        notes.Add(new TestNote(ThrowablesTest.Pot, 139));
        notes.Add(new TestNote(ThrowablesTest.Pot, 141));
        notes.Add(new TestNote(ThrowablesTest.Pot, 143));
        notes.Add(new TestNote(ThrowablesTest.Pot, 145));
        notes.Add(new TestNote(ThrowablesTest.LightBulb, 147)); // Lightbulb
        notes.Add(new TestNote(ThrowablesTest.Pot, 149)); // Punch
        notes.Add(new TestNote(ThrowablesTest.Pot, 149.5f)); // Kick!
        songEvents.Add(new SongEvent(EventTypes.BackgroundStart, 151)); // Background start
        notes.Add(new TestNote(ThrowablesTest.Pot, 153));
        notes.Add(new TestNote(ThrowablesTest.Pot, 155));
        songEvents.Add(new SongEvent(EventTypes.Hit3, 155.5f)); // Hit 3 SFX
        notes.Add(new TestNote(ThrowablesTest.Pot, 157));
        notes.Add(new TestNote(ThrowablesTest.Pot, 159)); // Hit 3
        notes.Add(new TestNote(ThrowablesTest.Pot, 160)); // Hit 3
        notes.Add(new TestNote(ThrowablesTest.Rock, 161)); // Hit 3
        notes.Add(new TestNote(ThrowablesTest.Pot, 163));
        notes.Add(new TestNote(ThrowablesTest.Pot, 165));
        notes.Add(new TestNote(ThrowablesTest.Pot, 167));
        notes.Add(new TestNote(ThrowablesTest.Pot, 169));
        notes.Add(new TestNote(ThrowablesTest.Pot, 171));
        notes.Add(new TestNote(ThrowablesTest.Pot, 173));
        notes.Add(new TestNote(ThrowablesTest.Pot, 175));
        notes.Add(new TestNote(ThrowablesTest.Pot, 177));
        notes.Add(new TestNote(ThrowablesTest.Pot, 179));
        songEvents.Add(new SongEvent(EventTypes.Hit3, 179.5f)); // Hit 3 SFX
        notes.Add(new TestNote(ThrowablesTest.Pot, 181));
        notes.Add(new TestNote(ThrowablesTest.Pot, 183)); // Hit 3
        notes.Add(new TestNote(ThrowablesTest.Pot, 184)); // Hit 3
        notes.Add(new TestNote(ThrowablesTest.Rock, 185)); // Hit 3
        notes.Add(new TestNote(ThrowablesTest.Pot, 187));
        songEvents.Add(new SongEvent(EventTypes.Hit3, 187.5f)); // Hit 3 SFX
        notes.Add(new TestNote(ThrowablesTest.Pot, 189));
        notes.Add(new TestNote(ThrowablesTest.Pot, 191)); // Hit 3
        notes.Add(new TestNote(ThrowablesTest.Pot, 192)); // Hit 3
        notes.Add(new TestNote(ThrowablesTest.Rock, 193)); // Hit 3
        notes.Add(new TestNote(ThrowablesTest.Pot, 195));
        notes.Add(new TestNote(ThrowablesTest.Pot, 197));
        notes.Add(new TestNote(ThrowablesTest.Pot, 199));
        notes.Add(new TestNote(ThrowablesTest.Pot, 201));
        notes.Add(new TestNote(ThrowablesTest.Pot, 203));
        notes.Add(new TestNote(ThrowablesTest.Pot, 205));
        notes.Add(new TestNote(ThrowablesTest.Pot, 207));
        notes.Add(new TestNote(ThrowablesTest.Pot, 209));
        notes.Add(new TestNote(ThrowablesTest.LightBulb, 211)); // Lightbulb
        notes.Add(new TestNote(ThrowablesTest.Pot, 213)); // Punch
        notes.Add(new TestNote(ThrowablesTest.Pot, 213.5f)); // Kick!
        notes.Add(new TestNote(ThrowablesTest.Pot, 217));
        notes.Add(new TestNote(ThrowablesTest.Pot, 219));
        notes.Add(new TestNote(ThrowablesTest.Pot, 221));
        notes.Add(new TestNote(ThrowablesTest.LightBulb, 223)); // Lightbulb
        notes.Add(new TestNote(ThrowablesTest.Pot, 225)); // Punch
        notes.Add(new TestNote(ThrowablesTest.Pot, 225.5f)); // Kick!
        notes.Add(new TestNote(ThrowablesTest.Pot, 229));
        notes.Add(new TestNote(ThrowablesTest.LightBulb, 231)); // Lightbulb
        notes.Add(new TestNote(ThrowablesTest.Pot, 233)); // Punch
        notes.Add(new TestNote(ThrowablesTest.Pot, 233.5f)); // Kick!
        notes.Add(new TestNote(ThrowablesTest.Pot, 235));
        notes.Add(new TestNote(ThrowablesTest.Pot, 237));
        notes.Add(new TestNote(ThrowablesTest.LightBulb, 239)); // Lightbulb
        notes.Add(new TestNote(ThrowablesTest.Pot, 241)); // Punch
        notes.Add(new TestNote(ThrowablesTest.Pot, 241.5f)); // Kick!
        notes.Add(new TestNote(ThrowablesTest.Pot, 243));
        notes.Add(new TestNote(ThrowablesTest.Pot, 245));
        notes.Add(new TestNote(ThrowablesTest.LightBulb, 247)); // Lightbulb
        notes.Add(new TestNote(ThrowablesTest.Pot, 249)); // Punch
        notes.Add(new TestNote(ThrowablesTest.Pot, 249.5f)); // Kick!
        notes.Add(new TestNote(ThrowablesTest.LightBulb, 251)); // Lightbulb
        notes.Add(new TestNote(ThrowablesTest.Pot, 253)); // Punch
        notes.Add(new TestNote(ThrowablesTest.Pot, 253.5f)); // Kick!
        notes.Add(new TestNote(ThrowablesTest.Pot, 257)); // Punch
        notes.Add(new TestNote(ThrowablesTest.Pot, 257.5f)); // Kick!
        songEvents.Add(new SongEvent(EventTypes.BackgroundStop, 257)); // Background stop
        notes.Add(new TestNote(ThrowablesTest.Pot, 266));

        return new Song(bpm, notes, bpmChanges, songEvents, songOffset);
    }
}