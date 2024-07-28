using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNetApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanciamentoController : ControllerBase
    {
        // GET: api/<FinanciamentoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FinanciamentoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FinanciamentoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FinanciamentoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FinanciamentoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public class Financiamento
        {
            enum Tabela
            {
                Sac = 1,
                Price = 2
            }

            public IList<double> ListaDePrestacoesPrice { get; set; }
            public IList<double> ListaDePrestacoesSac { get; set; }
            public double ValorPresente { get; set; }
            public double TaxaDeJurosAoMes { get; set; }
            public int NumeroParcelas { get; set; }
            public double Amortizacao { get; set; }
            public double TotalJurosPrice { get; set; }
            public double TotalJurosSac { get; set; }
            public double TotalPagoPrice { get; set; }
            public double TotalPagoSac { get; set; }
            public int Modalidade { get; }

            public Financiamento()
            {
                this.ListaDePrestacoesPrice = new List<double>();
                this.ListaDePrestacoesSac = new List<double>();
            }

            private void CalculandoAmortizacao()
            {
                this.Amortizacao = this.ValorPresente / this.NumeroParcelas;
            }

            private void FinanciarTabelaPrince()
            {
                double prestacao = this.ValorPresente * (Math.Pow((1 + this.TaxaDeJurosAoMes), this.NumeroParcelas) * this.TaxaDeJurosAoMes) / (Math.Pow((1 + this.TaxaDeJurosAoMes), this.NumeroParcelas) - 1);
                ListaDePrestacoesPrice.Add(prestacao);

            }

            private void FinanciarTabealSac()
            {
                this.CalculandoAmortizacao();

                for (int i = 0; i < this.NumeroParcelas; i++)
                {
                    double prestacao = this.Amortizacao + this.TaxaDeJurosAoMes * (this.ValorPresente - (i * this.NumeroParcelas));
                    this.ListaDePrestacoesSac.Add(prestacao);
                }
            }

            private void CalculaTotalPagoPrice()
            {
                this.TotalPagoPrice = this.ListaDePrestacoesPrice[0] * this.NumeroParcelas;
            }

            private void CalculaTotalJurosPrice()
            {
                if (this.TotalPagoPrice == 0)
                {
                    this.CalculaTotalPagoPrice();
                }
                this.TotalPagoPrice = this.TotalPagoPrice - this.ValorPresente;
            }

            private void CalculaTotalPagoSac()
            {
                for (int i = 0; i < this.NumeroParcelas; i++)
                {
                    this.TotalPagoSac += this.ListaDePrestacoesSac[i];
                }

            }

            private void CalculaTotalJurosSac()
            {
                if (this.TotalPagoSac == 0)
                {
                    this.CalculaTotalPagoSac();
                }
                this.TotalPagoSac = this.TotalPagoSac - this.ValorPresente;
            }
        }
    }
}
