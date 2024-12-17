using System.Collections;
using System.Collections.Generic;
namespace BehaviorTree
{
    
    public enum NodeState
    {
        RUNNING,
        SUCCES,
        FAILURE
    }
    public class Node
    {
        protected NodeState state;

        public Node parent;
        protected List<Node> children;
    }


}  