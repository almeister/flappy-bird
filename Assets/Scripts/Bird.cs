using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
  public GameObject lookAtPoint;

  private CharacterController _controller;
  private Animator _animator;

  private const float _gravity = 15f;
  private const float _velocityBoost = 8f;
  private Vector3 _velocity = new Vector3(0, 0, 0);

  void Start()
  {
    _controller = GetComponent<CharacterController>();
    _animator = GetComponent<Animator>();
  }

  void Update()
  {
    // Make bird fall
    _velocity.y -= _gravity * Time.deltaTime;

    // On pressing spacebar, make bird fly
    if (Input.GetKeyDown("space"))
    {
      _velocity.y = _velocityBoost;

      // Flap bird's wings
      _animator.SetTrigger("Flap");
    }

    _controller.Move(_velocity * Time.deltaTime);
  }

  void OnTriggerEnter(Collider other)
  {
    Debug.Log("Bird collision.");

    // Check if hit a pipe
    if (other.CompareTag("pipe"))
    {
      // Kill bird
      Debug.Log("Birdie done died.");

    }

  }

  public void OnFlappingStart()
  {
    // Tilt bird when flapping
    transform.LookAt(lookAtPoint.transform);
  }

  public void OnFlappingFinish()
  {
    // Untilt bird after flapping finishes
    transform.eulerAngles = new Vector3(0, 0, 0);
  }
}

