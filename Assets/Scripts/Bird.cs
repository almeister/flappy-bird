using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
  public GameObject lookAtPoint;

  public float velocityBoost = 6f;

  private CharacterController _controller;
  private Animator _animator;

  private const float _gravity = 15f;
  private Vector3 _velocity = new Vector3(0, 0, 0);
  private bool _crashed = false;

  private GameObject _pipesFactory;

  void Start()
  {
    _controller = GetComponent<CharacterController>();
    _animator = GetComponent<Animator>();
    _pipesFactory = GameObject.Find("PipesFactory");
  }

  void Update()
  {
    // Make bird fall
    _velocity.y -= _gravity * Time.deltaTime;

    // On pressing spacebar, make bird fly
    if (!_crashed && Input.GetKeyDown("space"))
    {
      _velocity.y = velocityBoost;

      // Flap bird's wings
      _animator.SetTrigger("Flap");
    }

    _controller.Move(_velocity * Time.deltaTime);
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "pipe")
    {
      Debug.Log("Bird crashed.");
      _crashed = true;

      // Rotate bird downwards
      transform.eulerAngles = new Vector3(60f, 0f, 0f);

      // Drop bird
      _velocity.y = 0f;

      // Pause pipe movement
      _pipesFactory.GetComponent<Pipes>().StopMovement();
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

