using MyOwnGame.Models;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace MyOwnGame.XamlHelpers
{
    public class EnumConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return "";
			}

			foreach (var one in Enum.GetValues(typeof(QuestionType)))
			{
				if (value.Equals(one))
				{
					return GetDescription((Enum)one);
				}
			}
			return "";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return null;
			}

			foreach (var one in Enum.GetValues(parameter as Type))
			{
				if (value.ToString() == GetDescription((Enum)one))
				{
					return one;
				}
			}
			return null;
		}

		public static string GetDescription(Enum en)
		{
			var type = en.GetType();
			var memInfo = type.GetMember(en.ToString());
			if (memInfo != null && memInfo.Length > 0)
			{
				var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
				if (attrs != null && attrs.Length > 0)
				{
					return ((DescriptionAttribute)attrs[0]).Description;
				}
			}
			return en.ToString();
		}
	}
}
