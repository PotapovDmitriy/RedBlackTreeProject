using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
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

            var res = false;

            foreach (var node in tree.GetAllNodes())
            {
                if (node.GetColor() == Color.Red)
                {
                    res = node.GetLeft().IsNil() && node.GetRight().IsNil() ||
                          node.GetRight().GetColor() == Color.Black && node.GetLeft().GetColor() == Color.Black;
                }
            }

            Assert.AreEqual(true, res, "Red parent can have only black child");
        }

        [Test]
        public void CheckIsNilNodesColors()
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
                if (node.IsNil())
                {
                    res = node.GetColor() == Color.Black;
                }

                Assert.AreEqual(true, res, "Nil nodes can be only black color");
            }
        }


        [Test]
        public void CheckInsertWhenNodeExisting()
        {
            var tree = new RedBlackTree(12);
            tree.InsertNode(8);
            tree.InsertNode(17);
            tree.InsertNode(1);
            tree.InsertNode(8);
            Assert.AreEqual(4, tree.CountOfNode(), "In this implementation, the tree cannot contain 2 identical nodes");
        }

        [Test]
        public void CheckInsertIfParentAndUncleRed()
        {
            var tree = new RedBlackTree(13);
            tree.InsertNode(8);
            tree.InsertNode(17);
            tree.InsertNode(1);
            tree.InsertNode(11);
            tree.InsertNode(15);
            tree.InsertNode(25);
            tree.InsertNode(22);
            tree.InsertNode(27);

            Assert.AreEqual(true, RootIsBlack(tree), "Root should be only black color");
        }

        private static bool RootIsBlack(RedBlackTree tree)
        {
            return Color.Black == tree.GetRoot().GetColor();
        }


        [Test]
        public void CheckEachBranchForTheNumberOfBlackNodesAfterInsert()
        {
            var tree = new RedBlackTree(1);
            tree.InsertNode(2);
            tree.InsertNode(3);
            tree.InsertNode(4);
            tree.InsertNode(5);
            tree.InsertNode(6);
            tree.InsertNode(7);


            Assert.AreEqual(true, CheckEachBranchForTheNumberOfBlackNodes(tree),
                "The number of black nodes in each branch should be the same");
        }

        [Test]
        public void CheckEachBranchForTheNumberOfBlackNodesAfterDelete()
        {
            var tree = new RedBlackTree(13);
            tree.InsertNode(8);
            tree.InsertNode(17);
            tree.InsertNode(1);
            tree.InsertNode(11);
            tree.InsertNode(15);
            tree.InsertNode(25);
            tree.InsertNode(22);
            tree.InsertNode(27);
            tree.DeleteNode(13);
            tree.DeleteNode(17);
            tree.InsertNode(13);


            Assert.AreEqual(true, CheckEachBranchForTheNumberOfBlackNodes(tree),
                "The number of black nodes in each branch should be the same");
        }

        private static bool CheckEachBranchForTheNumberOfBlackNodes(RedBlackTree tree)
        {
            if (!tree.FindRootNode())
            {
                return false;
            }

            var currentNode = tree.GetRoot();

            var count = 0;
            var res = true;
            var prevCountOfBlack = 0;


            while (count < tree.CountOfNode() + 1)
            {
                var countOfBlack = 0;
                while (!currentNode.IsNil())
                {
                    if (currentNode.GetColor() == Color.Black)
                    {
                        countOfBlack++;
                    }

                    currentNode = !currentNode.GetLeft().GetFlag() ? currentNode.GetLeft() : currentNode.GetRight();
                }

                currentNode.SetFlag(true);

                count++;
                if (count == 1)
                {
                    prevCountOfBlack = countOfBlack;
                }
                else
                {
                    res = prevCountOfBlack == countOfBlack;
                }

                currentNode = tree.GetRoot();
            }

            return res;
        }

        [Test]
        public void CheckRedNodesAfterDelete()
        {
            var tree = new RedBlackTree(12);
            tree.InsertNode(8);
            tree.InsertNode(17);
            tree.DeleteNode(8);
            tree.InsertNode(1);
            tree.InsertNode(11);
            tree.DeleteNode(17);
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

        [Test]
        public void CheckForNodeExistenceWhenDeleting()
        {
            var tree = new RedBlackTree(12);
            tree.InsertNode(8);
            tree.InsertNode(17);
            Assert.AreEqual(false, tree.DeleteNode(5));
            Assert.AreEqual(true, tree.DeleteNode(8));
        }
    }
}