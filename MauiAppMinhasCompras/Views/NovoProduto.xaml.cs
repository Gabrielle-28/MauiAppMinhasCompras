using MauiAppMinhasCompras.Models;
// Aqui estamos importando o namespace onde a classe Produto foi definida, para utiliz�-la na nossa p�gina.

namespace MauiAppMinhasCompras.Views
{
    // Define a p�gina onde ser� feito o cadastro de um novo produto.
    public partial class NovoProduto : ContentPage
    {
        public NovoProduto()
        {
            InitializeComponent();
            // M�todo construtor, respons�vel por inicializar a p�gina e carregar os componentes da interface.
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Criamos uma nova inst�ncia da classe Produto e preenchemos as propriedades com os dados inseridos pelo usu�rio.
                Produto p = new Produto
                {
                    Descricao = txt_descricao.Text,  // Obt�m o texto inserido no campo de descri��o.
                    Quantidade = Convert.ToDouble(txt_quantidade.Text), // Converte o texto de quantidade para n�mero.
                    Preco = Convert.ToDouble(txt_preco.Text) // Converte o texto de pre�o para n�mero.
                };

                // Aqui estamos inserindo o novo produto no banco de dados atrav�s do m�todo Insert da classe App.Db.
                await App.Db.Insert(p);

                // Exibe um alerta informando que o produto foi inserido com sucesso.
                await DisplayAlert("Sucesso!", "Registro Inserido", "OK");

                // Ap�s o sucesso, a navega��o volta para a p�gina anterior.
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
