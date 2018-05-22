using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Presets;

public class WorldAudioScript : MonoBehaviour {

	public Preset normalPreset;
    public Preset distortedPreset;

    private void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        normalPreset.ApplyTo(this.GetComponent<AudioSource>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        distortedPreset.ApplyTo(this.GetComponent<AudioSource>());
    }

}
