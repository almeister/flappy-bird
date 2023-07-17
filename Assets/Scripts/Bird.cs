using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
  private CharacterController controller;
  private Animator animator;

  private const float gravity = 15f;
  private const float flappingVelocity = 8f;
  private Vector3 velocity = new Vector3(0, 0, 0);

  public GameObject lookAtPoint;


  // Start is called before the first frame update
  void Start()
  {
    controller = GetComponent<CharacterController>();
    animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    // Make bird fall
    velocity.y -= gravity * Time.deltaTime;

    // On pressing spacebar, make bird fly
    if (Input.GetKeyDown("space"))
    {
      velocity.y = flappingVelocity;

      // Flap bird's wings
      animator.Play("Wings|flapping");

      // Lift bird head when flapping
      transform.LookAt(lookAtPoint.transform);
      StartCoroutine(WaitThenResetRotation());
    }

    controller.Move(velocity * Time.deltaTime);
  }

  IEnumerator WaitThenResetRotation()
  {
    yield return new WaitForSeconds(0.5f);

    transform.eulerAngles = new Vector3(0, 0, 0);
  }
}
