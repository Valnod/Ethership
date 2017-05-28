using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EtherShip;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TestEtherShip
{
    [TestClass]
    public class TestEtherShip
    {
        private GameObject obj;
        private Player p;
        private Enemy e;

        [TestInitialize]
        public void TestStart()
        {
            obj = new GameObject(new Vector2(100, 100));
            p = new Player(obj, new Vector2(1, 0), 3, false);
            e = new Enemy(obj, 100, 10f, 1, new Vector2());
        }

        [TestMethod]
        public void TestAntiGravity()
        {
            Assert.IsFalse(p.antiGravity); //Test if antiGravity bool is false
        }

        [TestMethod]
        public void TestGenerating()
        {
            Assert.IsFalse(e.Generating); //Test if Generating bool is false
        }

        [TestMethod]
        public void TestEnemy()
        {
            Assert.AreEqual(100, e.Health); //Test if the enemy's health is 100
        }
    }
}
