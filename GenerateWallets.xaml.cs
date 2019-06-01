using System;
using System.Windows;
using System.Security;
using NBitcoin;
using Nethereum.HdWallet;

namespace FB_BIP44
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    class Functions
    {
        public Wordlist SelectedLanguage(string language)
        {
            Wordlist wordList = Wordlist.English;

            switch (language)
            {
                case "Chinese Simplified":
                    wordList = Wordlist.ChineseSimplified;
                    break;
                case "Chinese Traditional":
                    wordList = Wordlist.ChineseTraditional;
                    break;
                case "English":
                    wordList = Wordlist.English;
                    break;
                case "French":
                    wordList = Wordlist.French;
                    break;
                case "Japanese":
                    wordList = Wordlist.Japanese;
                    break;
                case "Spanish":
                    wordList = Wordlist.Spanish;
                    break;
            }

            return wordList;
        }
    }
    public partial class GenerateWallets : Window
    {
        public Wordlist wordList;

        public GenerateWallets()
        {
            InitializeComponent();

            cboLanguage.Items.Add("Chinese Simplified");
            cboLanguage.Items.Add("Chinese Traditional");
            cboLanguage.Items.Add("English");
            cboLanguage.Items.Add("French");
            cboLanguage.Items.Add("Japanese");
            cboLanguage.Items.Add("Portuguese Brazil");
            cboLanguage.Items.Add("Spanish");
            cboLanguage.SelectedValue = "English";

            cboCoin.Items.Add("BTC - Bitcoin (BIP-44)");
            cboCoin.Items.Add("ETH - Ethereum (BIP-44)");
            cboCoin.Items.Add("BTC - Bitcoin (BIP-49)");
            cboCoin.SelectedValue = "BTC - Bitcoin (BIP-44)";

            cboType.Items.Add("0 - Receiving");
            cboType.Items.Add("1 - Change");
            cboType.SelectedValue = "0 - Receiving";

            for (int i = 0; i < 100; i++)
            {
                cboAccount.Items.Add(i);
                cboIndex.Items.Add(i);
            }
            cboAccount.SelectedValue = 0;
            cboIndex.SelectedValue = 0;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            SecureString secureStr = new SecureString();
            for (int i = 0; i < txtRandom.ToString().Length; i++)
            {
                secureStr.AppendChar(txtRandom.ToString()[i]);
            }
            secureStr.MakeReadOnly();

            var functions = new Functions();
            wordList = functions.SelectedLanguage(cboLanguage.SelectedValue.ToString());
            RandomUtils.AddEntropy(secureStr.ToString());
            Mnemonic mnemo = new Mnemonic(wordList, WordCount.TwentyFour);
            txtMnemonic.Text = mnemo.ToString();
            btnCalculate.IsEnabled = true;
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            var functions = new Functions();
            wordList = functions.SelectedLanguage(cboLanguage.SelectedValue.ToString());
            Mnemonic mnemo = new Mnemonic(txtMnemonic.Text, wordlist: wordList);
            ExtKey hdRoot = new ExtKey();
            ExtKey key32;
            ExtPubKey pubKey32;
            hdRoot = mnemo.DeriveExtKey(txtPasswort.Text);
            txtRoot.Text = hdRoot.ToString(Network.Main);

            var wallet = new Wallet(txtMnemonic.Text, txtPasswort.Text);
            txtSeed.Text = wallet.Seed;

            switch(cboCoin.SelectedValue)
            {
                case "BTC - Bitcoin (BIP-44)":
                    ExtKey key44 = hdRoot.Derive(new NBitcoin.KeyPath("m/44'/0'/" + cboAccount.SelectedValue + "'/" + Convert.ToString(cboType.SelectedValue).Substring(0, 1) + "/" + cboIndex.SelectedValue));
                    ExtPubKey pubKey44 = key44.Neuter();
                    txtAddress.Text = key44.PrivateKey.PubKey.ToString(Network.Main);
                    txtPublicKey.Text = Convert.ToString(pubKey44.PubKey.ScriptPubKey).Substring(0, 66);
                    txtExtPublicKey.Text = pubKey44.ToString(Network.Main);
                    txtPrivateKey.Text = key44.PrivateKey.ToString(Network.Main);
                    key32 = hdRoot.Derive(new NBitcoin.KeyPath("m/44'/0'/" + cboAccount.SelectedValue + "'/" + Convert.ToString(cboType.SelectedValue).Substring(0, 1)));
                    pubKey32 = key32.Neuter();
                    txt32ExtPublicKey.Text = pubKey32.ToString(Network.Main);
                    break;

                case "ETH - Ethereum (BIP-44)":
                    int index = Convert.ToInt32(cboIndex.SelectedValue);
                    var account = wallet.GetAccount(index);
                    txtAddress.Text = account.Address;
                    txtPublicKey.Text = "";
                    txtExtPublicKey.Text = "";
                    txtPrivateKey.Text = account.PrivateKey;
                    break;

                case "BTC - Bitcoin (BIP-49)":
                    ExtKey key49 = hdRoot.Derive(new NBitcoin.KeyPath("m/49'/0'/" + cboAccount.SelectedValue + "'/" + Convert.ToString(cboType.SelectedValue).Substring(0, 1) + "/" + cboIndex.SelectedValue));
                    ExtPubKey pubKey49 = key49.Neuter();
                    txtAddress.Text = key49.PrivateKey.PubKey.WitHash.GetAddress(Network.Main).GetScriptAddress().ToString();
                    txtPublicKey.Text = Convert.ToString(pubKey49.PubKey.ScriptPubKey).Substring(0, 66);
                    txtExtPublicKey.Text = pubKey49.ToString(Network.Main);
                    txtPrivateKey.Text = key49.PrivateKey.ToString(Network.Main);
                    key32 = hdRoot.Derive(new NBitcoin.KeyPath("m/49'/0'/" + cboAccount.SelectedValue + "'/" + Convert.ToString(cboType.SelectedValue).Substring(0, 1)));
                    pubKey32 = key32.Neuter();
                    txt32ExtPublicKey.Text = pubKey32.ToString(Network.Main);
                    break;
            }
        }

        private void btnCopyAddress_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtAddress.Text);
        }

        private void btnCopyPublicKey_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtPublicKey.Text);
        }

        private void btnCopyExtPublicKey_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtExtPublicKey.Text);
        }

        private void btnCopyPrivateKey_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Please be careful - Private Keys should never be stored electronically and should only be used in a secure environment!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            Clipboard.SetText(txtPrivateKey.Text);
        }

        public string Words;

        private void btnExisting_Click(object sender, RoutedEventArgs e)
        {
            btnCalculate.IsEnabled = true;
            Words = "";

            for (int i = 1; i <= 24; i++)
            {
                EnterMnemonic dialog = new EnterMnemonic(i, cboLanguage.SelectedValue.ToString());

                dialog.ShowDialog();

                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                {
                    if (Words.Length == 0)
                    {
                        Words = dialog.txtOutput.Text;
                    }
                    else
                    {
                        Words = Words + " " + dialog.txtOutput.Text;
                    }
                }
                else
                {
                    if (Words.Length == 0)
                    {
                        Words = dialog.txtOutput.Text;
                    }
                    else
                    {
                        Words = Words + " " + dialog.txtOutput.Text;
                    }

                    txtMnemonic.Text = Words;
                    break;
                }
            }
        }

        private void btnCopy32ExtPublicKey_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txt32ExtPublicKey.Text);
        }
    }
}
