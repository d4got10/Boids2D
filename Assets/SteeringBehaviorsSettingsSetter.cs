using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteeringBehaviorsSettingsSetter : MonoBehaviour
{
    [SerializeField] private BoidEntityController _controller;

    [SerializeField] private Slider _seekSlider;
    [SerializeField] private Slider _fleeSlider;

    private void Start()
    {
        _controller.SeekValue = _seekSlider.value;
        _controller.FleeValue = _fleeSlider.value;
    }

    public void OnChanged_Seek()
    {
        _controller.SeekValue = _seekSlider.value;
    }

    public void OnChanged_Flee()
    {
        _controller.FleeValue = _fleeSlider.value;
    }
}
