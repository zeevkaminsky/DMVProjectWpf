using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public sealed class FactorySingletonDal
    {
        private static IDal instance = null;

        private FactorySingletonDal() { }

        public static IDal GetDal()
        {
            if (instance == null)
            {
                instance = new DalXML();
            }
            return instance;
        }
    }
}
