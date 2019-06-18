# AES-256 Encryption Demo in C#

To get started, a nix shell has been provided that contains required dependencies to run the demo.

```
$ nix-shell          # enter a nix shell with needed dependencies
$ csc aes-cbc.cs     # compile demo program
$ mono aes-cbc.exe   # encrypt plaintext.txt and write ciphertext.hex
$ python decrypt.py  # decrypt ciphertext.hex and print plain text to the console
```
