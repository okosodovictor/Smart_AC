using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Models
{
    public class Page
    {
        public int Offset { get; set; }
        public int Limit { get; set; }

        public Page(int page, int itemsPerPage = 50)
        {
            if (page < 1) page = 1;
            Offset = (page - 1) * itemsPerPage;
            Limit = itemsPerPage;
        }
    }
}
