# Projeto de Importação VB.NET com PostgreSQL e Logs

## Descrição

Esta aplicação em **VB.NET** realiza a importação de arquivos Excel para um banco de dados **PostgreSQL**, com validação de dados e geração de logs. É voltada para ambientes que exigem integridade dos dados e acompanhamento em tempo real do processo de importação.

O projeto gera **logs em arquivo TXT** e registra **logs no banco**, possibilitando posteriormente a criação de uma página web para visualização.

---

## Funcionalidades

1. **Importação de arquivos Excel (.xlsx)**
   - Leitura de planilhas usando **ClosedXML**.
   - Suporte a colunas: CPF, Telefone, Celular, E-mail, UF, Valor, Vencimento, Parcela, Endereço.

2. **Validações de dados**
   - CPF obrigatório e válido.
   - E-mail válido.
   - Telefone e celular válidos.
   - UF válido.
   - Valores numéricos positivos.
   - Datas no formato `dd/MM/yyyy`.

3. **Geração de logs**
   - Registro de linhas ignoradas em **arquivo TXT** (`importacao_ignorada.log`).
   - Registro de logs em **tabela PostgreSQL (`importacao_logs`)**:
     - Nome do arquivo
     - Número da linha
     - CPF
     - Parcela
     - Mensagem do erro

4. **Processos de importação**
   - Criação de tabelas temporárias.
   - Inserção e atualização em tabelas principais: `cbcontrato`, `cbparcela`, `geqlcontato`, `geqlemail`.
   - Barra de progresso (`ProgressBar`) e status em tempo real.

5. **Endereços aleatórios**
   - Campos de endereço vazios na primeira parcela recebem valores aleatórios.

6. **Interatividade**
   - Usuário pode optar por **ignorar linhas inválidas** ou cancelar a importação.
   - Feedback visual durante o processo.

7. **Preparado para Web**
   - Com logs gravados no banco, é possível criar uma página web para consultar registros de linhas ignoradas.

---

## Tecnologias Utilizadas

- **VB.NET** (.NET Framework ou .NET 6/7)
- **PostgreSQL**
- **ClosedXML** para manipulação de Excel
- **Npgsql** para conexão com PostgreSQL
- **Windows Forms** para interface gráfica
- **Logs em arquivo e banco de dados**
- **Logs em uma página web desenvolvida com JS (Vue3 e Node.js)

