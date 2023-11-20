using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{

    // Add debug keys to go back and forth between levels (Z and X keys)
    void Update()
    {
        ToggleLevel();
    }

    private void ToggleLevel() {
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      int nextSceneIndex;
      if (Input.GetKey(KeyCode.Z)) {
        nextSceneIndex = currentSceneIndex == 0 ? currentSceneIndex : currentSceneIndex - 1;
        SceneManager.LoadScene(nextSceneIndex);
      } else if (Input.GetKey(KeyCode.X)) {
        nextSceneIndex = currentSceneIndex + 1;
        int totalSceneCount = SceneManager.sceneCountInBuildSettings;
        if (nextSceneIndex == totalSceneCount) {
          nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex); 
      }
    }
}
