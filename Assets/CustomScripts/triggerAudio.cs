using UnityEngine;

public class triggerAudio : MonoBehaviour
{

    private AudioSource audio;
    private bool played = false;


    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !played)
        {
            audio.Play();
            played = true;
        }
    }

}
