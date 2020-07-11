Module Wallets
    Function WalletAddress(ByRef Coin As String)
        'AIOMiner wallets, to be used when benchmarking

        Dim Wallet As String
        Wallet = ""

        If Coin = "Ethereum" Then
            Wallet = "0x1efccad435663a013a6dc4cc2d14ae5b93e69332"
        End If

        If Coin = "BitcoinZ" Then
            Wallet = "t1dbq8sgJtHVFxq1tTFnZaHhSUQHqTUhUNs"
        End If


        Return Wallet

    End Function
End Module
