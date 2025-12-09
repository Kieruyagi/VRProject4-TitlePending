using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnCopyOnGrab : MonoBehaviour
{
    public GameObject objectPrefab;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private SpawnCopyOnGrab scriptOG;
    private Rigidbody rb;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        scriptOG = gameObject.GetComponent<SpawnCopyOnGrab>();
        rb = gameObject.GetComponent<Rigidbody>();

        if (objectPrefab == null)
        {
            objectPrefab = gameObject;
        }

        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        GameObject clone = Instantiate(objectPrefab, transform.position, transform.rotation);


        if (!clone.TryGetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>(out _))
        {
            clone.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        }

    }
}
