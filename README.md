# FB_BIP44

## What is FB_BIP44?
FB_BIP44 is a C# WPF implementation to generate [BIP-32](https://github.com/bitcoin/bips/blob/master/bip-0032.mediawiki) / [BIP-39](https://github.com/bitcoin/bips/blob/master/bip-0039.mediawiki) / [BIP-44](https://github.com/bitcoin/bips/blob/master/bip-0044.mediawiki) compliant wallets for Bitcoin (BTC) and Ethereum (ETH).

## Background
So called hierarchical deterministic wallets (or HD Wallets) are one way to manage wallets for crypto currencies (for a comprehensive overview of options see e.g. [Storing bitcoins](https://en.bitcoin.it/wiki/Storing_bitcoins)). They are particularly suitable because the mnemonic words or the seed can be stored offline, e.g. on paper or an even more secure method. They also protect privacy because a new address can be generated for each transaction without increasing the risk of loss (all addresses can be retrieved with the same seed). In a nutshell: One of the most secure forms of storing your own crypto currencies.

HD wallets can be managed on devices provided by Trezor, Ledger and others or on your own computer using tools like FB_BIP44.

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
