using NUnit.Framework;
using RedBlackTreeProject;

namespace RedBlackTreeTests
{
    public class RedBlackTreeTests
    {


        [Test]
        public void CheckCountOfNodesAfterInsert()
        {
            var tree = new RedBlackTree(10);
            tree.InsertNode(4);
            Assert.AreEqual(2, tree.CountOfNode());
        }
        
        [Test]
        public void CheckCountOfNodesAfterDelete()
        {
            var tree = new RedBlackTree(10);
            tree.InsertNode(4);
            tree.InsertNode(12);
            tree.DeleteNode(4);
            Assert.AreEqual(2, tree.CountOfNode());
        }

        [Test]
        public void CheckRedColorNodesAfterInsert()
        {
            var tree = new RedBlackTree(12);
            tree.InsertNode(8);
            tree.InsertNode(17);
            tree.InsertNode(1);
            tree.InsertNode(11);
            tree.InsertNode(15);
            tree.InsertNode(25);

            foreach (var node in tree.GetAllNodes())
            {
                var res = true;
                if (node.GetColor() == Color.Red)
                {
                    res = node.GetLeft().IsNil() && node.GetRight().IsNil() ||
                                node.GetRight().GetColor() == Color.Black && node.GetLeft().GetColor() == Color.Black;
                }
                Assert.AreEqual(true, res, "Red parent can have only black child");
            }
        }
    }
}