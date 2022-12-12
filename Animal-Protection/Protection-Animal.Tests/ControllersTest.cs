using Animal_Protection.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Protection_Animal.Model.Entities;

namespace Protection_Animal.Tests
{
    [TestFixture]
    public class ControllersTest
    {
        [Test]
        public void AnimalControllerIndexReturnView()
        {
            var obj = new AnimalController();

            IActionResult? resutl = obj.Index() as IActionResult;
            Assert.That(resutl, Is.EqualTo("Index"));
        }
    }
}