﻿using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MenuGridFinal
{
    /// <summary>
    /// Interaction logic for LogoScreen.xaml
    /// </summary>
    public partial class LogoScreen : Page
    {
        DoubleAnimation fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(3));
        DoubleAnimation fadeOutAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(2));

        public LogoScreen()
        {
            Sounds.Startup();
            InitializeComponent();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {           
            fadeInAnimation.Completed += PresentsFadeIn;
            LizzardLogo.BeginAnimation(Image.OpacityProperty, fadeInAnimation);
        }

        private void PresentsFadeIn(object sender, EventArgs e)
        {
            fadeInAnimation.Completed -= PresentsFadeIn;
            fadeInAnimation.Completed += LizzardAndPresentsFadeOut;
            PresentsTextBlock.BeginAnimation(Image.OpacityProperty, fadeInAnimation);
        }

        private void LizzardAndPresentsFadeOut(object sender, EventArgs e)
        {
            fadeInAnimation.Completed -= LizzardAndPresentsFadeOut;
            fadeOutAnimation.Completed += EvoCraftLogoFadeIn;
            LizzardLogo.BeginAnimation(Image.OpacityProperty, fadeOutAnimation);
            PresentsTextBlock.BeginAnimation(Image.OpacityProperty, fadeOutAnimation);
        }

        private void EvoCraftLogoFadeIn(object sender, EventArgs e)
        {
            fadeOutAnimation.Completed -= EvoCraftLogoFadeIn;
            fadeOutAnimation.Completed += EvoCraftLogoFadeOut;
            EvoCraftLogo.BeginAnimation(Image.OpacityProperty, fadeInAnimation);
        }

        private void EvoCraftLogoFadeOut(object sender, EventArgs e)
        {
            fadeOutAnimation.Completed -= EvoCraftLogoFadeOut;
            fadeOutAnimation.Completed += NextPage;
            EvoCraftLogo.BeginAnimation(Image.OpacityProperty, fadeOutAnimation);
        }
    
        private void NextPage(object sender, EventArgs e)
        {
            fadeOutAnimation.Completed -= NextPage;
            MainMenu mainmenu = new MainMenu();
            this.NavigationService.Navigate(mainmenu);
        }
    }
}
