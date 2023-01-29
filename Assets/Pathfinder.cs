using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {
    public Vector2Int position;
    public Node target;
    
    public float tickSpeed;
    public float smoothTime;

    private readonly Vector2Int[] directions = {
        new Vector2Int(0, -1),
        new Vector2Int(0, 1),
        new Vector2Int(-1, 0),
        new Vector2Int(1, 0)
    };

    private int pathIndex = 0;
    private float ticks = 0;
    private bool hasMadePath = false;

    private List<Node> path;

    private void Start() {
        SetPosition();
    }

    private void Update() {
        // Apply lerp position
        ApplyPosition();
        
        // Once target is not null and we know we have not made a path, make a path!
        if (target != null && !hasMadePath) {
            path = BFS(position, target.nodePosition);

            print("Made path! Positions:");
            for (int i = 0; i < path.Count; ++i)
                print(path[i].nodePosition);
            
            hasMadePath = true;
        }

        // If has made path, move according to tickspeed
        else if (hasMadePath) {
            ticks += Time.deltaTime;
            if (ticks >= tickSpeed) {
                if (position != target.nodePosition) {
                    pathIndex++;
                    position = path[pathIndex].nodePosition;
                }

                ticks = 0;
            }
        }
    }
    
    private List<Node> BFS(Vector2Int startPos, Vector2Int endPos) {
        Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>(MapHandler.mapHandler.grid);
        List<Node> explored = new List<Node>();

        Node start = grid[startPos];
        Node end = grid[endPos];

        explored.Add(start);
        
        while (!explored.Contains(end)) {
            // Carry out breadth first search
            bool foundNewNeighbor = false;
            for (int i = 0; i < explored.Count; ++i) {
                for (int j = 0; j < directions.Length; ++j) {
                    int x = explored[i].nodePosition.x + directions[j].x;
                    int y = explored[i].nodePosition.y + directions[j].y;

                    Vector2Int neighborPosition = new Vector2Int(x, y);
                    
                    if (!grid.ContainsKey(neighborPosition)) {
                        print("No key! Continuing");
                        continue;
                    }

                    Node neighbor = grid[neighborPosition];
                    if (!neighbor.isWall && !explored.Contains(neighbor)) {
                        if (!foundNewNeighbor) {
                            foundNewNeighbor = true;
                        }

                        explored.Add(neighbor);
                        neighbor.exploredFrom = explored[i];
                    }
                }
            }
        
            if (!foundNewNeighbor) {
                print("No paths exist!");
                return null;
            }
        }

        // Generate path
        List<Node> path = new List<Node>();

        Node current = end;
        while (current != start) {
            path.Add(current);
            current = current.exploredFrom;
        }

        path.Add(start);
        path.Reverse();

        // Clear data from explored nodes
        for (int i = 0; i < explored.Count; ++i) {
            explored[i].exploredFrom = null;
        }

        return path;
    }

    private void SetPosition() {
        // Make follow int coordinates
        position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        transform.position = new Vector3(position.x, 1, position.y);
    }

    private void ApplyPosition() {
        // Just update position to follow rounded coords
        transform.position = Vector3.Lerp(transform.position, new Vector3(position.x, 1, position.y), smoothTime * Time.deltaTime);
    }
}
