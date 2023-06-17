using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class HUBManager : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3f);

        AsyncOperation asyncLoad;
        if (!ElevatorManager.isPortal)
            asyncLoad = SceneManager.LoadSceneAsync(ElevatorManager.desireSceneName);
        else
            asyncLoad = SceneManager.LoadSceneAsync(ElevatorManager.desireSceneIndex);

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
                asyncLoad.allowSceneActivation = true;

            yield return null;
        }
    }
}
