using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace V4Udaan.Api.XML_Models
{
    [XmlRoot(ElementName = "COMPANYNAME.LIST")]
    public class COMPANYNAME_LIST
    {
        [XmlElement(ElementName = "COMPANYNAME")]
        public List<string> COMPANYNAME { get; set; }
    }

    [XmlRoot(ElementName = "ENVELOPE")]
    public class ENVELOPE
    {
        [XmlElement(ElementName = "COMPANYNAME.LIST")]
        public COMPANYNAME_LIST COMPANYNAME_LIST { get; set; }
    }
}
