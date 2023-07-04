using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
  public const float gravity = 15f;
  public GameObject lookUp;

  private CharacterController controller;
  private Animator animator;
  private Vector3 velocity = new Vector3(0, 0, 0);

  void Start()
  {
    controller = gameObject.GetComponent<CharacterController>();
    animator = gameObject.GetComponent<Animator>();
  }

  void Update()
  {
    velocity.y -= gravity * Time.deltaTime;

    if (Input.GetKeyDown("space"))
    {
      velocity.y = 8f;
      animator.Play("Wings|flapping");
      StartCoroutine(WaitThenPlayDefaultAnim());
      gameObject.transform.LookAt(lookUp.transform);
    }

    controller.Move(velocity * Time.deltaTime);
  }

  private IEnumerator WaitThenPlayDefaultAnim()
  {
    yield return new WaitForSeconds(0.5f);
    animator.Play("Wings|flying");
    gameObject.transform.LookAt(new Vector3(0, transform.position.y, transform.position.z + 10));
  }
}

