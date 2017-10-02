using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ReportWebApplication.Commons
{
    public class clsCicReport
    {
        [DisplayName(@"MADV")]
        public string MADV { get; set; }
        [DisplayName(@"MTV")]
        public string MTV { get; set; }
        [DisplayName(@"HOTEN")]
        public string HOTEN { get; set; }
        [DisplayName(@"LOAIKH")]
        public string LOAIKH { get; set; }
        [DisplayName(@"GT")]
        public string GT { get; set; }
        [DisplayName(@"NG_SINH")]
        public string NG_SINH { get; set; }
        [DisplayName(@"DIACHI")]
        public string DIACHI { get; set; }
        [DisplayName(@"CMND")]
        public string CMND { get; set; }
        [DisplayName(@"MST")]
        public string MST { get; set; }
        [DisplayName(@"NG_CAP")]
        public string NG_CAP { get; set; }
        [DisplayName(@"HD_SO")]
        public string HD_SO { get; set; }
        [DisplayName(@"HD_NGKY")]
        public string HD_NGKY { get; set; }
        [DisplayName(@"HD_NGDH")]
        public string HD_NGDH { get; set; }
        [DisplayName(@"HD_HM")]
        public string HD_HM { get; set; }
        [DisplayName(@"KU_SO")]
        public string KU_SO { get; set; }
        [DisplayName(@"KU_NGKY")]
        public string KU_NGKY { get; set; }
        [DisplayName(@"KU_NGDH")]
        public string KU_NGDH { get; set; }
        [DisplayName(@"KU_NGPS")]
        public string KU_NGPS { get; set; }
        [DisplayName(@"LAISUAT")]
        public decimal LAISUAT { get; set; }
        [DisplayName(@"LOAIVAY")]
        public string LOAIVAY { get; set; }
        [DisplayName(@"MUCDICH")]
        public string MUCDICH { get; set; }
        [DisplayName(@"DUNO")]
        public decimal DUNO { get; set; }
        [DisplayName(@"NHOMNO")]
        public string NHOMNO { get; set; }
    }
}