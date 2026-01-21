Imports System.Text.RegularExpressions
Imports System.Globalization

Module ValidationUtils

    Private ReadOnly UFsValidas As HashSet(Of String) =
        New HashSet(Of String) From {
            "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA",
            "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN",
            "RS", "RO", "RR", "SC", "SP", "SE", "TO"
        }

    ' --- Validação de e-mail ---
    Public Function ValidarEmail(email As String) As Boolean
        If String.IsNullOrWhiteSpace(email) Then Return False
        Dim pattern As String = "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"
        Return Regex.IsMatch(email.Trim(), pattern)
    End Function

    ' --- Validação de valor positivo ---
    Public Function ValidarValor(valor As String) As Boolean
        Dim v As Decimal
        Return Decimal.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, v) AndAlso v >= 0
    End Function

    ' --- Validação de telefone fixo (10 dígitos) ---
    Public Function ValidarTelefone(telefone As String) As Boolean
        If String.IsNullOrWhiteSpace(telefone) Then Return False
        Dim numeros As String = Regex.Replace(telefone, "[^0-9]", "")
        Return numeros.Length = 10
    End Function

    ' --- Validação de celular (11 dígitos) ---
    Public Function ValidarCelular(celular As String) As Boolean
        If String.IsNullOrWhiteSpace(celular) Then Return False
        Dim numeros As String = Regex.Replace(celular.Trim(), "[^0-9]", "")
        Return numeros.Length = 11
    End Function

    ' --- Normaliza UF (nome → sigla / sigla → sigla) ---
    Public Function ObterUF(estado As String) As String
        If String.IsNullOrWhiteSpace(estado) Then Return ""

        Dim e As String = estado.Trim().ToUpper()

        ' Se já for sigla válida
        If UFsValidas.Contains(e) Then Return e

        Select Case e
            Case "ACRE" : Return "AC"
            Case "ALAGOAS" : Return "AL"
            Case "AMAPÁ" : Return "AP"
            Case "AMAZONAS" : Return "AM"
            Case "BAHIA" : Return "BA"
            Case "CEARÁ" : Return "CE"
            Case "DISTRITO FEDERAL" : Return "DF"
            Case "ESPÍRITO SANTO" : Return "ES"
            Case "GOIÁS" : Return "GO"
            Case "MARANHÃO" : Return "MA"
            Case "MATO GROSSO" : Return "MT"
            Case "MATO GROSSO DO SUL" : Return "MS"
            Case "MINAS GERAIS" : Return "MG"
            Case "PARÁ" : Return "PA"
            Case "PARAÍBA" : Return "PB"
            Case "PARANÁ" : Return "PR"
            Case "PERNAMBUCO" : Return "PE"
            Case "PIAUÍ" : Return "PI"
            Case "RIO DE JANEIRO" : Return "RJ"
            Case "RIO GRANDE DO NORTE" : Return "RN"
            Case "RIO GRANDE DO SUL" : Return "RS"
            Case "RONDÔNIA" : Return "RO"
            Case "RORAIMA" : Return "RR"
            Case "SANTA CATARINA" : Return "SC"
            Case "SÃO PAULO" : Return "SP"
            Case "SERGIPE" : Return "SE"
            Case "TOCANTINS" : Return "TO"
            Case Else : Return ""
        End Select
    End Function

    ' --- Validação de UF ---
    Public Function ValidarUF(uf As String) As Boolean
        If String.IsNullOrWhiteSpace(uf) Then Return False
        Return UFsValidas.Contains(uf.Trim().ToUpper())
    End Function

    ' --- Validação de data (dd/MM/yyyy ou com hora) ---
    Public Function ValidarData(valor As String) As Boolean
        If String.IsNullOrWhiteSpace(valor) Then Return False

        Dim dataStr As String = valor.Split(" "c)(0)

        Dim data As DateTime
        Return DateTime.TryParseExact(
            dataStr,
            "dd/MM/yyyy",
            CultureInfo.GetCultureInfo("pt-BR"),
            DateTimeStyles.None,
            data
        )
    End Function

    ' --- Validação de CPF (não pode ser vazio) ---
    Public Function ValidarCPF(cpf As String) As Boolean
        Return Not String.IsNullOrWhiteSpace(cpf)
    End Function

End Module
