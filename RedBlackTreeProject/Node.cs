namespace RedBlackTreeProject
{
    public class Node
    {
        private Color _color;
        private readonly int _value;
        private Node _left;
        private Node _right;
        private bool _isNil;
        private Node _parent;

        public Node(int value, Color color, Node parent = null, Node left = null, Node right = null,
            bool isNil = true)
        {
            _color = color;
            _left = left;
            _right = right;
            _isNil = isNil;
            _parent = parent;
            _value = value;
        }

        public Node GetLeft() => _left;

        public Node GetRight() => _right;

        public Color GetColor() => _color;

        public bool IsNil() => _isNil;

        public int GetValue() => _value;

        public void AddChild(int value)
        {
        }
    }
}