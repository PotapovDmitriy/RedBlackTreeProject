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
            if (currentNode == _root) return;
            currentNode.GetParent().AddChild(new Node(value, Color.Red, currentNode.GetParent()));
            newNode = currentNode.GetParent().GetLeft().GetValue() == value
                ? currentNode.GetParent().GetLeft()
                : currentNode.GetParent().GetRight();
            _allNodes.Add(newNode);
            FindRootNode();
            InsertCase1(newNode);

        }

        public bool FindRootNode()
        {
            var countOfRoot = 0;
            foreach (var node in _allNodes.Where(node => node.GetParent() == null))
            {
                countOfRoot++;
                _root = node;
            }

            return countOfRoot <= 1;
        }

        public bool DeleteNode(int value)
        {
            if (FindNodeByValue(value).GetValue() == null)
            {
                return false;
            }

            var deleteNode = FindNodeByValue(value);
            Node replaceNode;

            if (!deleteNode.GetLeft().IsNil() && !deleteNode.GetRight().IsNil())
            {
                replaceNode = deleteNode.GetLeft();
                
                while (!replaceNode.GetRight().IsNil())
                    replaceNode = replaceNode.GetRight();
                
                deleteNode.SetValue(replaceNode.GetValue());
                
                if (replaceNode.GetLeft().IsNil() && replaceNode.GetRight().IsNil())
                    DeleteWithoutChild(replaceNode);
                else
                    DeleteOneChild(replaceNode);
            }
            else if(deleteNode.GetLeft().IsNil() && deleteNode.GetRight().IsNil())
                DeleteWithoutChild(deleteNode);
            else
                DeleteOneChild(deleteNode);

            _allNodes.Remove(deleteNode);
            return true;
            
            var node = FindNodeByValue(value);
            node.GetLeft().SetParent(null);
            node.GetRight().SetParent(null);
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
                node.Recolor();
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
                node.GetParent().Recolor();
                uncle.Recolor();
                var g = GetGrandparent(node);
                g.Recolor();
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
            node.GetParent().Recolor();
            g.Recolor();
            if (node == node.GetParent().GetLeft() && node.GetParent() == g.GetLeft())
                RightRotation(g);
            else
                LeftRotation(g);
        }
        
        private void ReplaceNode(Node node, Node child)
        {
            child.SetParent(node.GetParent());
            if(node == node.GetParent().GetLeft())
                node.GetParent().SetLeft(child);
            else
                node.GetParent().SetRight(child);
        }

        private void DeleteOneChild(Node node)
        {
            var child = node.GetRight().IsNil() ? node.GetLeft() : node.GetRight();
            ReplaceNode(node, child);
            if (node.GetColor() != Color.Black) return;
            if (child.GetColor() == Color.Red)
                child.Recolor();
            else
                DeleteCase1(child);
        }
        
         private void DeleteWithoutChild(Node node)
        {
            if (node == _root)
            {
                _root = new Node(null, Color.Black);
                return;
            }
            var parent = node.GetParent();
            if(parent.GetLeft() == node)
                parent.SetLeft(new Node(null,Color.Black, parent));
            else
                parent.SetRight(new Node(null,Color.Black, parent));
        }

        private void DeleteCase1(Node node)
        {
            if (node.GetParent() == null)
                DeleteCase2(node);
        }
        
        private void DeleteCase2(Node node)
        {
            var n = GetBrother(node);

            if (n.GetColor() == Color.Red)
            {
                node.GetParent().Recolor();
                n.Recolor();
                if(node == node.GetParent().GetLeft())
                    LeftRotation(node.GetParent());
                else
                    RightRotation(node.GetParent());
            }
            DeleteCase3(node);
        }
        
        private void DeleteCase3(Node node)
        {
            var n = GetBrother(node);

            if (node.GetParent().GetColor() == Color.Black && n.GetColor() == Color.Black &&
                n.GetLeft().GetColor() == Color.Black && n.GetRight().GetColor() == Color.Black)
            {
                n.Recolor();
                DeleteCase1(node.GetParent());
            }
            else
                DeleteCase4(node);
        }
        
        private void DeleteCase4(Node node)
        {
            var n = GetBrother(node);

            if (node.GetParent().GetColor() == Color.Red && n.GetColor() == Color.Black &&
                n.GetLeft().GetColor() == Color.Black && n.GetRight().GetColor() == Color.Black)
            {
                n.Recolor();
                node.GetParent().Recolor();
            }
            else
                DeleteCase5(node);
        }
        
        private void DeleteCase5(Node node)
        {
            var n = GetBrother(node);

            if (n.GetColor() == Color.Black)
            {
                if (node == node.GetParent().GetLeft() && n.GetRight().GetColor() == Color.Black &&
                    n.GetLeft().GetColor() == Color.Red)
                {
                    n.Recolor();
                    n.GetLeft().Recolor();
                    RightRotation(n);
                }
                else if (node == node.GetParent().GetRight() && n.GetLeft().GetColor() == Color.Black &&
                         n.GetRight().GetColor() == Color.Red)
                {
                    n.Recolor();
                    n.GetRight().Recolor();
                    LeftRotation(n);
                }
            }
            DeleteCase6(node);
        }
        
        private void DeleteCase6(Node node)
        {
            var s = GetBrother(node);
            
            s.SetColor(node.GetParent().GetColor());
            node.GetParent().SetColor(Color.Black);
            if (node == node.GetParent().GetLeft())
            {
                s.GetRight().SetColor(Color.Black);
                LeftRotation(node.GetParent());
            }
            else
            {
                s.GetLeft().SetColor(Color.Black);
                RightRotation(node.GetParent());
            }
        }
    }
}