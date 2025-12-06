using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VRPhoneCall : MonoBehaviour
{
    [Header("Phone Audio")]
    public AudioClip ringTone;
    public AudioClip voiceMessage;     // The caller’s lore/story audio
    public AudioSource audioSource;

    [Header("Interaction")]
    public XRBaseInteractable interactable;  // The phone receiver / phone object

    [Header("Events")]
    public UnityEvent OnPhoneAnswered;
    public UnityEvent OnPhoneHungUp;

    private bool isRinging = false;
    private bool isPlayingMessage = false;

    void Start()
    {
        // Start the phone ringing when the scene loads
        StartRinging();

        // Listen for interact events
        interactable.selectEntered.AddListener(OnPickUp);
        interactable.selectExited.AddListener(OnPutDown);
    }

    public void StartRinging()
    {
        isRinging = true;
        audioSource.clip = ringTone;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void OnPickUp(SelectEnterEventArgs args)
    {
        if (isRinging)
        {
            // Stop ringing and play the story audio
            isRinging = false;
            audioSource.Stop();

            PlayStoryMessage();
            OnPhoneAnswered.Invoke();
        }
    }

    private void PlayStoryMessage()
    {
        isPlayingMessage = true;
        audioSource.loop = false;
        audioSource.clip = voiceMessage;
        audioSource.Play();
    }

    private void OnPutDown(SelectExitEventArgs args)
    {
        if (isPlayingMessage)
        {
            OnPhoneHungUp.Invoke();
            audioSource.Stop();
            isPlayingMessage = false;
        }
    }
}
