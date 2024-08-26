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
    /// Логика взаимодействия для DishEdit.xaml
    /// </summary>
    public partial class DishEdit : Window
    {
        RecipesEntities1 entities;

       

        public ObservableCollection<DataBase.Ingredient> Ingredients { get; set; }

        public ObservableCollection<Ingredient> RecipeIngredients { get; set; }

       

        public Ingredient SelectedIngredient { get; set; }
        public Ingredient SelectedRecipeIngredient { get; set; }

        public Dish CurrentDish { get; set; }

        public Tracking CurrentTracking { get; set; }

        public DishEdit(RecipesEntities1 entities, Dish SelectedDish)
        {
            InitializeComponent();

            this.entities = entities;

            CurrentDish = SelectedDish;

            var ingredientsInDish = entities.Trackings.Where(gsi => gsi.DishId == SelectedDish.DishID).Select(gsi => gsi.IngredientId).ToList();

            Ingredients = new ObservableCollection<Ingredient>(entities.Ingredients.Where(ingredient => !ingredientsInDish.Contains(ingredient.IngredientID)).ToList());

            RecipeIngredients = new ObservableCollection<Ingredient>(entities.Trackings.Where(gsi => gsi.DishId == SelectedDish.DishID).Select(gsi => entities.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientID == gsi.IngredientId)).ToList());

           



            DataContext = this;

            nameValue.Text = CurrentDish.TitleDish;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DishView dishView = new DishView();
            dishView.Show();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            CurrentDish.TitleDish = nameValue.Text.Trim();
            CurrentDish.CaloriesDish = RecipeIngredients.Sum(ing => ing.CaloriesIng);
            CurrentDish.ProteinsDish = RecipeIngredients.Sum(ing => ing.ProteinsIng);
            CurrentDish.FatsDish = RecipeIngredients.Sum(ing => ing.FatsIng);
            CurrentDish.СarbohydratesDish = RecipeIngredients.Sum(ing => ing.СarbohydratesIng);

            var ingredientsToDelete = entities.Trackings.Where(gsi => gsi.DishId == CurrentDish.DishID);
            entities.Trackings.RemoveRange(ingredientsToDelete);

            foreach (var ingredient in RecipeIngredients)
            {

                Tracking tracking = new Tracking()
                {
                    DishId = CurrentDish.DishID,
                    IngredientId = ingredient.IngredientID,

                };

                entities.Trackings.Add(tracking);
            }


            entities.SaveChanges();

            


            this.Close();
            DishView dishView = new DishView();
            dishView.Show();

        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {

            if (SelectedIngredient != null)
            {
                RecipeIngredients.Add(SelectedIngredient);
                Ingredients.Remove(SelectedIngredient);
                SelectedIngredient = null;
            }

        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedRecipeIngredient != null)
            {
                Ingredients.Add(SelectedRecipeIngredient);
                RecipeIngredients.Remove(SelectedRecipeIngredient);
                SelectedRecipeIngredient = null;
            }
        }
    }
}
