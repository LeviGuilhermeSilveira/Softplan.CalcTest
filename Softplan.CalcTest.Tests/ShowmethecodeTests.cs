using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Softplan.CalcTest.Controllers;

namespace Softplan.CalcTest.Tests
{
    public class ShowMeTheCodeTests
    {
        private ValuesController _valuesController;

        [SetUp]
        public void Setup()
        {
            _valuesController = new ValuesController(null, false);
        }

        [Test]
        public void Showmethecode_ExpectingNullUrl_ValueCannotBeNull()
        {
            string errorMessage = "Value cannot be null";

            var response = _valuesController.Get();
            var result = response.Result as ObjectResult;

            Assert.AreEqual(result.StatusCode, 500);
            Assert.IsTrue(result.Value.ToString().Contains(errorMessage));
        }

        [Test]
        public void Showmethecode_ExpectingEmptyUrl_InvalidURI()
        {
            string errorMessage = "Invalid URI";

            var response = new ValuesController(string.Empty, false).Get();
            var result = response.Result as ObjectResult;

            Assert.AreEqual(result.StatusCode, 500);
            Assert.IsTrue(result.Value.ToString().Contains(errorMessage));
        }

        [Test]
        public void Showmethecode_ExpectingInvalidUrl_InvalidURI()
        {
            string errorMessage = "Invalid URI";

            var response = new ValuesController("xyz.com", false).Get();
            var result = response.Result as ObjectResult;

            Assert.AreEqual(result.StatusCode, 500);
            Assert.IsTrue(result.Value.ToString().Contains(errorMessage));
        }

        [Test]
        public void Showmethecode_ExpectingValidUrl_Success()
        {
            var url = "https://github.com/LeviGuilhermeSilveira/SoftplanCalcTest";

            var response = new ValuesController().Get();
            var okResponse = response.Result as OkObjectResult;

            Assert.AreEqual(okResponse.StatusCode, 200);
            Assert.AreEqual(okResponse.Value, url);
        }

    }
}