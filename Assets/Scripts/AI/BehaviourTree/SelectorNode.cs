using System.Collections.Generic;

public class SelectorNode : BTNode
{
    private List<BTNode> _children;

    public SelectorNode(params BTNode[] children)
    {
        _children = new(children);
    }

    public override EBTNodeState Execute()
    {
        foreach(BTNode child in _children)
        {
            EBTNodeState state = child.Execute();
            if (state != EBTNodeState.FAILURE) return state;
        }

        return EBTNodeState.FAILURE;
    }
}