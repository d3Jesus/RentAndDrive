using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Chart.Mdl
{
    
    public class ComparisonBarChart
    {
        [Key, Column("JAN")]
        public int JAN { get; set; }
        [Column("FEV")]
        public int FEV { get; set; }
        [Column("MAR")]
        public int MAR { get; set; }
        [Column("ABR")]
        public int ABR { get; set; }
        [Column("MAI")]
        public int MAI { get; set; }
        [Column("JUN")]
        public int JUN { get; set; }
        [Column("JUL")]
        public int JUL { get; set; }
        [Column("AGO")]
        public int AGO { get; set; }
        [Column("SET")]
        public int SET { get; set; }
        [Column("OUT")]
        public int OUT { get; set; }
        [Column("NOV")]
        public int NOV { get; set; }
        [Column("DEZ")]
        public int DEZ { get; set; }
    }
}