using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Softplan.CalcTest.Controllers;
using System;

namespace Softplan.CalcTest.Tests
{
    public class CalculaJurosTests
    {
        private ValuesController _valuesController;

        [SetUp]
        public void Setup()
        {
            _valuesController = new ValuesController();
        }

        [Test]
        public void CalculaJuros_ValorInicialMenorQueZero_InvalidOperation()
        {
            string errorMessage = "O valor é menor ou igual a zero.";

            var response = _valuesController.Post(-1500, 5);
            var result = response.Result as ObjectResult;

            Assert.AreEqual(result.StatusCode, 500);
            Assert.IsTrue(result.Value.ToString().Contains(errorMessage));
        }

        [Test]
        public void CalculaJuros_MesesMenorQueZero_InvalidOperation()
        {
            string errorMessage = "O valor é menor ou igual a zero.";

            var response = _valuesController.Post(1500, -5);
            var result = response.Result as ObjectResult;

            Assert.AreEqual(result.StatusCode, 500);
            Assert.IsTrue(result.Value.ToString().Contains(errorMessage));
        }

        [Test]
        public void CalculaJuros_AmbosMenoresQueZero_InvalidOperation()
        {
            string errorMessage = "O valor é menor ou igual a zero.";

            var response = _valuesController.Post(-1500, -5);
            var result = response.Result as ObjectResult;

            Assert.AreEqual(result.StatusCode, 500);
            Assert.IsTrue(result.Value.ToString().Contains(errorMessage));
        }

        [Test]
        public void CalculaJuros_ValoresAceitaveis_Success()
        {
            decimal esperado = 2253.65M;

            var response = _valuesController.Post(2000, 12);
            var okResult = response.Result as OkObjectResult;

            Assert.AreEqual(okResult.StatusCode, 200);
            Assert.True(Convert.ToDecimal(okResult.Value) == esperado);
        }
    }
}