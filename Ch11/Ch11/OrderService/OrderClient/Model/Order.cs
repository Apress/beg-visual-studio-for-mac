using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrderClient.Model
{
   public class Order: INotifyPropertyChanged
   {
        int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        DateTime orderDate;
        public DateTime OrderDate
        {
            get
            {
                return orderDate;
            }
            set
            {
                orderDate = value;
                OnPropertyChanged();
            }
        }

        string orderDescription;
        public string OrderDescription
        {
            get
            {
                return orderDescription;
            }
            set
            {
                orderDescription = value;
                OnPropertyChanged();
            }
        }

        string customerName;
        public string CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                customerName = value;
                OnPropertyChanged();
            }
        }

        public Order()
        {
            this.OrderDate = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // CallerMemberName supplies the name of the caller member and
        // simplifies the invocation to OnPropertyChanged
        private void OnPropertyChanged([CallerMemberName]string PropertyName="")
        {
            // Use the null conditional operator to execute Invoke only
            // if PropertyChanged is not null
            this.PropertyChanged?.
                Invoke(this, 
                new PropertyChangedEventArgs(PropertyName));
        }
    }
}
