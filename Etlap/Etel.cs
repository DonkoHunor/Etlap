using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etlap
{
	internal class Etel
	{
		public Etel(int id, string nev, string leiras, string kategoria, int ar)
		{
			Id = id;
			Nev = nev;
			Leiras = leiras;
			Kategoria = kategoria;
			Ar = ar;
		}

		public Etel()
		{

		}

		public int Id { get; set; }
		public string Nev { get; set; }
		public string Leiras{ get; set; }
		public string Kategoria { get; set; }
		public int Ar { get; set; }
	}
}
