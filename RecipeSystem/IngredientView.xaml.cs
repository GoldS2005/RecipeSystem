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
    /// Логика взаимодействия для IngredientView.xaml
    /// </summary>
    public partial class IngredientView : Window
    {
        RecipesEntities1 entities;

        public ObservableCollection<DataBase.Ingredient> Ingredients { get; set; }
        public IngredientView(RecipesEntities1 entities)
        {
            InitializeComponent();

            this.entities = entities;

            Ingredients = new ObservableCollection<Ingredient>(entities.Ingredients);

            IngredientBlock.DataContext = new Ingredient();
           
            DataContext = this;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Ingredient ingredient = IngredientBlock.DataContext as Ingredient;
            if (ingredient.TitleIng.Length == 0)
            {
                MessageBox.Show("Введите ингредиенты");
                return;
            }

            Ingredients.Add(ingredient);
            entities.Ingredients.Add(ingredient);
            entities.SaveChanges();

            IngredientBlock.DataContext = new Ingredient();

            
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DishView dishView = new DishView();
            dishView.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientListView.SelectedItems.Count > 0)
            {
                var selectedIngredient = IngredientListView.SelectedItems[0] as Ingredient;

                if (selectedIngredient != null)
                {


                    Ingredients.Remove(selectedIngredient);
                    entities.Ingredients.Remove(selectedIngredient);
                    entities.SaveChanges();


                }
            }
        }
    }
}
