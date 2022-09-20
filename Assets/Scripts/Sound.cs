using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    [SerializeReference]
    public string name;
    [SerializeReference]
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    public bool loop = false;
    [HideInInspector]
    public AudioSource source;
}
