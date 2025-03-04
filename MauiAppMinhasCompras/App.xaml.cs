using MauiAppMinhasCompras.Helpers; // Importa a classe que gerencia o banco de dados SQLite

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        static SQliteDatabaseHelper _db; // Declara um objeto estático para a conexão com o banco de dados

        // Propriedade estática que retorna a instância do banco de dados
        public static SQliteDatabaseHelper Db
        {
            get
            {
                // Verifica se a instância do banco de dados já foi criada
                if (_db == null)
                {
                    // Define o caminho do arquivo do banco de dados SQLite dentro do armazenamento local do app
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData), // Obtém o diretório local da aplicação
                        "banco_sqlite_compras.db3"); // Nome do arquivo do banco de dados

                    _db = new SQliteDatabaseHelper(path); // Inicializa a conexão com o banco de dados
                }

                return _db; // Retorna a instância do banco de dados
            }
        }

        // Construtor da classe App
        public App()
        {
            InitializeComponent(); // Inicializa os componentes da aplicação

            // Define a página inicial do aplicativo
            // MainPage = new AppShell(); // Essa linha está comentada e não será usada

            MainPage = new NavigationPage(new Views.ListaProduto());
            // Define a página inicial como uma página de navegação, carregando a tela ListaProduto
        }
    }
}
