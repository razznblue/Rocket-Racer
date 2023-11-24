using UnityEngine;

public class Quit : MonoBehaviour
{
    void Update() {
      AddQuitListener();
    }

    private void AddQuitListener() {
      if (Input.GetKey(KeyCode.Escape)) {
        Debug.Log("Exiting Game");
        Application.Quit();
      }
    }
}
