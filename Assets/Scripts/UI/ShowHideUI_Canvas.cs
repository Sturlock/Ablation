using UnityEngine;

public class ShowHideUI_Canvas : MonoBehaviour
{
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab;
    [SerializeField] private GameObject uiContainer = null;
    [SerializeField] public bool retract = false;

    public bool Retract
    {
        get => retract;
    }

    // Start is called before the first frame update
    private void Start()
    {
        uiContainer = this.gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            Toggle();
        }
        if (retract)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void Toggle()
    {
        retract = !retract;
        FindObjectOfType<Mouse_Look>().AddCineComp(retract);
        uiContainer.GetComponent<Animator>().SetBool("retract", retract);
    }
}