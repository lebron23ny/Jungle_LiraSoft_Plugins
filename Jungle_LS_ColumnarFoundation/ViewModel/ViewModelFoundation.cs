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

		private double _c1 = 0;

		public double C1
		{
			get { return _c1; }
			set 
			{ 
				_c1 = value; 
				OnPropertyChanged(nameof(C1));
            }
		}

		private double _c2 = 0;

		public double C2
		{
			get { return _c2; }
			set 
			{ 
				_c2 = value;
				OnPropertyChanged(nameof(C2));
            }
		}

		private double _cx = 0;

		public double Cx
		{
			get { return _cx; }
			set 
			{ 
				_cx = value; 
				OnPropertyChanged(nameof(Cx));
            }
		}

		private double _cy = 0;

		public double Cy
		{
			get { return _cy; }
			set 
			{ 
				_cy = value; 
				OnPropertyChanged(nameof(Cy));
            }
		}





	}
}
