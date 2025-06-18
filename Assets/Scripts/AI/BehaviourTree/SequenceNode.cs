using System.Collections.Generic;

public class SequenceNode : BTNode
{
    private List<BTNode> _children;

    public SequenceNode(params BTNode[] children)
    {
        _children = new(children);
    }

    public override EBTNodeState Execute()
    {
        foreach(BTNode child in _children)
        {
            EBTNodeState state = child.Execute();
            if (state != EBTNodeState.SUCCESS) return state;
        }

        return EBTNodeState.SUCCESS;
    }
}