public enum EBTNodeState { RUNNING, SUCCESS, FAILURE }

public abstract class BTNode
{
    public abstract EBTNodeState Execute();
}