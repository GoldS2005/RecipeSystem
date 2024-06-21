using RecipeSystem.DataBase;
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
using System.Windows.Shapes;

namespace RecipeSystem
{
    /// <summary>
    /// Логика взаимодействия для DishView.xaml
    /// </summary>
    public partial class DishView : Window
    {
         RecipesEntities1 entities;

       

        public ObservableCollection<Dish> DishesTrue { get; set; }
        public ObservableCollection<Dish> DishesFalse { get; set; }

        public DishView()
        {
            InitializeComponent();


            entities = new RecipesEntities1();

            DishesTrue = new ObservableCollection<Dish>(entities.Dishes.Where(d => d.Status == true));
            DishesFalse = new ObservableCollection<Dish>(entities.Dishes.Where(d => d.Status == false));

            DataContext = this;
        }

        private void IngredientButton_Click(object sender, RoutedEventArgs e)
        {
            IngredientView ingredientView = new IngredientView(entities);
            ingredientView.Show();
            Hide();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (FalseListView.SelectedItems.Count > 0)
            {
                var selectedDish = FalseListView.SelectedItems[0] as Dish;

                if (selectedDish != null)
                {


                    DishesFalse.Remove(selectedDish);
                    entities.Dishes.Remove(selectedDish);
                    entities.SaveChanges();


                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DishPage dishPage = new DishPage(entities);
            dishPage.Show();
            Hide();

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDish = (Dish)FalseListView.SelectedItem;
            if (selectedDish != null)
            {
                selectedDish.Status = true;
                entities.SaveChanges();
                RefreshDishes();
            }


        }

        private void NoReadyButton_Click(object sender, RoutedEventArgs e)
        {

            var selectedDish = (Dish)TrueListView.SelectedItem;
            if (selectedDish != null)
            {
                selectedDish.Status = false;
                entities.SaveChanges();
                RefreshDishes();
            }
        }

        private void RefreshDishes()
        {
            DishesTrue = new ObservableCollection<Dish>(entities.Dishes.Where(d => d.Status == true));
            DishesFalse = new ObservableCollection<Dish>(entities.Dishes.Where(d => d.Status == false));
            TrueListView.ItemsSource = DishesTrue;
            FalseListView.ItemsSource = DishesFalse;
        }
    }
}
