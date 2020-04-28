using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xamarin.Forms;

namespace travellingeuro.Helper
{
	public class ImagePicker
	{
		public ImageSource image { get; set; }
		public ImageSource Imagepicker(int value)
		{
			switch (value)
			{
                case 5:
					image= ImageSource.FromResource("travellingeuro.Images.five.gif");
					break;
				case 10:
					image = ImageSource.FromResource("travellingeuro.Images.ten.gif");
					break;
				case 20:
					image = ImageSource.FromResource("travellingeuro.Images.twenty.gif");
					break;
				case 50:
					image = ImageSource.FromResource("travellingeuro.Images.fifty.gif");
					break;
				case 100:
					image = ImageSource.FromResource("travellingeuro.Images.onehundred.gif");
					break;
				case 200:
					image = ImageSource.FromResource("travellingeuro.Images.twohundred.gif");
					break;
				case 500:
					image = ImageSource.FromResource("travellingeuro.Images.binladen.gif");
					break;
				default:
					image = ImageSource.FromResource("travellingeuro.Images.specimen.gif");
					break;
			}
			return image;
		}
	}
}
