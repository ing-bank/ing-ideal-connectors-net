# Certificate configuration

- `ideal_v3.cer` is the `AcquirerCertificate` and is not password protected. To convert into a form suitable to put into `appsettings.json`, remove the lines "-----BEGIN CERTIFICATE-----" and "-----END CERTIFICATE-----" and any line breaks and whitespaces.

- A file called `*.p12` is the `ClientCertificate`, most likely including the private key used to sign the messages. Convert the file as is to base64 encoding using the following command:

```bash
    openssl base64 -in  CkoIdeal.p12 -out CkoIdeal.p12.base64
```

and remove any line breaks and whitespaces. Use the resulting string as value for `ClientCertificate`.

- Since your `*.p12` file contains your private key it is most likely password protected. Use `ClientCertificatePassword` to specify the password to be used when loading the certificate.