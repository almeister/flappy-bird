using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
  public GameObject pipesRecyclePoint;

  public float yTopPipeMax;
  public float yTopPipeMin;

  public float yBottomPipeMax;
  public float yBottomPipeMin;

  public float pipeSpeed = 5;
  public float horizontalSpacing = 5;

  void Start()
  {
    foreach (Transform child in transform)
    {
      PlacePipesVertically(child);
    }
  }

  void Update()
  {
    foreach (Transform child in transform)
    {
      Debug.Log($"Child name: {child.name}");

      child.localPosition -= new Vector3(0, 0, pipeSpeed * Time.deltaTime);

      if (child.position.z <= pipesRecyclePoint.transform.position.z)
      {
        child.localPosition += transform.childCount * new Vector3(0, 0, horizontalSpacing);
        PlacePipesVertically(child);
      }
    }
  }

  private void PlacePipesVertically(Transform child)
  {
    PlacePipeVertically("pipes/Pipe_T", child.gameObject, yTopPipeMin, yTopPipeMax);
    PlacePipeVertically("pipes/Pipe_B", child.gameObject, yBottomPipeMin, yBottomPipeMax);
  }

  private void PlacePipeVertically(string pipeName, GameObject pipeContainer, float yMin, float yMax)
  {
    float yPipe = Random.Range(yMin, yMax);
    Vector3 pipePosition = new Vector3(0, yPipe, 0);

    Transform pipe = pipeContainer.transform.Find(pipeName);
    if (pipe == null)
    {
      Debug.LogError($"Could not find a pipe with name {pipeName}");
    }

    pipe.localPosition = pipePosition;
  }

}
