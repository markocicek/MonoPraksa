using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Business.Common
{
    public class Paging
    {
        private int rpp;
        private int pageNumber;
        private int offset;
        public int Rpp { get { return rpp; } set { rpp = value; } }
        public int PageNumber { get { return pageNumber; } set { pageNumber = value; } }
        public int Offset { get { return offset; } set { offset = value; } }

        public Paging()
        {
            Rpp = 4;
            PageNumber = 1;
            Offset = 0;
        }
        public Paging(int pageNumber, int rpp)
        {
            PageNumber = pageNumber;
            if(pageNumber < 1)
            {
                PageNumber = 1;
            }
            Rpp = rpp;
            if(rpp < 4)
            {
                Rpp = 4;
            }
            Offset = Rpp * (PageNumber - 1);
        }
    }
}
