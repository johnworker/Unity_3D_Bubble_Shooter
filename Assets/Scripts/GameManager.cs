using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] bubblePrefabs;
    public int gridWidth = 8;
    public int gridHeight = 10;
    private Bubble[,] bubbleGrid;
    public float bubbleSpacing = 0.5f;
    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        InitializeGrid();
        UpdateScoreDisplay();
    }

    void InitializeGrid()
    {
        bubbleGrid = new Bubble[gridWidth, gridHeight];
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                if (y < 5) // 只填充上半部分
                {
                    Vector3 position = GetWorldPosition(new Vector2Int(x, y));
                    int randomColor = Random.Range(0, bubblePrefabs.Length);
                    GameObject bubbleObj = Instantiate(bubblePrefabs[randomColor], position, Quaternion.identity);
                    Bubble bubble = bubbleObj.GetComponent<Bubble>();
                    bubble.colorIndex = randomColor;
                    bubble.gridPosition = new Vector2Int(x, y);
                    bubbleGrid[x, y] = bubble;
                }
            }
        }
    }

    public Vector3 GetWorldPosition(Vector2Int gridPos)
    {
        float xPos = gridPos.x * bubbleSpacing;
        float yPos = gridPos.y * bubbleSpacing;
        if (gridPos.y % 2 == 1) xPos += bubbleSpacing / 2; // 奇數行偏移
        return new Vector3(xPos, yPos, 0);
    }

    public Vector2Int CalculateGridPosition(Vector3 worldPos)
    {
        int y = Mathf.RoundToInt(worldPos.y / bubbleSpacing);
        float xOffset = (y % 2 == 1) ? bubbleSpacing / 2 : 0;
        int x = Mathf.RoundToInt((worldPos.x - xOffset) / bubbleSpacing);
        return new Vector2Int(x, y);
    }

    public void CheckForMatches(Bubble newBubble)
    {
        List<Bubble> matchingBubbles = new List<Bubble>();
        FloodFillMatch(newBubble.gridPosition, newBubble.colorIndex, matchingBubbles);

        if (matchingBubbles.Count >= 3)
        {
            foreach (Bubble bubble in matchingBubbles)
            {
                RemoveBubble(bubble);
            }
            score += matchingBubbles.Count * 10;
            UpdateScoreDisplay();
            CheckFloatingBubbles();
        }
    }

    void FloodFillMatch(Vector2Int pos, int colorIndex, List<Bubble> matches)
    {
        if (pos.x < 0 || pos.x >= gridWidth || pos.y < 0 || pos.y >= gridHeight)
            return;

        Bubble bubble = bubbleGrid[pos.x, pos.y];
        if (bubble == null || bubble.colorIndex != colorIndex || matches.Contains(bubble))
            return;

        matches.Add(bubble);

        // 檢查六個相鄰位置
        FloodFillMatch(pos + new Vector2Int(1, 0), colorIndex, matches);
        FloodFillMatch(pos + new Vector2Int(-1, 0), colorIndex, matches);
        FloodFillMatch(pos + new Vector2Int(0, 1), colorIndex, matches);
        FloodFillMatch(pos + new Vector2Int(0, -1), colorIndex, matches);
        FloodFillMatch(pos + new Vector2Int((pos.y % 2 == 0 ? -1 : 1), 1), colorIndex, matches);
        FloodFillMatch(pos + new Vector2Int((pos.y % 2 == 0 ? -1 : 1), -1), colorIndex, matches);
    }

    void RemoveBubble(Bubble bubble)
    {
        bubbleGrid[bubble.gridPosition.x, bubble.gridPosition.y] = null;
        Destroy(bubble.gameObject);
    }

    void CheckFloatingBubbles()
    {
        bool[,] visited = new bool[gridWidth, gridHeight];

        // 從頂行開始DFS
        for (int x = 0; x < gridWidth; x++)
        {
            if (bubbleGrid[x, gridHeight - 1] != null)
            {
                DFS(new Vector2Int(x, gridHeight - 1), visited);
            }
        }

        // 移除所有未訪問的泡泡
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                if (!visited[x, y] && bubbleGrid[x, y] != null)
                {
                    RemoveBubble(bubbleGrid[x, y]);
                    score += 20; // 額外分數for浮動泡泡
                }
            }
        }

        UpdateScoreDisplay();
    }

    void DFS(Vector2Int pos, bool[,] visited)
    {
        if (pos.x < 0 || pos.x >= gridWidth || pos.y < 0 || pos.y >= gridHeight || visited[pos.x, pos.y])
            return;

        if (bubbleGrid[pos.x, pos.y] == null)
            return;

        visited[pos.x, pos.y] = true;

        // 訪問六個相鄰位置
        DFS(pos + new Vector2Int(1, 0), visited);
        DFS(pos + new Vector2Int(-1, 0), visited);
        DFS(pos + new Vector2Int(0, 1), visited);
        DFS(pos + new Vector2Int(0, -1), visited);
        DFS(pos + new Vector2Int((pos.y % 2 == 0 ? -1 : 1), 1), visited);
        DFS(pos + new Vector2Int((pos.y % 2 == 0 ? -1 : 1), -1), visited);
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}