using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnCopyOnGrab : MonoBehaviour
{
    [Header("Prefab to Spawn (leave empty to use this object)")]
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
            // Default to duplicating this object
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
        // Spawn copy where the original is
        GameObject clone = Instantiate(
            objectPrefab,
            transform.position,
            transform.rotation
        );
        Destroy(scriptOG);
        rb.useGravity = true;

        

        // Ensure the new object is grabbable
        if (!clone.TryGetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>(out _))
        {
            clone.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        }

    }
}
