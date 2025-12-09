using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{
    public int sceneNum;
    public AudioSource audioSource;
    private float length;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            Debug.Log($"Loading {sceneNum}");

            //play audio if there
            if(audioSource != null)
            {
                Debug.Log("Playing leaving audio");
                length = audioSource.clip.length;
                audioSource.Play();
            }
            Invoke("sceneLoad", length);
        }
    }

    public void sceneLoad()
    {
        SceneManager.LoadScene(sceneNum);
    }
}
