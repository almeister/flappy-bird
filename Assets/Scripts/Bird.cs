using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
  public const float gravity = 15f;
  public GameObject lookUp;

  private CharacterController controller;
  private Animator animator;
  private Vector3 velocity = new Vector3(0, 0, 0);
  private const float flappingVelocity = 8f;
  private const float flappingPeriod = 0.5f;

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
      velocity.y = flappingVelocity;
      animator.Play("Wings|flapping");

      StartCoroutine(WaitThenPlayDefaultAnim());
      gameObject.transform.LookAt(lookUp.transform);
    }

    controller.Move(velocity * Time.deltaTime);
  }

  private IEnumerator WaitThenPlayDefaultAnim()
  {
    yield return new WaitForSeconds(flappingPeriod);
    animator.Play("Wings|flying");
    transform.eulerAngles = new Vector3(0, 0, 0);
  }
}

