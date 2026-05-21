using UnityEngine;

public class GlassesController : MonoBehaviour
{
    [Header("Settings")]
    public bool glassesOn = false;
    public float pickupRange = 10f;

    [Header("References")]
    public GameObject glassesObject;
    public GameObject[] energyWasteDevices;
    public GameObject scannerPrefab;

    private bool glassesPickedUp = false;
    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (!glassesPickedUp)
        {
            if (Input.GetKeyDown(KeyCode.E))
                TryPickupGlasses();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
                ToggleGlasses();
        }
    }

    void TryPickupGlasses()
    {
        Ray ray = playerCamera.ScreenPointToRay(
            new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * pickupRange,
                      Color.red, 2f);

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name
                      + " | Tag: " + hit.collider.tag);

            if (hit.collider.CompareTag("Glasses"))
            {
                glassesPickedUp = true;
                glassesObject.SetActive(false);
                Debug.Log("Glasses picked up!");
            }
        }
        else
        {
            Debug.Log("Raycast hit nothing!");
        }
    }

    void ToggleGlasses()
    {
        glassesOn = !glassesOn;

        foreach (GameObject device in energyWasteDevices)
        {
            Renderer rend = device.GetComponent<Renderer>();
            if (rend != null)
            {
                SwitchController sw = FindSwitchForDevice(device);
                bool alreadyOff = sw != null && sw.isTurnedOff;

                if (glassesOn)
                {
                    if (alreadyOff)
                    {
                        rend.material.EnableKeyword("_EMISSION");
                        rend.material.SetColor("_EmissionColor", Color.green * 2f);
                    }
                    else
                    {
                        rend.material.EnableKeyword("_EMISSION");
                        rend.material.SetColor("_EmissionColor", Color.red * 2f);
                    }
                }
                else
                {
                    rend.material.DisableKeyword("_EMISSION");
                    rend.material.SetColor("_EmissionColor", Color.black);
                }
            }
        }

        if (glassesOn && scannerPrefab != null)
        {
            Vector3 spawnPos = new Vector3(
                this.transform.position.x,
                0f,
                this.transform.position.z
            );
            Instantiate(scannerPrefab, spawnPos, Quaternion.identity);
        }

        HUDManager.Instance.UpdateGlassesState(glassesOn);
        Debug.Log("Glasses: " + (glassesOn ? "ON" : "OFF"));
    }

    // Find switch linked to device
    SwitchController FindSwitchForDevice(GameObject device)
    {
        SwitchController[] allSwitches =
            FindObjectsOfType<SwitchController>();

        foreach (SwitchController sw in allSwitches)
        {
            if (sw.linkedDevice == device)
                return sw;
        }
        return null;
    }
}