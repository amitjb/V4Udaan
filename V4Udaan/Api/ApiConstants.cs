using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V4Udaan.Api
{
    public static class ApiConstants
    {
        public static readonly string BaseURL = "https://www.v4account.com/ws";
        public static readonly string LoginApiUrl = $"{BaseURL}/tally_login.php";
        public static readonly string CompanyInsertApiUrl = $"{BaseURL}/tally_company_insert.php";
        public static readonly string XmlInsertApiUrl = $"{BaseURL}/tally_xml_insert.php";
        public static readonly string TallyStatusApiUrl = $"{BaseURL}/tally_status.php";

        public static readonly string TallyBaseURL = "http://localhost:9000";

        public static readonly string TallyReportName_Stock = "stock category summary";
        public static readonly string TallyReportName_DayBook = "daybook";

        public static readonly string XML_ListCompany = $"<ENVELOPE><HEADER><TALLYREQUEST>Export Data</TALLYREQUEST></HEADER>" +
                                                        $"<BODY><EXPORTDATA><REQUESTDESC><REPORTNAME>List of Companies</REPORTNAME>" +
                                                        $"<STATICVARIABLES><SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT></STATICVARIABLES>" +
                                                        $"</REQUESTDESC></EXPORTDATA></BODY></ENVELOPE>";

        public static string XML_DayBook(string companyName) => $"<ENVELOPE><HEADER><VERSION>1</VERSION><TALLYREQUEST>Export</TALLYREQUEST><TYPE>Collection</TYPE>" +
                                                    $"<ID>Day Book</ID></HEADER><BODY>" +
                                                    $"<DESC><STATICVARIABLES><SVCURRENTCOMPANY>{companyName}</SVCURRENTCOMPANY><SVFROMDATE TYPE=\"Date\">02-apr-2017</SVFROMDATE>" +
                                                    $"<SVTODATE TYPE=\"Date\">02-mar-2017</SVTODATE><EXPLODEFLAG>Yes</EXPLODEFLAG>" +
                                                    $"<SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT></STATICVARIABLES>" +
                                                    $"<TDL><TDLMESSAGE><COLLECTION NAME=\"Day Book\" ISMODIFY=\"No\"><TYPE>Voucher</TYPE>" +
                                                    $"<FETCH>Vouchernumber,Amount</FETCH></COLLECTION></TDLMESSAGE></TDL></DESC></BODY></ENVELOPE>";

        public static string XML_StockCategorySummary(string companyName) => $"<ENVELOPE><HEADER><TALLYREQUEST>Export Data</TALLYREQUEST></HEADER><BODY>" +
                                                                             $"<EXPORTDATA><REQUESTDESC><REPORTNAME>stock category summary</REPORTNAME><STATICVARIABLES>" +
                                                                             $"<SVCURRENTCOMPANY>{companyName}</SVCURRENTCOMPANY>" +
                                                                             $"</STATICVARIABLES></REQUESTDESC></EXPORTDATA></BODY></ENVELOPE>";
        public static string XML_GetReport(string reportName, string companyName) =>
            $"<ENVELOPE><HEADER><TALLYREQUEST>Export Data</TALLYREQUEST></HEADER><BODY><EXPORTDATA><REQUESTDESC><REPORTNAME>{reportName}</REPORTNAME><STATICVARIABLES>" +
            $"<SVCURRENTCOMPANY>{companyName}</SVCURRENTCOMPANY></STATICVARIABLES></REQUESTDESC></EXPORTDATA></BODY></ENVELOPE>";
    }
}
