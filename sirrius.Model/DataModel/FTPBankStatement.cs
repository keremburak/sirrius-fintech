using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sirrius.Model.DataModel
{
    //every bank has its own MT940 file format / pattern set..for this reason, one / some of below patterns need to be changed for each bank.
    //(need to come from a pattern table/static dictionary)
    public class FTPBankStatement
    {
        const string TAG_PATTERN = @"^:(?'tag'[^:]+):(?'value'.*)";

        public string senderReference { get; set; }  //code 20 - sender reference info
        public string authorization { get; set; } // tag 25 - account info - IBAN or Account Number
        public string messageIndexTotal { get; set; } //tag 28 / 28C - statement number/sequence number
        public Currency openingBalance { get; set; }  //tag 60F - opening balance
        //public string firstTransaction { get; set; } //tag 61
        //public string secondTransaction { get; set; } //tag 61
        public List<Transaction> transactions { get; set; } //tag 61  - transactions for the current date - N items
        public Currency closingBalance { get; set; } //tag 62F
        public Currency closingAvailableBalance { get; set; } //tag 62F
        public List<string> accountOwners { get; set; } //tag 86

        public FTPBankStatement(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return;

            accountOwners = new List<string>();
            transactions = new List<Transaction>();

            StreamReader reader = new StreamReader(filename);
            string line = "";
            //int transactionCount = 0;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith(":"))
                {
                    Match match = Regex.Match(line, TAG_PATTERN);
                    string tag = match.Groups["tag"].Value;
                    string value = match.Groups["value"].Value;

                    switch (tag)
                    {
                        case "20":
                            senderReference = value;
                            break;

                        case "25":
                            authorization = value;
                            break;
                        case "28":
                        case "28C":
                            messageIndexTotal = value;
                            break;
                        case "60F":
                            openingBalance = new Currency(value);
                            break;
                        case "61":
                            //if (++transactionCount == 1)
                            //{
                            //    firstTransaction = value;
                            //}
                            //else
                            //{
                            //    secondTransaction = value;
                            //}
                            transactions.Add(new Transaction(value));
                            break;
                        case "62F":
                            closingBalance = new Currency(value);
                            break;
                        case "64":
                            closingAvailableBalance = new Currency(value);
                            break;
                        case "86":
                            accountOwners.Add(value);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }
    public class Currency
    {
        const string BALANCE_PATTERN = @"^(?'credit_debit'.)(?'date'.{6})(?'country_code'.{3})(?'amount'.*)";
        static CultureInfo culture = CultureInfo.GetCultureInfoByIetfLanguageTag("tr");

        public DateTime date { get; set; }
        public string dateString { get { return date.ToString("dd/MM/yyyy"); } }
        public string currencyCode { get; set; }
        public decimal amount { get; set; }

        public Currency(string input)
        {
            if (string.IsNullOrEmpty(input)) return;

            Match match = Regex.Match(input, BALANCE_PATTERN);

            string credit_debit = match.Groups["credit_debit"].Value;

            string dateStr = match.Groups["date"].Value;
            date = DateTime.ParseExact(dateStr, "yyMMdd", CultureInfo.InvariantCulture);

            currencyCode = match.Groups["country_code"].Value;

            string amountStr = match.Groups["amount"].Value;
            amount = decimal.Parse(amountStr, culture);
            amount *= credit_debit == "D" ? -1 : 1;
        }
    }

    public class Transaction
    {
        const string TRANSACTION_PATTERN = @"^(?'date'.{6})(?'credit_debit'.)(?'currency_code'.{1})(?'amount'.{16})(?'booking_type'.{1})(?'booking_code'.{3})(?'Client_ref'.*)//-(?'bank_ref'.*)";
        static CultureInfo culture = CultureInfo.GetCultureInfoByIetfLanguageTag("tr");

        public DateTime date { get; set; }
        public string dateString { get { return date.ToString("dd/MM/yyyy"); } }
        public string currencyCode { get; set; }
        public bool isCredit { get; set; }
        public decimal amount { get; set; }
        public string bookingType { get; set; }
        public string bookingCode { get; set; }
        public string receiverReference { get; set; } //Client reference
        public string bankReference { get; set; } //Client reference

        public Transaction(string input)
        {
            if (string.IsNullOrEmpty(input)) return;

            Match match = Regex.Match(input, TRANSACTION_PATTERN);

            isCredit = match.Groups["credit_debit"].Value == "C" ? true : false;

            string dateStr = match.Groups["date"].Value;
            date = DateTime.ParseExact(dateStr, "yyMMdd", CultureInfo.InvariantCulture);

            currencyCode = match.Groups["currency_code"].Value;

            string amountStr = match.Groups["amount"].Value;
            amount = decimal.Parse(amountStr, culture);
            amount *= isCredit ? 1 : -1;

            bookingType = match.Groups["booking_type"].Value; //booking type - Swift / N - Non-Swift
            bookingCode = match.Groups["booking_code"].Value;

            receiverReference = match.Groups["Client_ref"].Value;
            bankReference = match.Groups["bank_ref"].Value;
        }

        //:62F:C210929TRY0000000000179812,15

        //:61: 210929DL0000000020000,00NEFTNONREF//-15.01.57.820749
    }
}
