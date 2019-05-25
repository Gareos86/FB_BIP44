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
using NBitcoin;
using Nethereum.HdWallet;
using Nethereum.Web3.Accounts;

namespace FB_BIP44
{
    /// <summary>
    /// Interaktionslogik für GeneratePublicKeys.xaml
    /// </summary>
    public partial class GeneratePublicKeys : Window
    {
        public GeneratePublicKeys()
        {
            InitializeComponent();

            cboCoin.Items.Add("BTC - Bitcoin");
            cboCoin.SelectedValue = "BTC - Bitcoin";


            for (int i = 0; i < 100; i++)
            {
                cboIndex.Items.Add(i);
            }
            cboIndex.SelectedValue = 0;

        }

        private void btnCopyAddress_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtAddress.Text);
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            //var wallet = new Wallet(txtMnemonic.Text, txtPasswort.Text);

            switch (cboCoin.SelectedValue)
            {
                case "BTC - Bitcoin":
                    BitcoinExtPubKey bitcoinExtPubKey = new BitcoinExtPubKey(txtInput.Text, Network.Main);
                    ExtPubKey extPubKey = new ExtPubKey(bitcoinExtPubKey.ToBytes());
                    txtAddress.Text = extPubKey.Derive(Convert.ToUInt32(cboIndex.SelectedValue)).PubKey.ToString(Network.Main);
                    break;
            }
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnCalculate.IsEnabled = true;
        }
    }
}