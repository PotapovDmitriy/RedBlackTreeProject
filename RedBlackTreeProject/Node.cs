namespace RedBlackTreeProject
{
    public class Node
    {
        private int? _value;
        private Color _color;
        private Node _left;
        private Node _right;
        private Node _parent;
        private bool _flag;

        public Node(int? value, Color color, Node parent = null, Node left = null, Node right = null)
        {
            _value = value;
            _color = color;
            _left = value == null ? null : new Node(null, Color.Black, this);
            _right = value == null ? null : new Node(null, Color.Black, this);
            _parent = parent;
            _flag = false;
        }

        public Node GetLeft() => _left;

        public Node GetRight() => _right;

        public bool GetFlag() => _flag;

        public void SetFlag(bool flag)
        {
            _flag = flag;
        }

        public void SetLeft(Node node)
        {
            _left = node;
        }

        public void SetRight(Node node)
        {
            _right = node;
        }

        public Color GetColor() => _color;

        public bool IsNil() => _value == null;

        public int? GetValue() => _value;
        
        public void SetValue(int? value)
        {
            _value = value;
        }

        public Node GetParent() => _parent;

        public void SetParent(Node node)
        {
            _parent = node;
        }

        public void AddChild(Node node)
        {
            if (node.GetValue() >= _value)
            {
                _right = node;
            }
            else
            {
                _left = node;
            }
        }

        public void Recolor()
        {
            if (_parent == null) return;
            _color = _color == Color.Black ? Color.Red : Color.Black;
        }

        public void SetColor(Color color)
        {
            _color = color;
        }
    }
}