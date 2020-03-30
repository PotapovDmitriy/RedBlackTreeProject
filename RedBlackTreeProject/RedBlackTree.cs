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

        public Node GetRoot() => _root;

        public void InsertNode(int value)
        {
            if (FindNodeByValue(value).GetValue() != null)
            {
                return;
            }

            var currentNode = this._root;
            while (!currentNode.IsNil())
            {
                currentNode = value > currentNode.GetValue() ? currentNode.GetRight() : currentNode.GetLeft();
            }

            Node newNode;
            if (currentNode != _root)
            {
                currentNode.GetParent().AddChild(new Node(value, Color.Red, currentNode.GetParent()));
                newNode = currentNode.GetParent().GetLeft().GetValue() == value
                    ? currentNode.GetParent().GetLeft()
                    : currentNode.GetParent().GetRight();
                _allNodes.Add(newNode);
                FindRootNode();
                InsertCase1(newNode);
            }

        }

        private void FindRootNode()
        {
            foreach (var node in _allNodes.Where(node => node.GetParent() == null))
            {
                _root = node;
            }
        }

        public bool DeleteNode(int value)
        {
            
            var node = FindNodeByValue(value);
            _allNodes.Remove(node);
            return true;
        }

        public int CountOfNode() => _allNodes.Count();

        public IEnumerable<Node> GetAllNodes() => _allNodes;

        private Node FindNodeByValue(int value)
        {
            foreach (var node in _allNodes.Where(node => value == node.GetValue()))
            {
                return node;
            }

            return new Node(null, Color.Red);
        }

        private  Node GetGrandparent(Node node) =>
            node != null && node.GetParent() != null
                ? node.GetParent().GetParent()
                : null;


        private Node GetUncle(Node node)
        {
            var g = GetGrandparent(node);
            if (g == null)
                return null;
            return node.GetParent() == g.GetLeft()
                ? g.GetRight()
                : g.GetLeft();
        }

        private Node GetBrother(Node node) =>
            node == node.GetParent().GetLeft()
                ? node.GetParent().GetRight()
                : node.GetParent().GetLeft();

        private void LeftRotation(Node node)
        {
            var pivot = node.GetRight();
            pivot.SetParent(node.GetParent());
            if (node.GetParent() != null)
                if (node.GetParent().GetLeft() == node)
                    node.GetParent().SetLeft(pivot);
                else
                    node.GetParent().SetRight(pivot);

            node.SetRight(pivot.GetLeft());
            if (pivot.GetLeft() != null)
                pivot.GetLeft().SetParent(node);

            node.SetParent(pivot);
            pivot.SetLeft(node);
        }

        private void RightRotation(Node node)
        {
            var pivot = node.GetLeft();
            pivot.SetParent(node.GetParent());
            if (node.GetParent() != null)
                if (node.GetParent().GetLeft() == node)
                    node.GetParent().SetLeft(pivot);
                else
                    node.GetParent().SetRight(pivot);

            node.SetLeft(pivot.GetRight());
            if (pivot.GetRight() != null)
                pivot.GetRight().SetParent(node);

            node.SetParent(pivot);
            pivot.SetRight(node);
        }

        private void InsertCase1(Node node)
        {
            if (node.GetParent() == null)
                node.CheckColor();
            else
                InsertCase2(node);
        }

        private void InsertCase2(Node node)
        {
            if (node.GetParent().GetColor() == Color.Black)
                return;
            InsertCase3(node);
        }

        private void InsertCase3(Node node)
        {
            var uncle = GetUncle(node);
            if (uncle != null && uncle.GetColor() == Color.Red)
            {
                node.GetParent().CheckColor();
                uncle.CheckColor();
                var g = GetGrandparent(node);
                g.CheckColor();
                InsertCase1(g);
            }
            else
                InsertCase4(node);
        }

        private void InsertCase4(Node node)
        {
            var g = GetGrandparent(node);
            if (node == node.GetParent().GetRight() && node.GetParent() == g.GetLeft())
            {
                LeftRotation(node.GetParent());
                node = node.GetLeft();
            }
            else if (node == node.GetParent().GetLeft() && node.GetParent() == g.GetRight())
            {
                RightRotation(node.GetParent());
                node = node.GetRight();
            }

            InsertCase5(node);
        }

        private void InsertCase5(Node node)
        {
            var g = GetGrandparent(node);
            node.GetParent().CheckColor();
            g.CheckColor();
            if (node == node.GetParent().GetLeft() && node.GetParent() == g.GetLeft())
                RightRotation(g);
            else
                LeftRotation(g);
        }
    }
}