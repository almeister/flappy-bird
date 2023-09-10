using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bird : MonoBehaviour
{
  [SerializeField] GameObject lookAtPoint;
  [SerializeField] GameObject pipesFactory;
  [SerializeField] GameObject scenery;

  private Animator _animator;

  private const float _gravity = 15f;
  private const float _velocityBoost = 8f;
  private Vector3 _velocity = new Vector3(0, 0, 0);

  private bool _birdAlive = true;

  void Start()
  {
    _animator = GetComponent<Animator>();
  }

  void Update()
  {
    // Make bird fall
    _velocity.y -= _gravity * Time.deltaTime;

    // On pressing spacebar, make bird fly
    if (_birdAlive && Input.GetKeyDown("space"))
    {
      _velocity.y = _velocityBoost;

      // Flap bird's wings
      _animator.SetTrigger("Flap");
    }

    transform.Translate(_velocity * Time.deltaTime, Space.World);
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

  private void OnTriggerEnter(Collider other)
  {
    Debug.Log($"Bird crashed into: {other.name}");
    if (other.tag == "Pipe")
    {
      // Drop bird
      _animator.enabled = false;
      _birdAlive = false;
      _velocity = Vector3.zero;
      transform.Translate(new Vector3(2f, 0, 0), Space.World);
      transform.Rotate(0, 0, -45f);

      // Stop pipes and scenery movement
      pipesFactory.GetComponent<Pipes>().stop();
      scenery.GetComponent<Scenery>().stop();
    }
  }
}

