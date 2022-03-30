using UnityEngine;
using System.Collections;

public class ShowHideHandy : MonoBehaviour
{
    public Mouse_Look _look;
    public AccessHM _hm;
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab;
    [SerializeField] private GameObject uiContainer = null;
    [SerializeField] private bool show = false;
    private bool aniShow = false;

    public bool Show
    {
        get => show;
    }
    public bool AniShow
    {
        get => aniShow;
    }

    // Start is called before the first frame update
    private void Start()
    {


        show = false;
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
        _look.AddCineComp(aniShow);
        _hm.AccessHandy(aniShow);
        uiContainer.GetComponent<Animator>().SetBool("retract", aniShow);
        CursorState(aniShow);
        yield return new WaitForSeconds(.1f);
        show = !show;


    }
}