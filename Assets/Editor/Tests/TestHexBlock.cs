using System.Collections;
using HyperCasualMatchGame;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestHexBlock
    {
        HexBlock block1, block2;

        [SetUp]
        public void Setup()
        {
            block1 = new GameObject("1").AddComponent<HexBlock>();
            block2 = new GameObject("2").AddComponent<HexBlock>();
        }

        [Test]
        public void TestHexBlock_When_Type_Are_Equal()
        {
            Assert.AreEqual(block1,block2);
        }

        [Test]
        public void TestHexBlock_When_Type_Are_Not_Equal()
        {
            block1.type = 1;
            Assert.AreNotEqual(block1, block2);
        }

        [Test]
        public void TestHexBlock_When_Type_Are_Equal_Positions_Different()
        {
            block1.type = 0;
            block1.x = 2;
            Assert.AreNotEqual(block1, block2);
            block1.x = 0;
        }

        [Test]
        public void TestHexBlock_Operator_When_Type_Are_Not_Equal()
        {
            block1.type = 1;
            Assert.False(block1 == block2);
        }

        [Test]
        public void TestHexBlock_Operator_When_Type_Are_Equal()
        {
            block1.type = 0;
            Assert.True(block1 == block2);
        }

        [Test]
        public void TestHexBlock_When_Special()
        {
            block1.isSpecial = true;
            Assert.True(block1 == block2);
        }


        [TearDown]
        public void Teardown()
        {
            GameObject.DestroyImmediate(block1);
            GameObject.DestroyImmediate(block2);
        }
    }
}
