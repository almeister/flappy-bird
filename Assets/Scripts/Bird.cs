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
  private Quaternion lookingForwardRotation;
  private Quaternion lookingUpRotation;
  private const float flappingPeriod = 0.5f;
  private float flappingTimeElapsed = 0;

  void Start()
  {
    controller = gameObject.GetComponent<CharacterController>();
    animator = gameObject.GetComponent<Animator>();
    lookingForwardRotation = transform.rotation;
  }

  void Update()
  {
    velocity.y -= gravity * Time.deltaTime;

    if (Input.GetKeyDown("space"))
    {
      velocity.y = flappingVelocity;
      animator.Play("Wings|flapping");

      flappingTimeElapsed = 0f;
      // Get looking up rotation
      lookingUpRotation = Quaternion.LookRotation(lookUp.transform.position - transform.position);

      StartCoroutine(WaitThenPlayDefaultAnim());
      gameObject.transform.LookAt(lookUp.transform);
    }

    flappingTimeElapsed += Time.deltaTime;
    // SLERP between the quaternions
    transform.rotation = Quaternion.Slerp(lookingUpRotation, lookingForwardRotation, flappingTimeElapsed / flappingPeriod);

    controller.Move(velocity * Time.deltaTime);
  }

  private IEnumerator WaitThenPlayDefaultAnim()
  {
    yield return new WaitForSeconds(flappingPeriod);
    animator.Play("Wings|flying");
  }
}

