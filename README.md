# FB_BIP44

## What is FB_BIP44?
FB_BIP44 is a C# WPF implementation to generate [BIP-32](https://github.com/bitcoin/bips/blob/master/bip-0032.mediawiki) / [BIP-39](https://github.com/bitcoin/bips/blob/master/bip-0039.mediawiki) / [BIP-44](https://github.com/bitcoin/bips/blob/master/bip-0044.mediawiki) compliant wallets for Bitcoin (BTC) and Ethereum (ETH).

## How to use?
The tool uses the following NuGet Packages, please install them first:
- [NBitcoin](https://github.com/MetacoSA/NBitcoin)
- [Nethereum.HDWallet](https://github.com/Nethereum/Nethereum)

You should use FB_BIP only on a secure computer without access to the Internet. Once the mnemonic words has been generated, write them down and keep them in a safe place (never online!). FB_BIP should then be used to generate a new address for each transaction (not for security reasons but to increase privacy). The private keys do not need to be stored as they can be retrieved later.

NEVER LOSE THE MNEMONIC WORDS – OTHERWISE YOU WON'T BE ABLE TO GET ACCESS TO YOUR COINS ANYMORE!

## Useful links
An online Mnemonic Code Converter including more coins is available under the following link: https://iancoleman.io/bip39/

## License
The MIT License
