using MauiAppMinhasCompras.Models; // Importa o namespace onde está definida a classe Produto
using SQLite; // Importa a biblioteca do SQLite para manipulação do banco de dados

namespace MauiAppMinhasCompras.Helpers
{
    public class SQliteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn; // Declara a conexão assíncrona com o banco de dados SQLite

        // Construtor da classe que recebe o caminho do banco de dados como parâmetro
        public SQliteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path); // Inicializa a conexão com o SQLite
            _conn.CreateTableAsync<Produto>().Wait(); // Cria a tabela Produto, caso ainda não exista
        }

        // Método para inserir um novo produto no banco de dados
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p); // Executa a inserção de forma assíncrona e retorna o número de linhas afetadas
        }

        // Método para atualizar os dados de um produto existente
        public Task<List<Produto>> Update(Produto p)
        {
            // Define a consulta SQL para atualizar os dados do produto com base no ID
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";

            return _conn.QueryAsync<Produto>(
               sql, p.Descricao, p.Quantidade, p.Preco, p.Id // Passa os parâmetros para a query SQL
            );
        }

        // Método para excluir um produto do banco de dados com base no ID
        public Task<int> Delete(int id)
        {
            // Utiliza a API do SQLite para excluir o produto onde o ID for igual ao informado
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        // Método para buscar todos os produtos cadastrados no banco de dados
        public Task<List<Produto>> Getall()
        {
            return _conn.Table<Produto>().ToListAsync(); // Retorna todos os produtos na tabela
        }

        // Método para buscar produtos pela descrição usando uma pesquisa com LIKE
        public Task<List<Produto>> Search(string q)
        {
            
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%" + q + "%'";

            return _conn.QueryAsync<Produto>(sql); // Executa a consulta e retorna os produtos que correspondem à busca
        }
    }
}
