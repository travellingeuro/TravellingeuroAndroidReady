using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xamarin.Forms;

namespace travellingeuro.Helper
{
	public class ImagePicker
	{
		public ImageSource Image { get; set; }
		public ImageSource Imagepicker(int value)
		{
			switch (value)
			{
                case 5:
					Image= ImageSource.FromResource("travellingeuro.Images.five.gif");
					break;
				case 10:
					Image = ImageSource.FromResource("travellingeuro.Images.ten.gif");
					break;
				case 20:
					Image = ImageSource.FromResource("travellingeuro.Images.twenty.gif");
					break;
				case 50:
					Image = ImageSource.FromResource("travellingeuro.Images.fifty.gif");
					break;
				case 100:
					Image = ImageSource.FromResource("travellingeuro.Images.onehundred.gif");
					break;
				case 200:
					Image = ImageSource.FromResource("travellingeuro.Images.twohundred.gif");
					break;
				case 500:
					Image = ImageSource.FromResource("travellingeuro.Images.binladen.gif");
					break;
				default:
					Image = ImageSource.FromResource("travellingeuro.Images.specimen.gif");
					break;
			}
			return Image;
		}
	}
}
