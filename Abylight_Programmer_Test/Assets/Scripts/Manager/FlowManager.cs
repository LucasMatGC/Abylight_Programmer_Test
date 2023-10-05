using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowManager : MonoBehaviour
{

    //Singleton reference to the FlowManager
    public static FlowManager _flowManager;

    public string _nextScene = "";

    /// <summary>
    /// OnEnable.
    /// </summary>
    private void OnEnable()
    {

        if (_flowManager != null && _flowManager != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _flowManager = this;
        DontDestroyOnLoad(this);

    }

    /// <summary>
    /// Loads the Init scene.
    /// </summary>
    public void GotoInit()
    {

        _nextScene = Constants.InitScene;
        SceneManager.LoadScene(Constants.LoadingScreenScene);

    }

    /// <summary>
    /// Loads the Menu scene.
    /// </summary>
    public void GotoMenu()
    {

        _nextScene = Constants.MenuScene;
        SceneManager.LoadScene(Constants.LoadingScreenScene);

    }

    /// <summary>
    /// Loads the InGame scene.
    /// </summary>
    public void GotoGame()
    {

        _nextScene = Constants.GameScene;
        SceneManager.LoadScene(Constants.LoadingScreenScene);

    }
}
