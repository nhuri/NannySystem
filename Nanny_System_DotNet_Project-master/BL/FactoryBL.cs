using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class FactoryBL
    {

        static IBL BlP = null;
        public static IBL GetBL()
        {
            if (BlP == null)
                BlP = new BLֹֹ_imp();
            return BlP;
        }

    }
}
