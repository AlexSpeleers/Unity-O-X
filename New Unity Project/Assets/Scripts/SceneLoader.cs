using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private int sceneIndex;
    public Button button;
    private Scene scene;

    private void Awake()
    {
       // SetSceneIndex();
        button.onClick.AddListener(LoadScene);
    }
    //void SetSceneIndex()
    //{
    //    //scene.buildIndex == 0 ? (sceneIndex = 1): (sceneIndex = 0);
    //    if (scene.buildIndex == 0)
    //    {
    //        sceneIndex = 1;
    //    }
    //    else
    //    {
    //        sceneIndex = 0;
    //    }
    //}

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
