using UnityEngine;
using System.Collections;

public class ShowHideUI_Canvas : MonoBehaviour
{
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab;
    [SerializeField] private GameObject uiContainer = null;
    [SerializeField] private bool show;
    private bool aniShow = false;

    public bool Show
    {
        get => show;
    }

    // Start is called before the first frame update
    private void Start()
    {
        aniShow = false;
        uiContainer = this.gameObject;
    }

    private void CursorState(bool bShow)
    {
        if (bShow)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = bShow;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = bShow;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            StartCoroutine(Toggle());
        }
    }

    public IEnumerator Toggle()
    {
        aniShow = !aniShow;

        show = !show;
        //FindObjectOfType<Mouse_Look>().AddCineComp(retract);
        uiContainer.GetComponent<Animator>().SetBool("retract", aniShow);
        CursorState(aniShow);
        yield return new WaitForSeconds(.1f);
        show = !show;


    }
}