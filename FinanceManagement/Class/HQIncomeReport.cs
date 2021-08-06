using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagement.Class
{
    public class HQIncomeReport
    {
        public string   Department { get; set; }

        public int      Index { get; set; }

        public string   Item { get; set; }

        public string   Sumary{ get; set; }

        public int      Month4 { get; set; }

        public int      Month5 { get; set; }

        public int      Month6 { get; set; }

        public int      Month7 { get; set; }

        public int      Month8 { get; set; }

        public int      Month9 { get; set; }

        public int      Month10 { get; set; }

        public int      Month11 { get; set; }

        public int      Month12 { get; set; }

        public int      Month1 { get; set; }

        public int      Month2 { get; set; }

        public int      Month3 { get; set; }

        public int      Sum
        {
            get
            {
                return Month1 + Month2 + Month3 + Month4 + Month5 + Month6 + Month7 + Month8 + Month9 + Month10 + Month11 + Month12;
            }
        }

    }
}
