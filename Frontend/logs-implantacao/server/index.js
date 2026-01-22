const express = require('express');
const { Pool } = require('pg');
const cors = require('cors');

const app = express();
app.use(cors());

const pool = new Pool({
    user: 'root',
    host: 'localhost',
    database: 'implantacao',
    password: '1234',
    port: 5432,
});

app.get('/logs', async (req, res) => {
    try {
        const result = await pool.query('SELECT * FROM importacao_logs ORDER BY data_hora desc');
        res.json(result.rows);
    } catch (err) {
        console.error("Erro ao acessar o banco:", err.message);
        res.status(500).json({ error: "Erro no servidor ao buscar logs" });
    }
});

const PORT = 3000;
app.listen(PORT, () => {
    console.log(`Servidor de logs rodando em http://localhost:${PORT}`);
});