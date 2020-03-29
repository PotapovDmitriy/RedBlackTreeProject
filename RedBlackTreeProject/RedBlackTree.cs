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
                _root = new Node(valueRoot, Color.Black);
                _allNodes.Add(_root);
            }

            public void InsertNode(int value)
            {
                var node = new Node(value, Color.Red);
                _allNodes.Add(node);
            }

            public void DeleteNode(int value)
            {
                var node = FindNodeByValue(value);
                _allNodes.Remove(node);
            }
            public int CountOfNode() => _allNodes.Count();

            public List<Node> GetAllNodes() => _allNodes;

            private Node FindNodeByValue(int value)
            {
                foreach (var node in _allNodes)
                {
                    if (value == node.GetValue())
                    {
                        return node;
                    }
                }

                return new Node(0, Color.Red);
            }
    }
}