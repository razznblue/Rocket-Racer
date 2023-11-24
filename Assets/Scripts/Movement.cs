using UnityEngine;

public class Movement : MonoBehaviour
{

  Rigidbody rb;
  AudioSource audioSource;
  [SerializeField] AudioClip mainEngine;

  [SerializeField] ParticleSystem mainThrustParticle;
  [SerializeField] ParticleSystem rightRocketParticle;
  [SerializeField] ParticleSystem leftRocketParticle;

  public float speed = 1f;
  public float rotationSpeed = 1f;

  void Start() {
    rb = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();
  }

  void Update() {
    ProcessThrust();
    ProcessRotation();
  }

  void ProcessThrust() {  
    if (Input.GetKey(KeyCode.Space)) {
      StartThrust();
    } else if (Input.GetKeyUp(KeyCode.Space)) {
      StopThrust();
    }
  }

  void ProcessRotation() {
    if (Input.GetKey(KeyCode.D)) {
      DoRotateRight();
    } else if (Input.GetKey(KeyCode.A)) {
      DoRotateLeft();
    } else {
      StopParticle(rightRocketParticle);
      StopParticle(leftRocketParticle);
    }
  }

  void DoRotateRight() {
    RotateRocket(-Vector3.forward);
    PlayParticle(rightRocketParticle);
  }

  void DoRotateLeft() {
    RotateRocket(Vector3.forward);
    PlayParticle(leftRocketParticle);
  }

  void StartThrust() {
    rb.AddRelativeForce(speed * Time.deltaTime * Vector3.up);
    PlayRocketThrustSound();
    PlayParticle(mainThrustParticle);
  }

  void StopThrust() {
    StopRocketThrustSound();
    StopParticle(mainThrustParticle);
  }
  
  // Helper Methods
  private void RotateRocket(Vector3 direction) {
    rb.freezeRotation = true; // Before manually rotating, freeze the Unity physics system rotation on object collision
    transform.Rotate(rotationSpeed * Time.deltaTime * direction);
    rb.freezeRotation = false;
  }

  private void PlayRocketThrustSound() {
    if (!audioSource.isPlaying) {
      audioSource.PlayOneShot(mainEngine);
    }
  }

  private void StopRocketThrustSound() {
    if (audioSource.isPlaying) {
      audioSource.Stop();
    }
  }

  private void PlayParticle(ParticleSystem particleSystem) {
      if (!particleSystem.isPlaying) {
        particleSystem.Play();
      }
  }

  private void StopParticle(ParticleSystem particleSystem) {
    particleSystem.Stop();
  }
 
}
