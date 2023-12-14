using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Etlap
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		DbService service = new DbService();

		public MainWindow()
		{
			InitializeComponent();
			data.ItemsSource = service.GetAll();
		}

		private void data_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(data.SelectedItem == null)
			{
				leirasTB.Content = "";
			}
			else
			{
				leirasTB.Content = (data.SelectedItem as Etel).Leiras;
			}
			
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if(data.SelectedItem == null)
			{
				MessageBox.Show("Nincs kiválasztott étel");
			}
			else
			{
				if(MessageBox.Show("Biztos törli ezt az ételt: " + (data.SelectedItem as Etel).Nev,"Törlés",MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes)
				{
					service.Delete(data.SelectedItem as Etel);
				}
			}
			data.ItemsSource = service.GetAll();
		}

		private void Button_Click_Add(object sender, RoutedEventArgs e)
		{
			etelForm form = new etelForm();
			form.Closed += (_, _) => 
			{
				data.ItemsSource = service.GetAll();
			};
			form.ShowDialog();
		}

		private void arInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			var textBox = sender as TextBox;
			e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
		}

		private void btnFix_Click(object sender, RoutedEventArgs e)
		{
			Etel etel = data.SelectedItem as Etel;
			int ar = Convert.ToInt32(arInput.Text);
			if(etel == null)
			{
				if(MessageBox.Show("Biztos növeli az összes étel árát?","Áremelés",MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes)
				{
					service.UpdateAll(ar);
				}
			}
			else 
			{
				if (MessageBox.Show("Biztos növeli ennek az ételenek az árát?:\n" + etel.Nev, "Áremelés", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
				{
					service.Update(etel,ar);
				}
			}
			arInput.Text = "";
			arSzazalekInput.Text = "";
			data.ItemsSource = service.GetAll();
		}

		private void btnSzazalek_Click(object sender, RoutedEventArgs e)
		{
			Etel etel = data.SelectedItem as Etel;
			int ar = Convert.ToInt32(arSzazalekInput.Text);
			if (etel == null)
			{
				if (MessageBox.Show("Biztos növeli az összes étel árát?", "Áremelés", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
				{
					service.UpdatePercentAll(ar);
				}
			}
			else
			{
				if (MessageBox.Show("Biztos növeli ennek az ételenek az árát?:\n" + etel.Nev, "Áremelés", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
				{
					service.UpdatePercent(etel, ar);
				}
			}
			arInput.Text = "";
			arSzazalekInput.Text = "";
			data.ItemsSource = service.GetAll();
		}
	}
}
