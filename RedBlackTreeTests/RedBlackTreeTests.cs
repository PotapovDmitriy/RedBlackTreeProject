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
    }
}