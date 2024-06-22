﻿using RecipeSystem.DataBase;
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
    /// Логика взаимодействия для DishPage.xaml
    /// </summary>
    public partial class DishPage : Window
    {
        RecipesEntities1 entities;


        public ObservableCollection<DataBase.Ingredient> Ingredients { get; set; }

        public ObservableCollection<Ingredient> RecipeIngredients { get; set; }

        public ObservableCollection<Dish> Dishes { get; set; }

        public Ingredient SelectedIngredient { get; set; }
        public Ingredient SelectedRecipeIngredient { get; set; }

        public DishPage(RecipesEntities1 entities)
        {
            InitializeComponent();


            this.entities = entities;

            Ingredients = new ObservableCollection<Ingredient>(entities.Ingredients.ToList());

            RecipeIngredients = new ObservableCollection<Ingredient>();

            Dishes = new ObservableCollection<Dish>(entities.Dishes);





            DataContext = this;


        }



        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DishView dishView = new DishView();
            dishView.Show();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Dish newDish = new Dish
            {
                TitleDish = nameValue.Text,


                CaloriesDish = RecipeIngredients.Sum(ing => ing.CaloriesIng),
                ProteinsDish = RecipeIngredients.Sum(ing => ing.ProteinsIng),
                FatsDish = RecipeIngredients.Sum(ing => ing.FatsIng),
                СarbohydratesDish = RecipeIngredients.Sum(ing => ing.СarbohydratesIng),
                Status = false
            };


            foreach (var ingredient in RecipeIngredients)
            {

            }


            Dishes.Add(newDish);
            entities.Dishes.Add(newDish);

            entities.SaveChanges();



            RecipeIngredients.Clear();


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
