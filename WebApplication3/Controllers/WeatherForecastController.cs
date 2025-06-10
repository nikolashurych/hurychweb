using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace WebServiceExample.Controllers
{
    [ApiController]
    [Route("api")]


    public class ExampleController : ControllerBase
    {
        // 1. P�evod m�n
        [HttpGet("prevedMeny")]
        public ActionResult<decimal> PrevedMeny(decimal castka, string zdrojMena, string cilovaMena)
        {
            var kurzy = new Dictionary<string, decimal>
            {
                { "CZK", 1m },
                { "EUR", 25m },
                { "USD", 22m }
            };

            if (!kurzy.ContainsKey(zdrojMena) || !kurzy.ContainsKey(cilovaMena))
                return BadRequest("Nezn�m� m�nov� k�d");

            decimal castkaVCzk = castka * kurzy[zdrojMena];
            decimal prevedenaCastka = castkaVCzk / kurzy[cilovaMena];
            return Ok(Math.Round(prevedenaCastka, 2));
        }
        // 2. Vypocet BMI
        [HttpGet("vypocetBMI")]
        public ActionResult<object> VypocetBMI(decimal vahaKg, decimal vyskaCm, int vek)
        {



            decimal vyskaM = vyskaCm / 100m;
            decimal bmi = vahaKg / (vyskaM * vyskaM);
            bmi = Math.Round(bmi, 2);


            string hodnoceni;
            if (bmi < 18.5m)
                hodnoceni = "Podv�ha";
            else if (bmi < 25m)
                hodnoceni = "Norm�ln� v�ha";
            else if (bmi < 30m)
                hodnoceni = "Nadv�ha";
            else
                hodnoceni = "Obezita";

            return Ok(new
            {

                BMI = bmi,
                Hodnoceni = hodnoceni
            });
        }

        // 3. Vr�cen� dne v t�dnu podle data
        [HttpGet("denVTydnu")]
        public ActionResult<string> DenVTydnu(string datum)
        {
            if (!DateTime.TryParse(datum, out var dt))
                return BadRequest("Neplatn� datum");

            return Ok(dt.DayOfWeek.ToString());
        }
    }
}


