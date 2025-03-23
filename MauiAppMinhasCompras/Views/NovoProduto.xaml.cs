using MauiAppMinhasCompras.Models;
// Aqui estamos importando o namespace onde a classe Produto foi definida, para utilizá-la na nossa página.

namespace MauiAppMinhasCompras.Views
{
    // Define a página onde será feito o cadastro de um novo produto.
    public partial class NovoProduto : ContentPage
    {
        public NovoProduto()
        {
            InitializeComponent();
            // Método construtor, responsável por inicializar a página e carregar os componentes da interface.
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Criamos uma nova instância da classe Produto e preenchemos as propriedades com os dados inseridos pelo usuário.
                Produto p = new Produto
                {
                    Descricao = txt_descricao.Text,  // Obtém o texto inserido no campo de descrição.
                    Quantidade = Convert.ToDouble(txt_quantidade.Text), // Converte o texto de quantidade para número.
                    Preco = Convert.ToDouble(txt_preco.Text) // Converte o texto de preço para número.
                };

                // Aqui estamos inserindo o novo produto no banco de dados através do método Insert da classe App.Db.
                await App.Db.Insert(p);

                // Exibe um alerta informando que o produto foi inserido com sucesso.
                await DisplayAlert("Sucesso!", "Registro Inserido", "OK");

                // Após o sucesso, a navegação volta para a página anterior.
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                // Se ocorrer algum erro, exibe uma mensagem de erro.
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}
