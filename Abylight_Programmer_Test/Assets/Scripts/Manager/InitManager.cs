using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitManager : MonoBehaviour
{

    [SerializeField]
    private Button menuButton;
    [SerializeField]
    private Button inGameButton;
    [SerializeField]
    private FlowManager _flowManager;

    /// <summary>
    /// Start. Ensures the exit button is properly linked
    /// </summary>
    void Start()
    {
        _flowManager = FlowManager._flowManager;
        menuButton.onClick.AddListener(_flowManager.GotoMenu);
        inGameButton.onClick.AddListener(_flowManager.GotoGame);
    }

}
