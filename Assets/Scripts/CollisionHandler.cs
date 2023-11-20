using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

  // PARAMETERS - for tuning, typically set in the unity editor
  [SerializeField] float levelReloadDelay = 2f;
  [SerializeField] AudioClip successSound;
  [SerializeField] AudioClip explosionSound;

  // Particles
  [SerializeField] ParticleSystem successParticle;
  [SerializeField] ParticleSystem explosionParticle;

  bool collisionDisabled = false; 

  // CACHE - references
  AudioSource audioSource;

  // STATE - private class variable
  bool isTransitioning = false;

  void Start() {
    audioSource = GetComponent<AudioSource>();
  }

  void Update() {
    AddDebugListeners();
  }

  void AddDebugListeners() {
    if (Input.GetKey(KeyCode.C)) {
      Debug.Log("Toggling collisionDisabled");
      collisionDisabled = !collisionDisabled; // Toggle on/off collisions for testing purposes
    }
  }

  private void OnCollisionEnter(Collision other) {
    if (isTransitioning || collisionDisabled) return;
    switch (other.gameObject.tag) {
      case "Friendly": 
      case "Start":
        HandleFriendlyCollision();
        break;
      case "Finish":
        HandleFinishCollision();
        break;
      default: 
        HandleUnfriendlyCollision();
        break;
    }
  }

  private void HandleFriendlyCollision() {
    Debug.Log("we collected some fuel!");   
  }
  private void HandleUnfriendlyCollision() {
    Debug.Log("Aaaaaargh! We Blew Up!");
    StartCrashSequence();
  }
  private void HandleFinishCollision() {
    Debug.Log("Level Complete!"); 
    StartLoadNextlevelSequence();
  }

  // Restarting the Current Level
  private void StartCrashSequence() {
    // Remove movement controls on rocket
    isTransitioning = true;
    audioSource.Stop();
    PlayExplosionSound();
    explosionParticle.Play();
    GetComponent<Movement>().enabled = false;
    Invoke(nameof(ReloadLevel), levelReloadDelay);
  }

  private void ReloadLevel() {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }

  // Loading the Next Level
  private void StartLoadNextlevelSequence() {
    isTransitioning = true;
    audioSource.Stop();
    PlaySuccessSound();
    successParticle.Play();
    GetComponent<Movement>().enabled = false;
    Invoke(nameof(LoadNextLevel), levelReloadDelay);
  }

  private void LoadNextLevel() {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;
    int totalSceneCount = SceneManager.sceneCountInBuildSettings;
    if (nextSceneIndex == totalSceneCount) {
      nextSceneIndex = 0;
    }
    SceneManager.LoadScene(nextSceneIndex);
  }

  private void PlaySuccessSound() {
    if (!audioSource.isPlaying) {
      audioSource.PlayOneShot(successSound);
    }
  }

  private void PlayExplosionSound() {
    if (!audioSource.isPlaying) {
      audioSource.PlayOneShot(explosionSound);
    }
  }

}
