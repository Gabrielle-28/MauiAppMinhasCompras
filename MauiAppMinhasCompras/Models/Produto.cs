using SQLite; // Importa a biblioteca SQLite para manipulação do banco de dados

namespace MauiAppMinhasCompras.Models
{
    // Classe que representa a tabela "Produto" no banco de dados
    public class Produto
    {
        [PrimaryKey, AutoIncrement] // Define que "Id" será a chave primária e terá auto incremento
        public int Id { get; set; } // Identificador único do produto no banco de dados

        public string Descricao { get; set; } // Nome ou descrição do produto

        public double Quantidade { get; set; } // Quantidade do produto

        public double Preco { get; set; } // Preço do produto
    }
}
