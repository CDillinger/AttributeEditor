using AttributeEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
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
using Xceed.Wpf.Toolkit;

namespace AttributeEditor.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindowViewModel ViewModel { get; private set; }

		public MainWindow()
		{
			InitializeComponent();

			DataContext = ViewModel = new MainWindowViewModel(this);
		}

		private void Window_DragOver(object sender, DragEventArgs e)
		{
			e.Effects = DragDropEffects.All;
		}

		private void Window_Drop(object sender, DragEventArgs e)
		{
			string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
			ViewModel.File = new Models.File(files[files.Length - 1]);
		}

		private void ButtonSave_Click(object sender, RoutedEventArgs e)
		{
			if (ViewModel.File == null)
			{
				System.Windows.MessageBox.Show("No file was selected.");

				return;
			}

			if (ViewModel.File.Attributes.ReadOnly)
			{
				MessageBoxResult result = System.Windows.MessageBox.Show("Read-only is set. Would you like to save anyways?", "Read-only File", MessageBoxButton.YesNo);
				if (result == MessageBoxResult.Yes)
					Models.File.Save(ViewModel.File, true);

				return;
			}

			if (ViewModel.File.OriginallyReadOnly)
			{
				MessageBoxResult result = System.Windows.MessageBox.Show("The file was read-only when it was opened. Would you still like to save?", "Read-only File", MessageBoxButton.YesNo);
				if (result == MessageBoxResult.Yes)
					Models.File.Save(ViewModel.File);

				return;
			}

			Models.File.Save(ViewModel.File);
		}

		private void CheckBoxAttNormal_Checked(object sender, RoutedEventArgs e)
		{
			foreach (CheckBox c in StackPanelAttributes.Children)
				if (c != sender)
					c.IsChecked = false;
		}

		private void CheckBoxAttOthers_Checked(object sender, RoutedEventArgs e)
		{
			CheckBoxAttNormal.IsChecked = false;
		}
	}
}
