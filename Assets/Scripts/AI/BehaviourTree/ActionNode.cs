using System;

public class ActionNode : BTNode
{
    private Func<bool> _action;

    public ActionNode(Func<bool> action) => _action = action;

    public override EBTNodeState Execute()
    {
        return _action.Invoke() ? EBTNodeState.SUCCESS : EBTNodeState.FAILURE;
    }
}