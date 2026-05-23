using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [Header("Device Info")]
    public string deviceName;
    public bool isWastingDevice = true;
    public float interactRange = 3f;

    [Header("References")]
    public GameObject linkedDevice;  

    public bool isTurnedOff = false;
    private Camera playerCamera;

    [Header("Education")]
    public string educationTitle;
    public string educationFact;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (isTurnedOff) return;

        Ray ray = playerCamera.ScreenPointToRay(
            new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                if (Input.GetKeyDown(KeyCode.F))
                    Interact();
            }
        }
    }

   void Interact()
{
    isTurnedOff = true;

    if (linkedDevice != null)
    {
        Renderer rend = linkedDevice.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.EnableKeyword("_EMISSION");
            rend.material.SetColor("_EmissionColor", Color.green * 2f);
        }
    }

    if (HUDManager.Instance == null) return;

  
    HUDManager.Instance.UpdateScore(25);
    HUDManager.Instance.ShowFeedback("+25pts - device turned off!");
    HUDManager.Instance.DecreaseEnergy();
    HUDManager.Instance.ShowEducation(educationTitle, educationFact);

}
}