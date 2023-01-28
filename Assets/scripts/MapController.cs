using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private float smoothTime;

    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;

    [SerializeField] private MapNode[] nodes; // These must be added manually
    [SerializeField] private int currentNodeIndex = 0;

    private Vector3[] positionNodes;

    void Start()
    {
        // NOTE Map nodes must be assigned before runtime!
        // Generate position nodes, we will interpolate to these
        positionNodes = new Vector3[nodes.Length];

        for (int i = 0; i < nodes.Length; ++i)
        {
            positionNodes[i] = nodes[i].transform.position;
        }
    }

    void Update()
    {
        // Player's position always moves to the next node
        transform.position = Vector3.Lerp(transform.position, nodes[currentNodeIndex].transform.position, smoothTime * Time.deltaTime);

        // Get inputs and move node
        if (Input.GetKeyDown(left) && currentNodeIndex - 1 >= 0)
            currentNodeIndex--;

        else if (Input.GetKeyDown(right) && currentNodeIndex + 1 < nodes.Length)
            currentNodeIndex++;
    }
}
