﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AttributeEditor.Models
{
	/// <summary>
	/// A class that utilizes <see cref="INotifyPropertyChanged"/>, allowing for easy addition of databinding to any addition model.
	/// </summary>
	public abstract class Base : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		public virtual bool SetField<T>(ref T field, T value,
			[CallerMemberName] string propertyName = "")
		{
			if (EqualityComparer<T>.Default.Equals(field, value)) return false;
			field = value;
			OnPropertyChanged(propertyName);
			return true;
		}
	}
}
