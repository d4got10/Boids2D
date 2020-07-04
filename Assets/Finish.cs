using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public int Score { get; private set; }

    public Action<int> OnScoreHasChanged;

    public void IncreaseScore()
    {
        Score++;
        OnScoreHasChanged?.Invoke(Score);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BoidEntity>())
        {
            IncreaseScore();
            collision.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(collision.gameObject, 0.5f);
        }
    }
}
