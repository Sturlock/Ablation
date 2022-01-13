using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ModelViewer : EditorWindow
{
    Camera cam;
    RenderTexture renderTexture;
    GameObject gameObject;
    [SerializeField]
    GameObject viewableModel;
    

    static ModelViewer window;
    [MenuItem("Window/My Window")]
    public static void ShowWindow()
    {
        window = GetWindow(typeof(ModelViewer)) as ModelViewer;
        if (window != null)
        {
            window.autoRepaintOnSceneChange = true;
            window.Show();
            window.InitCam();
        }
        else
        {
            Debug.Log("No Window");
        }
        
    }

    private void Awake()
    {
        renderTexture = new RenderTexture((int)position.width, (int)position.height, (int)RenderTextureFormat.ARGB32);
    }
    void InitCam()
    {
        gameObject = new GameObject("Model Viewer Camera");
        gameObject.transform.position = new Vector3(0f, 0f, GameObject.FindGameObjectWithTag("MainCamera").transform.position.z);
        gameObject.AddComponent<Camera>();

        cam = gameObject.GetComponent<Camera>();
        cam.cameraType = CameraType.Preview;
        cam.backgroundColor = Color.green;
        cam.enabled = false;
    }

    private void Update()
    {
        if (cam != null) 
        {
            cam.targetTexture = renderTexture;
            cam.Render();
            cam.targetTexture = null;
        }
        else
        {
            Debug.Log("Cam Null");
        }
        if(renderTexture.width != position.width || renderTexture.height != position.height)
        {
            renderTexture = new RenderTexture((int)position.width, (int)position.height, (int)RenderTextureFormat.ARGB32);
        }
    }
    private void OnGUI()
    {
        GUILayout.Label("Model Viewer", EditorStyles.boldLabel);
        viewableModel = EditorGUILayout.ObjectField("Model", viewableModel, typeof(GameObject), true) as GameObject;
        GUI.DrawTexture(new Rect(0f, 0f, position.width, position.width), renderTexture);
    }
}
