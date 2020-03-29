using System;
using System.Collections.Generic;
using System.Linq;

namespace RedBlackTreeProject
{
    public class RedBlackTree
    {
            private Node _root;
            private List<Node> _allNodes = new List<Node>();
     
    
            public RedBlackTree(int valueRoot)
            {
                _root = new Node(valueRoot);
            }
    
            public void InsertNode(int value){}
    
            public void DeleteNode(int value){}
            public int CountOfNode() => _allNodes.Count();
    }
}