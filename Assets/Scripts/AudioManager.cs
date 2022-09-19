using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeReference]
    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in sounds){
            gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
