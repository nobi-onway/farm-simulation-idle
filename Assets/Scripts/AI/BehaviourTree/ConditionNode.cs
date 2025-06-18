using System;

public class ConditionNode : BTNode
{
    private Func<bool> _condition;

    public ConditionNode(Func<bool> condition)
    {
        _condition = condition;
    }

    public override EBTNodeState Execute()
    {
        return _condition.Invoke() ? EBTNodeState.SUCCESS : EBTNodeState.FAILURE;
    }
}