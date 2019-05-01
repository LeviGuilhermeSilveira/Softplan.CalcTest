using Microsoft.AspNetCore.Mvc;
using Softplan.CalcTest.Shared;
using System;
using System.Net;

namespace Softplan.CalcTest.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly string _url = "https://github.com/LeviGuilhermeSilveira/Softplan.CalcTest";

        public ValuesController(string url = null, bool useCurrentValue = true)
        {
            if (!useCurrentValue)
                _url = url;
        }

        /// <summary>
        /// Retornar a url de onde encontra-se o fonte no GitHub.
        /// </summary>
        /// <returns>
        /// String: URL
        /// </returns>
        /// <remarks>
        /// Este responde pelo path relativo /showmethecode
        /// Deverá retornar a url de onde encontra-se o fonte no github
        /// </remarks>
        [HttpGet]
        [Route("showmethecode")]
        public ActionResult<string> Get()
        {
            Validator validator = new Validator();
            validator.Validate(validate =>
            {
                validate.UriValidation
                    .ForlNotNullUrl(_url)
                    .ForNotEmptyUrl(_url)
                    .ForValidUrl(_url);
            });

            if (validator.IsValid)
                return Ok(_url);
            else
                return StatusCode((int)HttpStatusCode.InternalServerError, validator.ErrorMessage);
        }

        /// <summary>
        /// Efetua cálculo de juros compostos.
        /// </summary>
        /// <param name="valorInicial"></param>
        /// <param name="meses"></param>
        /// <returns>
        /// Double: Montante
        /// </returns>
        /// <remarks>
        /// Este reponde pelo path relativo "/calculajuros"
        /// Faz um cálculo em memória, de juros compostos, conforme abaixo: 
        /// Valor Final = Valor Inicial* (1 + juros) ^ Tempo
        /// </remarks>
        [HttpPost]
        [Route("calculajuros")]
        public ActionResult<string> Post([FromQuery] decimal valorInicial, int meses)
        {
            Validator validator = new Validator();

            validator.Validate(validate =>
            {
                validate.NumberValidation
                    .IsGreaterThanZero(valorInicial)
                    .IsGreaterThanZero(meses);
            });

            if (!validator.IsValid)
                return StatusCode((int)HttpStatusCode.InternalServerError, validator.ErrorMessage);

            double capital = (double)valorInicial;
            double indice = 0.01;
            double taxa = 1 + indice;
            double periodo = meses;

            var montante = capital * (Math.Pow(taxa, periodo));

            var result = Math.Truncate(montante * 100) / 100;

            return Ok(result.ToString("F"));
        }
    }
}
