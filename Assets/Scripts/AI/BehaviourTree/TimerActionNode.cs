using System;
using UnityEngine;

public class TimerActionNode : BTNode
{
    private Func<bool> _action;
    private float _duration;
    private float _startTime;
    private bool _isRunning;

    public TimerActionNode(Func<bool> action, float duration)
    {
        _action = action;
        _duration = duration;
    }

    public override EBTNodeState Execute()
    {
        if (!_isRunning)
        {
            _startTime = Time.time;
            _isRunning = true;
            return EBTNodeState.RUNNING;
        }

        Debug.Log(FormatterUtils.TimeFormatter(Time.time - _startTime));

        if (Time.time - _startTime < _duration) return EBTNodeState.RUNNING;

        _isRunning = false;
        return _action.Invoke() ? EBTNodeState.SUCCESS : EBTNodeState.FAILURE;
    }
}