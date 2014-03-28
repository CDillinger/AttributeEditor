using System;
using System.Collections.Generic;
using System.Diagnostics;
using AttributeEditor.Models;
using AttributeEditor.Views;
using System.IO;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace AttributeEditor.ViewModels
{
	public class MainWindowViewModel : Base
	{
		public MainWindowViewModel(MainWindow window)
		{
			Window = window;
		}

		public MainWindow Window
		{
			get { return _window; }
			set { SetField(ref _window, value); }
		}
		private MainWindow _window;

		#region Properties

		public AttributeEditor.Models.File File
		{
			get { return _file; }
			set { SetField(ref _file, value); }
		}
		private AttributeEditor.Models.File _file;

		#endregion
	}
}
