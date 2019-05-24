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
using WPFTextBoxAutoComplete;
using NBitcoin;
using System.Collections;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace FB_BIP44
{
    /// <summary>
    /// Interaktionslogik für EnterMnemonic.xaml
    /// </summary>
    public partial class EnterMnemonic : Window
    {
        public Wordlist wordList;
        public string selectedLanguage;
        public int i;

        public EnterMnemonic(int _i, string _selectedLanguage)
        {
            InitializeComponent();
            selectedLanguage = _selectedLanguage;
            i = _i;
            lblWord.Content = "Enter word #" + _i;
            lblSelectedWord.Content= "Selected word #" + _i;
        }

        private void txtInput_KeyUp(object sender, KeyEventArgs e)
        {
            bool found = false;
            var border = (txtSuggestions.Parent as ScrollViewer).Parent as Border;

            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear   
                txtSuggestions.Children.Clear();
                border.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                border.Visibility = System.Windows.Visibility.Visible;
            }

            // Clear the list   
            txtSuggestions.Children.Clear();

            // Add the result   
            var functions = new Functions();
            wordList = functions.SelectedLanguage(selectedLanguage);

            foreach (var obj in wordList.GetWords())
            {
                if (obj.ToLower().StartsWith(query.ToLower()))
                {
                    // The word starts with this... Autocomplete must work   
                    addItem(obj);
                    found = true;
                }
            }

            if (!found)
            {
                txtSuggestions.Children.Add(new TextBlock() { Text = "No words found." });
            }
        }

        private void addItem(string text)
        {
            TextBlock block = new TextBlock();

            // Add the text   
            block.Text = text;

            // A little style...   
            block.Margin = new Thickness(2, 3, 2, 3);
            block.Cursor = Cursors.Hand;

            // Mouse events   
            block.MouseLeftButtonUp += (sender, e) =>
            {
                txtOutput.Text = (sender as TextBlock).Text;

                if (i != 24)
                {
                    btnNext.IsEnabled = true;
                }

                if (i == 12 || i == 15 || i == 18 || i == 21 || i == 24)
                {
                    btnClose.IsEnabled = true;
                }
            };

            block.MouseEnter += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.PeachPuff;
            };

            block.MouseLeave += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.Transparent;
            };

            // Add to the panel   
            txtSuggestions.Children.Add(block);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (txtOutput.Text.Length == 0)
            {
                MessageBox.Show("Please add the last word before closing this window.", "Missing word", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DialogResult = false;
            }
        }
    }
}
