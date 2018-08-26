using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill_Calculator
{
   public class BillCal : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string _foodName;
        public double _price;
        public int _quantity;
        public string foodName
        {
            get { return _foodName; }
            set
            {
                _foodName = value;
               
            }
        }

        public double price
        {
            get { return _price; }
            set
            {
                _price = value;
               
            }
        }

        public int quantity
        {
           
            get { return _quantity; }
            set
            {
                int oldValue = _quantity;
                _quantity = value;
                OnPropertyChanged(quantity, oldValue);
            }
        }
        public object OldValue { get; set; }

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(int quantity, int oldvalue)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                double unit = this.price / Convert.ToDouble(oldvalue);
                this.price = unit * quantity;
            }
        }


        
    }
}
