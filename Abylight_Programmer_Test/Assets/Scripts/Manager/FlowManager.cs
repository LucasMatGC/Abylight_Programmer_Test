using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowManager : MonoBehaviour
{

    //Singleton reference to the FlowManager
    private static FlowManager _flowManager;

    /// <summary>
    /// Start.
    /// </summary>
    void Start()
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

        SceneManager.LoadScene(Constants.InitScene);

    }

    /// <summary>
    /// Loads the Menu scene.
    /// </summary>
    public void GotoMenu()
    {

        SceneManager.LoadScene(Constants.MenuScene);

    }

    /// <summary>
    /// Loads the InGame scene.
    /// </summary>
    public void GotoGame()
    {

        SceneManager.LoadScene(Constants.GameScene);

    }
}
