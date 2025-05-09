using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sokoban
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        



        public int _characterX
        {
            get { return (int)GetValue(_characterXProperty); }
            set { SetValue(_characterXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for _characterX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty _characterXProperty =
            DependencyProperty.Register("_characterX", typeof(int), typeof(MainWindow), new PropertyMetadata(2));



        public int _characterY
        {
            get { return (int)GetValue(_characterYProperty); }
            set { SetValue(_characterYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for _characterY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty _characterYProperty =
            DependencyProperty.Register("_characterY", typeof(int), typeof(MainWindow), new PropertyMetadata(2));



        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_MoveUp(object sender, RoutedEventArgs e)
        {
            _characterY--;

        }

        private void Button_MoveLeft(object sender, RoutedEventArgs e)
        {
            _characterX--;
        }

        private void Button_MoveDown(object sender, RoutedEventArgs e)
        {
            _characterY++;
        }

        private void Button_MoveRight(object sender, RoutedEventArgs e)
        {
            _characterX++;
        }
    }
}