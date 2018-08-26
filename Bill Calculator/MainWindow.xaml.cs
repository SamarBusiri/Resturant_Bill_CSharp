using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bill_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // creating ObservbaleCollections of type class Food for ComboBoxes binding
        ObservableCollection<Food> Beverage = new ObservableCollection<Food>();
        ObservableCollection<Food> Appetizer = new ObservableCollection<Food>();
        ObservableCollection<Food> MainCourse = new ObservableCollection<Food>();
        ObservableCollection<Food> Dessert = new ObservableCollection<Food>();

        //Creating ObservableCollection of type class BillCall for DataGrid binding
        ObservableCollection<BillCal> orders = new ObservableCollection<BillCal>();

        // Decalare and initialize variables for the Final Bill calculation 
         double tax = 0.13;
         double totalTax = 0;
         double subtotal = 0;
         double totalBill = 0;
        

        public MainWindow()
        {
            InitializeComponent();
            //Binding the ComboBoxes
            BeverageComboBox.ItemsSource = Beverage;
            AppetizerComboBox.ItemsSource = Appetizer;
            MainCourseComboBox.ItemsSource = MainCourse;
            DessertComboBox.ItemsSource = Dessert;

            //Starting the GetFood in the Main method to assign the ComboBoxes items when running the UI
            DataContext = this.GetFood();

        }

        // the ComboBoxes binding mwthod
        public ObservableCollection<Food> GetFood()
        {
          
            //Beverage category
            Beverage.Add(new Food() { foodName = "Soda", foodPrice = 1.95 });
            Beverage.Add(new Food() { foodName = "Tea", foodPrice = 1.50 });
            Beverage.Add(new Food() { foodName = "Coffe", foodPrice = 1.25 });
            Beverage.Add(new Food() { foodName = "Mineral Water", foodPrice = 2.95 });
            Beverage.Add(new Food() { foodName = "Juice", foodPrice = 2.50 });
            Beverage.Add(new Food() { foodName = "Milk", foodPrice = 1.50 });
            //Appetizer category
            Appetizer.Add(new Food() { foodName = "Buffalo Wings", foodPrice = 5.95 });
            Appetizer.Add(new Food() { foodName = "Buffalo Fingers", foodPrice = 6.95 });
            Appetizer.Add(new Food() { foodName = "Potato Skins", foodPrice = 8.95 });
            Appetizer.Add(new Food() { foodName = "Nachos", foodPrice = 8.95 });
            Appetizer.Add(new Food() { foodName = "Mushroom Caps", foodPrice = 10.95 });
            Appetizer.Add(new Food() { foodName = "Shrimp Cocktail", foodPrice = 12.95 });
            Appetizer.Add(new Food() { foodName = "Chips and Salsa", foodPrice = 6.95 });
            //MainCourse category
            MainCourse.Add(new Food() { foodName = "Seafood Alferdo", foodPrice = 15.95 });
            MainCourse.Add(new Food() { foodName = "Chicken Alferdo", foodPrice = 13.95 });
            MainCourse.Add(new Food() { foodName = "Chicken Picatta", foodPrice = 13.95 });
            MainCourse.Add(new Food() { foodName = "Turkey Club", foodPrice = 11.95 });
            MainCourse.Add(new Food() { foodName = "Lobster Pie", foodPrice = 19.95 });
            MainCourse.Add(new Food() { foodName = "Prime Rib", foodPrice = 20.95 });
            MainCourse.Add(new Food() { foodName = "Shrimp Scampi", foodPrice = 18.95 });
            MainCourse.Add(new Food() { foodName = "Turkey Dinner", foodPrice = 13.95 });
            MainCourse.Add(new Food() { foodName = "Stuffed Chicken", foodPrice = 14.95 });
            //Dessert category
            Dessert.Add(new Food() { foodName = "Sundae", foodPrice = 3.95 });
            Dessert.Add(new Food() { foodName = "Carrot", foodPrice = 5.95 });
            Dessert.Add(new Food() { foodName = "Mud Pie", foodPrice = 4.95 });
            Dessert.Add(new Food() { foodName = "Apple Crisp", foodPrice = 5.95 });

            return Beverage;
            return Appetizer;
            return MainCourse;
            return Dessert;
        }

        public void getValue(string item, double price)
        {
           
            bool found = false;
             
            //Loop to check it the new chosen Item already in the DataGrid orders, if so the price and the quantity will be modified
            foreach (BillCal element in orders)
            {
                if (element.foodName == item)
                {
                    found = true;
                    element.foodName = item;
                    double unitprice = element.price / element.quantity;
                    element.quantity = element.quantity + 1;
                    element.price = element.quantity * unitprice;
                    break;
                }
            }

            if (found == false)
            {
                orders.Add(new BillCal { foodName = item, quantity = 1, price = price });
            }
            dgBill.ItemsSource = null;
            dgBill.ItemsSource = orders;

            //Calling the Bill_calculator method
            Bill_Calculator();

        }

        private void Bill_Calculator()
        {
            totalBill = 0;
            subtotal = 0;
            totalTax = 0;

            totalBill = 0;
            subtotal = 0;
            totalTax = 0;
            // calculating the final bill
            foreach (BillCal element in orders)
            {

                subtotal += element.price;
                totalTax += element.price * tax;
                totalBill += element.price + (element.price * tax);
                

            }

            // assigning the calculated value to the lables
            subAmountLabel.Content =  "$" +Convert.ToString(subtotal);
            taxLabel.Content = "$" + Convert.ToString(totalTax);
            totalLabel.Content =  "$" +Convert.ToString(totalBill);

        }


        //event handler method for ComboBoxes
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            //Crearting an object of type class Food to assign the 
            Food chosenFood = new Food();

            if (sender == BeverageComboBox)
            {
                chosenFood = (Food)BeverageComboBox.SelectedItem;
                if (BeverageComboBox.SelectedIndex != -1)
                { 

                getValue(chosenFood.foodName, chosenFood.foodPrice);
                BeverageComboBox.SelectedIndex = -1;

                }

             }else if (sender == MainCourseComboBox)
            {
                chosenFood = (Food)MainCourseComboBox.SelectedItem;
                if (MainCourseComboBox.SelectedIndex != -1)
                {
                    getValue(chosenFood.foodName, chosenFood.foodPrice);
                    MainCourseComboBox.SelectedIndex = -1;
                }
            }
            if (sender == DessertComboBox)
            {
                chosenFood = (Food)DessertComboBox.SelectedItem;
                if (DessertComboBox.SelectedIndex != -1)
                {
                    getValue(chosenFood.foodName, chosenFood.foodPrice);
                    DessertComboBox.SelectedIndex = -1;
                }
            }
            if (sender == AppetizerComboBox)
            {
                chosenFood = (Food)AppetizerComboBox.SelectedItem;
                if (AppetizerComboBox.SelectedIndex != -1)
                {
                    getValue(chosenFood.foodName, chosenFood.foodPrice);
                    AppetizerComboBox.SelectedIndex = -1;
                }
            }
        }

        //method for the DeleteItem Button
        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {

            orders.Remove((BillCal)dgBill.SelectedItem);
       
        }

        //event handler for the Datagride change
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgBill.Items.Refresh();

            Bill_Calculator();
        }

        //method for the Clear Button
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

            totalBill = 0;
            subtotal = 0;
            totalTax = 0;

            dgBill.ItemsSource = null;

            subAmountLabel.Content = " ";
            taxLabel.Content = " ";
            totalLabel.Content = " ";

            orders.Clear();
            

        }

    }

}
