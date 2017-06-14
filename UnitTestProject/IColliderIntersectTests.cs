using GameCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace UnitTestProject
{
    [TestClass]
    public class ICol
    {
        [TestMethod]
        public void PlayerIsBlockedByBlocks()
        {
            //var inputs = new InputRepository() { Down=true };

            //var player = new Player() { VerticalSpeed = 10 };
            //var block = new Block { Y = 110 };
            //var world = new World();

            //world.AddCollider(player);
            //world.AddCollider(block);
            //world.Update();

            //var expectedY = block.Top() - player.Height - World.SPACE_BETWEEN_THINGS;
            //Assert.AreEqual(expectedY, player.Y);
            //world.Update();
            //Assert.AreEqual(expectedY, player.Y);
            //world.Update();
            //Assert.AreEqual(expectedY, player.Y);
        }


        [TestMethod]
        public void HorizontalMovement()
        {
            //var world = new World();

            //var player = Substitute.For<Collider>();
            //player.HorizontalSpeed = 1;
            //world.Add(player);

            //Assert.AreEqual(0, player.X);

            //world.Update();
            //Assert.AreEqual(1, player.X);

            //world.Update();
            //Assert.AreEqual(2, player.X);
        }

        [TestMethod]
        public void BerticalMovement()
        {
            //var world = new World();

            //var player = Substitute.For<Collider>();
            //player.VerticalSpeed = 1;
            //world.Add(player);

            //Assert.AreEqual(0, player.Y);

            //world.Update();
            //Assert.AreEqual(1, player.Y);

            //world.Update();
            //Assert.AreEqual(2, player.Y);
        }

        [TestMethod]
        public void CallBotCollisionHandlerOnCollision()
        {
            //var collider = Substitute.For<Collider, ICollisionHandler>();
            //var other = Substitute.For<Collider>();

            //collider.Y = 0;
            //collider.Width = 10;
            //collider.Height = 10;
            //collider.VerticalSpeed = 1;

            //other.Y = 11;
            //other.Width = 10;
            //other.Height = 10;

            //var sut = new World();
            //sut.Add(collider);
            //sut.Add(other);

            //sut.Update();

            //collider.As<ICollisionHandler>()
            //    .Received(1).BotCollision(other);
        }
    }

    [TestClass]
    public class IColliderIntersectTests
    {
        public Collider CreateCollider(int x, int y, int width, int height)
        {
            var collider = Substitute.For<Collider>();
            collider.X = x;
            collider.Y = y;
            collider.Width = width;
            collider.Height = height;

            return collider;
        }

        [TestMethod]
        public void Intersect_False()
        {
            var source = CreateCollider(20, 0, 10, 10);
            var target = CreateCollider(9, 0, 10, 10);

            Assert.AreEqual(CollisionResult.Nope, source.IsColliding(target));
        }

        [TestMethod]
        public void Intersect_From_The_Left()
        {
            var source = CreateCollider(20, 0, 10, 10);
            var target = CreateCollider(9, 0, 10, 10);

            Assert.AreEqual(CollisionResult.Nope, source.IsColliding(target));

            target.X = 10;
            Assert.AreEqual(CollisionResult.Left, source.IsColliding(target));

            target.X = 11;
            Assert.AreEqual(CollisionResult.Left, source.IsColliding(target));
        }

        [TestMethod]
        public void Intersect_From_The_Right()
        {
            var source = CreateCollider(20, 0, 10, 10);
            var target = CreateCollider(31, 0, 10, 10);

            Assert.AreEqual(CollisionResult.Nope, source.IsColliding(target));

            target.X = 30;
            Assert.AreEqual(CollisionResult.Right, source.IsColliding(target));

            target.X = 29;
            Assert.AreEqual(CollisionResult.Right, source.IsColliding(target));
        }

        [TestMethod]
        public void Intersect_From_The_Top()
        {
            var source = CreateCollider(0, 30, 10, 10);
            var target = CreateCollider(0, 19, 10, 10);

            Assert.AreEqual(CollisionResult.Nope, source.IsColliding(target));

            target.Y = 20;
            Assert.AreEqual(CollisionResult.Top, source.IsColliding(target));

            target.Y = 21;
            Assert.AreEqual(CollisionResult.Top, source.IsColliding(target));
        }

        [TestMethod]
        public void Intersect_From_The_Bottom()
        {
            var source = CreateCollider(0, 20, 10, 10);
            var target = CreateCollider(0, 31, 10, 10);

            Assert.AreEqual(CollisionResult.Nope, source.IsColliding(target));

            target.Y = 30;
            Assert.AreEqual(CollisionResult.Bottom, source.IsColliding(target));

            target.Y = 29;
            Assert.AreEqual(CollisionResult.Bottom, source.IsColliding(target));
        }
    }
}
