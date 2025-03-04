using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{

    public class FactoryDal
    {
        static Dal_XML dalP = null;
        public static Idal GetDal()
        {
            if (dalP == null)
                dalP = new Dal_XML();
            return dalP;
        }
    }
}
