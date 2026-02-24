using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Jungle_LS_ColumnarFoundation.ViewModel
{
    public class ViewModelFoundation : BaseViewModel
    {
		private double _width1 = 4;

		public double Width1
		{
			get { return _width1; }
			set 
			{ 
				_width1 = value;
                OnPropertyChanged(nameof(Width1));
            }
		}

		private double _width2 = 4;

		public double Width2
		{
			get { return _width2; }
			set 
			{ 
				_width2 = value; 
				OnPropertyChanged(nameof(Width2));
            }
		}

		private double _thickPlate = 1.2;

		public double ThickPlate
		{
			get { return _thickPlate; }
			set 
			{ 
				_thickPlate = value; 
				OnPropertyChanged(nameof(ThickPlate));
            }
		}

		private double _height = 2;

		public double Height
		{
			get { return _height; }
			set 
			{ 
				_height = value;
				OnPropertyChanged(nameof(Height));
            }
		}


		private double _b1 = 0.5;

		public double B1
		{
			get { return _b1; }
			set { _b1 = value; }
		}

		private double _b2 = 0.5;

		public double B2
		{
			get { return _b2; }
			set { _b2 = value; }
		}

        private string _classConcrete = "B25";

        public string ClassConcrete
        {
            get { return _classConcrete; }
            set
            {
                _classConcrete = value;
                OnPropertyChanged(nameof(ClassConcrete));
            }
        }

    }
}
