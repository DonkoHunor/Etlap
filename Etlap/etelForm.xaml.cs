using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Etlap
{
	/// <summary>
	/// Interaction logic for etelForm.xaml
	/// </summary>
	public partial class etelForm : Window
	{
		DbService service = new DbService();

		public etelForm()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string nev = nevInput.Text;
			string leiras = leirasInput.Text;
			string kategoria = kategoriaCombo.Text;
			string arText = arInput.Text;

			if(nev.Trim().Length < 1)
			{
				MessageBox.Show("Nem adott meg nevet!");
				return;
			}
			else if(leiras.Trim().Length < 1)
			{
				MessageBox.Show("Nem adott meg leírást!");
				return;
			}
			else if(arText.Trim().Length < 1)
			{
				MessageBox.Show("Nem adott meg árat!");
				return;
			}
			else if(!int.TryParse(arText,out int ar))
			{
				MessageBox.Show("Árnak csak számot lehet megadni!");
				return;
			}
			else
			{
				Etel etel = new Etel();
				etel.Nev = nev;
				etel.Leiras = leiras;
				etel.Kategoria = kategoria;
				etel.Ar = ar;
				if(service.Create(etel))
				{
					MessageBox.Show("Sikeres hozzáadás");
					this.Close();
				}
				else
				{
					MessageBox.Show("Sikertelen hozzáadás");
				}
			}
        }
    }
}
