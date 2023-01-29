using UnityEngine;

[ExecuteInEditMode]
public class Node : MonoBehaviour {
    const int yOffset = 1;

    public Vector2Int nodePosition;
    public Node exploredFrom;
    public bool isWall = false;

    private void Update() {
        // Round position of cube to nearest int
        nodePosition = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        transform.position = new Vector3(nodePosition.x, yOffset, nodePosition.y);
    }
}
