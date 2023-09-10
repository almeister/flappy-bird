using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Scenery : MonoBehaviour
{
  [SerializeField] float speed = 0f;
  [SerializeField] GameObject sceneryRecyclePoint;

  private Vector3 _initialPosition;
  private bool _movementEnabled = true;

  // Start is called before the first frame update
  void Start()
  {
    _initialPosition = transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    if (_movementEnabled)
    {
      transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
      if (transform.position.z <= sceneryRecyclePoint.transform.position.z)
      {
        transform.position = _initialPosition;
      }

    }
  }

  public void stop()
  {
    _movementEnabled = false;
  }
}
