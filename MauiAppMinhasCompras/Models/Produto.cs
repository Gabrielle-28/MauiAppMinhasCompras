using SQLite;
// Esse comando importa a biblioteca SQLite, que é responsável por permitir a comunicação e o uso do banco de dados local.

namespace MauiAppMinhasCompras.Models
// Aqui estamos definindo o namespace, que serve para organizar o código. 
// No caso, todos os modelos da aplicação vão ficar dentro de 'MauiAppMinhasCompras.Models'.
{
    // Essa é a classe Produto, que representa a tabela Produto no banco de dados.
    public class Produto
    {
        string _descricao;
        // Aqui criamos uma variável privada para armazenar o valor da descrição. 
        // Vamos usar ela para fazer validação antes de atribuir o valor.

        [PrimaryKey, AutoIncrement]
        // Esse atributo indica que o campo Id será a chave primária da tabela 
        // e que ele será incrementado automaticamente a cada novo registro.
        public int Id { get; set; }
        // Essa propriedade vai armazenar o código identificador único de cada produto.

        public string Descricao
        {
            get => _descricao;
            // Aqui, no get, ele devolve o valor armazenado na variável _descricao.

            set
            {
                // Aqui, dentro do set, fazemos uma validação para garantir que a descrição não seja nula.
                if (value == null)
                {
                    throw new System.Exception("Por favor, preencha a descrição");
                    // Caso o valor seja nulo, será exibida uma mensagem de erro.
                }

                _descricao = value;
                // Se o valor for válido, armazenamos na variável _descricao.
            }
        }

        public double Quantidade { get; set; }
        // Aqui temos a propriedade que guarda a quantidade do produto informado pelo usuário.

        public double Preco { get; set; }
        // Essa propriedade armazena o preço unitário do produto.

        public double Total
        {
            get => Quantidade * Preco;
            // Por fim, temos a propriedade Total, que não recebe valor manualmente, 
            // mas calcula automaticamente a multiplicação da quantidade pelo preço.
        }
    }
}
