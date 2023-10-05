using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    [SerializeField]
    private FlowManager _flowManager;
    [SerializeField]
    private Image _progressBar;

    private float _targetFill;

    // Start is called before the first frame update
    void Start()
    {

        _flowManager = FlowManager._flowManager;
        
        AsyncLoadAsync();

    }

    private void OnLevelWasLoaded()
    {
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }

    async void AsyncLoadAsync()
    {

        _targetFill = 0f;
        _progressBar.fillAmount = 0f;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_flowManager._nextScene);
        asyncLoad.allowSceneActivation = false;

        do
        {
            await Task.Delay(100);
            _targetFill = asyncLoad.progress;

        } while (asyncLoad.progress < 0.9f);

        await Task.Delay(500);

        asyncLoad.allowSceneActivation = true;

    }

    void Update()
    {

        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _targetFill, 3 * Time.deltaTime);

    }
}
