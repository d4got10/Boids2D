using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoidSavedUI : MonoBehaviour
{
    [SerializeField] private BoidEntityController _boidController;
    [SerializeField] private Finish _finish;

    [SerializeField] private Text _text;

    public UnityEvent OnLose;
    public UnityEvent OnWin;

    private int _score;
    private int boidCount = 9;

    private void Start()
    {
        _finish.OnScoreHasChanged += UpdateScore;
        BoidEntity.OnAnyBoidBeenDestroyed += CheckGameState;
    }

    public void UpdateScore(int score)
    {
        _text.text = $"Score: {score}";
        _score = score;
    }

    public void CheckGameState()
    {
        boidCount--;
        if (boidCount == 0)
        {
            if (_score >= 3)
            {
                OnWin?.Invoke();
            }
            else
            {
                OnLose?.Invoke();
            }
        }
    }
}
