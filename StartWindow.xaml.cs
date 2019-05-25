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

namespace FB_BIP44
{
    /// <summary>
    /// Interaktionslogik für StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void btnGeneratePulicKeys_Click(object sender, RoutedEventArgs e)
        {
            GeneratePublicKeys wnd = new GeneratePublicKeys();
            wnd.Show();
        }

        private void btnGenerateWallets_Click(object sender, RoutedEventArgs e)
        {
            GenerateWallets wnd = new GenerateWallets();
            wnd.Show();
        }
    }
}
