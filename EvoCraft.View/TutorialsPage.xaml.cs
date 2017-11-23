using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace View
{
    /// <summary>
    /// Interaction logic for TutorialsPage.xaml
    /// </summary>
    public partial class TutorialsPage : Page
    {
        BitmapImage ActualTutorial;
        int ActualTutorialCounter = 0;
        List<BitmapImage> listOfTutorials = new List<BitmapImage>();

        public TutorialsPage()
        {
            InitializeComponent();

            listOfTutorials.Add((BitmapImage)FindResource("TutorialSlide0BitmapImage"));
            listOfTutorials.Add((BitmapImage)FindResource("TutorialSlide1BitmapImage"));
            listOfTutorials.Add((BitmapImage)FindResource("TutorialSlide2BitmapImage"));
            listOfTutorials.Add((BitmapImage)FindResource("TutorialSlide3BitmapImage"));
            listOfTutorials.Add((BitmapImage)FindResource("TutorialSlide4BitmapImage"));
            listOfTutorials.Add((BitmapImage)FindResource("TutorialSlide5BitmapImage"));
            listOfTutorials.Add((BitmapImage)FindResource("TutorialSlide6BitmapImage"));
            listOfTutorials.Add((BitmapImage)FindResource("TutorialSlide7BitmapImage"));
            listOfTutorials.Add((BitmapImage)FindResource("TutorialSlide8BitmapImage"));
            listOfTutorials.Add((BitmapImage)FindResource("TutorialSlide9BitmapImage"));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(2));
            MainGrid.BeginAnimation(Image.OpacityProperty, fadeInAnimation);
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (ActualTutorialCounter > 0)
            {
                ActualTutorialCounter--;
                MainImage.Source = listOfTutorials[ActualTutorialCounter];
            }
            
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu page = new MainMenu();
            this.NavigationService.Navigate(page);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (ActualTutorialCounter < listOfTutorials.Count - 1)
            {
                ActualTutorialCounter++;
                MainImage.Source = listOfTutorials[ActualTutorialCounter];
            }           
        }
    }
}
