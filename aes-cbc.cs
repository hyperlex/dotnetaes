using System;
using System.IO;
using System.Security.Cryptography;


public class AesCbc
{
    const int AES_CBC_BLOCK_SIZE_BITS = 128; 
    const int AES_CBC_BLOCK_SIZE_BYTES = 16; 
    const int AES_256_KEY_SIZE_BYTES = 32;

    public static byte[] encrypt(Stream plaintextStream, byte[] key, byte[] iv)
    {
        /* input validation */
        if (plaintextStream == null)
        {
            throw new ArgumentNullException("plaintextStream");
        }
        if (key == null || key.Length != AES_256_KEY_SIZE_BYTES)
        {
            throw new ArgumentException("Invalid key length");
        }
        if (iv == null || iv.Length != AES_CBC_BLOCK_SIZE_BYTES)
        {
            throw new ArgumentException("Invalid IV length");
        }
        
        /* setup aes in cbc mode */
        using (Aes aes = Aes.Create())
        {
            /* these seem to be the defaults, but lets be explicit */
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            /* install key & iv for encryption */
            aes.Key = key;
            aes.IV = iv;

            /* do the encryption dance */
            using (MemoryStream sink = new MemoryStream())
            {
                using (CryptoStream cipher = new CryptoStream(sink, aes.CreateEncryptor(),
                                                              CryptoStreamMode.Write))
                {
                    plaintextStream.CopyTo(cipher);
                }
                return sink.ToArray();
            }
        }
    }
}

public class EncyptionDemo {
    /**
     * This is a small demo that will encrypt the text in the file
     * `plaintext.txt` in the current directory, and write the cipher text to
     * `ciphertext.hex`. A python 3 implementation of decryption can be found
     * in `decrypt.py`, that will read `ciphertext.hex` and write the decrypted
     * plain text to the console. Any padding problems should be exposed when running
     * the decryption program.
     *
     * Note that the key and iv that have been hard-coded here for demo
     * purposes also can be found in the `decrypt.py` decryption program.
     **/
    public static void Main(string[] args)
    {
        byte[] key = Hex2Bytes("1adbe7770c41ab6712486368801b4cb557b1951546ffeed1b78e088da1c4f46d");
        byte[] iv = Hex2Bytes("bce0fd9681ea31757e207a84683fff63");
        using (FileStream fIn = File.OpenRead("plaintext.txt"))
        {
            byte[] ciphertext = AesCbc.encrypt(fIn, key, iv);
            using (FileStream fOut = File.OpenWrite("ciphertext.hex")) {
                /*  there is probably a nicer way of doing this - just for demo anyway */
                using (StreamWriter w = new StreamWriter(fOut))
                {
                    w.Write(BitConverter.ToString(ciphertext).Replace("-", ""));
                }
            }
            Console.WriteLine($"Ciphertext length: {ciphertext.Length}");
        }
    }

    public static byte[] Hex2Bytes(String hex)
    {
        /* this is of course where I expect a hex to bin conversion method to live */
        return System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary.Parse(hex).Value;
    }
}
