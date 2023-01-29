using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour {
    // Map handler is a singleton
    public static MapHandler mapHandler;

    public Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Vector2Int size;

    private void Awake() {
        if (mapHandler == null) {
            mapHandler = this;
        }

        else {
            Destroy(gameObject);
        }

        // Also find nodes on start
        // Add all nodes to a hashmap containing node data
        Node[] nodes = FindObjectsOfType<Node>();
        for (int i = 0; i < nodes.Length; ++i) {
            grid.Add(nodes[i].nodePosition, nodes[i]);
        }
    }
}
