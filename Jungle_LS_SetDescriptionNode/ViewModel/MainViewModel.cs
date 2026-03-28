using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungle_LS_SetDescriptionNode.ViewModel
{
    public class MainViewModel : BaseViewModel
    {


		private string _description = "Ф1";

		public string Description
		{
			get { return _description; }
			set 
			{ 
				_description = value; 
				OnPropertyChanged(nameof(Description));
            }
		}

	}
}
