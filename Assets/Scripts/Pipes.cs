using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
  public GameObject pipeContainerPrefab;
  public GameObject pipesRecyclePoint;

  public float yTopPipeMax;
  public float yTopPipeMin;

  public float yBottomPipeMax;
  public float yBottomPipeMin;

  public float pipeSpeed = 5;
  public float horizontalSpacing = 5;

  private int pipesCount = 5;

  private GameObject[] _pipeContainers;

  void Start()
  {
    CreatePipes();
  }

  void Update()
  {
    // For each pipe pair
    for (int i = 0; i < _pipeContainers.Length; i++)
    {
      // Move pipes towards player
      GameObject pipeContainer = _pipeContainers[i];
      pipeContainer.transform.localPosition -= new Vector3(0, 0, pipeSpeed * Time.deltaTime);

      // Recycle pipe if it has reached the off-screen recycle point
      if (pipeContainer.transform.position.z <= pipesRecyclePoint.transform.position.z)
      {
        pipeContainer.transform.localPosition += pipesCount * new Vector3(0, 0, horizontalSpacing);
      }
    }



  }

  private void CreatePipes()
  {
    // Create array of pipe pair containers
    _pipeContainers = new GameObject[pipesCount];

    Vector3 nextPipePos = new Vector3();
    for (int i = 0; i < _pipeContainers.Length; i++)
    {
      // Create a new pipe container GameObject from prefab
      _pipeContainers[i] = GameObject.Instantiate(pipeContainerPrefab);
      GameObject pipeContainer = _pipeContainers[i];

      // Set the PipesFactory to be it's parent
      pipeContainer.transform.SetParent(transform);

      // Set the horizontal location of the pipe behind the last one
      pipeContainer.transform.localPosition = nextPipePos;
      nextPipePos.z += horizontalSpacing;

      // Set the vertical spacing of the pipes randomly
      PlacePipeVertically("pipes/Pipe_T", pipeContainer, yTopPipeMin, yTopPipeMax);
      PlacePipeVertically("pipes/Pipe_B", pipeContainer, yBottomPipeMin, yBottomPipeMax);
    }
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
