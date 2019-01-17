using Stem.Core;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedStem.Core.Entities
{
    [Table("Currency", Schema = "Core")]
    public class Currency : BaseEntity
    {
        public Currency()
        { }

        public string Iso4217 { get; set; }
        public string Name { get; set; }
        public string CountryTwoLetter { get; set; }
        public string Symbol { get; set; }
        public string CultureName { get; set; }
        public string NativeName { get; set; }
        public string EnglishName { get; set; }
        public byte[] Flag { get; set; }
        public string FlagMime { get; set; }
        public decimal? ExchangeRate { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }
}

//for currency: http://admin.worketc.com/xml/GetAllCurrenciesWebSafe
//for flags :   http://www.oorsprong.org/websamples.countryinfo/countryinfoservice.wso/CountryFlag/JSON/debug?sCountryISOCode=ZAR