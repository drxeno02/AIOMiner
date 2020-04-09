Module Wallets
    Function WalletAddress(ByRef Coin As String)
        'AIOMiner wallets, to be used when benchmarking

        Dim Wallet As String
        Wallet = ""

        If Coin = "Ethereum" Then
            Wallet = ""
        End If

        If Coin = "BitcoinZ" Then
            Wallet = ""
        End If


        Return Wallet

    End Function
End Module
