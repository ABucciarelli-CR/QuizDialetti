using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Collection", menuName = "Extra/Audio Collection")]
public class AudioCollection : ScriptableObject
{
    public AudioClip winAudio;
    public AudioClip loseAudio;
    public AudioClip bgAudio;
}
