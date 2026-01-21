Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports System.Threading.Tasks

Module EnderecoUtils

    Public Async Function BuscarEnderecoRandomAsync() As Task(Of (logradouro As String, bairro As String, cidade As String, uf As String))
        Using client As New HttpClient()
            Dim resposta = Await client.GetStringAsync("https://randomuser.me/api/?nat=BR")
            Dim json As JObject = JObject.Parse(resposta)
            Dim location = json("results")(0)("location")

            Dim logradouro = location("street")("name").ToString() & " " & location("street")("number").ToString()
            Dim bairro = "Bairro " & location("street")("number").ToString()
            Dim cidade = location("city").ToString()
            Dim uf = location("state").ToString()

            Return (logradouro, bairro, cidade, uf)
        End Using
    End Function

End Module
