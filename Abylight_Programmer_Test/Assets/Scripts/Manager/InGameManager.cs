using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{

    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private FlowManager _flowManager;

    /// <summary>
    /// Start. Ensures the exit button is properly linked
    /// </summary>
    void Start()
    {
        _flowManager = FlowManager._flowManager;
        exitButton.onClick.AddListener(_flowManager.GotoInit);
    }

}
