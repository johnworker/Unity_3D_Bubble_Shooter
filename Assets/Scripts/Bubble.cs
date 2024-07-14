using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public int colorIndex;
    public bool isStuck = false;
    public Vector2Int gridPosition;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isStuck && (collision.gameObject.CompareTag("Bubble") || collision.gameObject.CompareTag("Ceiling")))
        {
            isStuck = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().isKinematic = true;

            // 計算並設置網格位置
            gridPosition = gameManager.CalculateGridPosition(transform.position);
            transform.position = gameManager.GetWorldPosition(gridPosition);

            // 通知遊戲管理器檢查匹配
            gameManager.CheckForMatches(this);
        }
    }
}