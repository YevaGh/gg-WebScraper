using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RateAmLib;
using RateAmLib.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RatesAmApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly IRateService rateService;
        private readonly IBankService bankService;
        private readonly ICurrencyService currencyService;

        public RatesController(IRateService rateService, IBankService bankService, ICurrencyService currencyService)
        {
            this.rateService = rateService;
            this.bankService = bankService;
            this.currencyService = currencyService;
        }

        //GET: api/<RatesController>
        [HttpGet("latest-rates")]
        [ProducesResponseType(typeof(IEnumerable<Rate>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Rate>>> GetLatestRates()
        {
            try
            {

                var rates = await rateService.GetLatestRates();
             

                //if(rates == null)
                //{
                //    return NotFound();
                //}
                return Ok(rates);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {


            }
            return Ok(null);

        }

        [HttpGet("cached-rates")]
        public ActionResult<IEnumerable<Rate>> GetRates()
        {
            return Ok(rateService.GetAllRates());
        }

        [HttpGet("banks_and_currencies")]
        public async Task<ActionResult<IEnumerable<Object>>> GetBanksAndCurrencies()
        {
            var banks = await bankService.GetAll();
            var currencies = await currencyService.GetAll();
          
            var combinedArrays = new { banks = banks, currencies = currencies };

            string json = JsonConvert.SerializeObject(combinedArrays, Formatting.Indented);

            return Ok(combinedArrays);
        }

        [HttpGet("all-rates")]
        public async Task<ActionResult<IEnumerable<Rate>>> GetAllRates()
        {
            var allRates = await rateService.GetAllRatesAsync();

            return Ok(allRates);
        }
    }
}
