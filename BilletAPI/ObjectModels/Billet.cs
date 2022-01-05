using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilletAPI.ObjectModels
{
    public class Billet
    {
        int id;
        DateTime dato;
        string eventting;

        public Billet(int iD, DateTime datO, string evenT)
        {
            id = iD;
            dato = datO;
            eventting = evenT;
        }
    }
}
