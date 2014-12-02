using System;
using System.Collections.Generic;
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

namespace Project1_final
{
    /// <summary>
    /// Interaction logic for player.xaml
    /// </summary>
    public partial class player : Window
    {
        public player()
        {
            InitializeComponent();
        }

        private void bt_pause_full_Click(object sender, RoutedEventArgs e)
        {
            fullscreen.Pause();
        }

        private void bt_play_full_Click(object sender, RoutedEventArgs e)
        {
            fullscreen.Play();
        }

        private void bt_stop_full_Click(object sender, RoutedEventArgs e)
        {
            fullscreen.Stop();
        }

        private void volume_full_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            fullscreen.Volume = volume_full.Value;
        }

        private void Gridfull_MouseEnter(object sender, MouseEventArgs e)
        {
            navfull.Visibility = Visibility;
        }

        private void Gridfull_MouseLeave(object sender, MouseEventArgs e)
        {
            navfull.Visibility = Visibility.Hidden;
        }

        private void load(object sender, RoutedEventArgs e)
        {
            fullscreen.Play();
        }
    }
}
