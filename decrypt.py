# Note that this program uses cryptographic functions from the `pycryptodome` library.
import binascii

import Crypto.Cipher.AES as aes
import Crypto.Util.Padding as padding


def decrypt(ciphertext, key, iv):
    plaintext = aes.new(key, aes.MODE_CBC, iv).decrypt(ciphertext)
    return padding.unpad(plaintext, aes.block_size)


if __name__ == '__main__':
    # Hard-coded key and iv matching those in the C# demo program.
    key =  binascii.unhexlify(b'1adbe7770c41ab6712486368801b4cb557b1951546ffeed1b78e088da1c4f46d')
    iv = binascii.unhexlify(b'bce0fd9681ea31757e207a84683fff63')
    # Load ciphertext to memory.
    ciphertext = binascii.unhexlify(open('ciphertext.hex', 'rb').read())
    print(decrypt(ciphertext, key, iv))
