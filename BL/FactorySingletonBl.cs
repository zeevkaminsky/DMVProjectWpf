using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class FactorySingletonBl
    {
        private static IBl instance = null;

        private FactorySingletonBl() { }

        public static IBl GetBl()
        {
            if (instance == null)
            {
                instance = new MyBl();
            }
            return instance;
        }
    }
}
