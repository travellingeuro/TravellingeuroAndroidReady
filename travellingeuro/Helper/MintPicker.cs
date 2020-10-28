using System;
using System.Collections.Generic;
using System.Text;

namespace travellingeuro.Helper
{
    class MintPicker
    {
        public string Name { get; set; }
        public string Picker(int value)
        {
            switch (value)
            {
                case 1:
                    Name = "Belgium";
                        break;
                case 2:
                    Name = "Austria";
                    break;
                case 3:
                    Name = "Latvia";
                    break;
                case 4:
                    Name = "United Kingdom";
                    break;
                case 5:
                    Name = "Slovakia";
                    break;
                case 6:
                    Name = "Malta";
                    break;
                case 7:
                    Name = "Cyprus";
                    break;
                case 8:
                    Name = "Slovenia";
                    break;
                case 9:
                    Name = "Italy";
                    break;
                case 10:
                    Name = "Ireland";
                    break;
                case 11:
                    Name = "Finland";
                    break;
                case 12:
                    Name = "Estonia";
                    break;
                case 13:
                    Name = "The Nederlands";
                    break;
                case 14:
                    Name = "Luxemburg";
                    break;
                case 15:
                    Name = "Ireland";
                    break;
                case 16:
                    Name = "Portugal";
                    break;
                case 17:
                    Name = "Greece";
                    break;
                case 18:
                    Name = "Slovakia";
                    break;
                case 19:
                    Name = "Spain";
                    break;
                case 20:
                    Name = "France";
                    break;
                case 21:
                    Name = "Italy";
                    break;
                case 22:
                    Name = "Lithuania";
                    break;
                case 23:
                    Name = "Germany";
                    break;
                default:
                    Name = "Not Defined";
                    break;
            }
            return Name;
        }
    }
}
