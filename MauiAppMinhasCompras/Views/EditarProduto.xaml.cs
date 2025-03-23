using MauiAppMinhasCompras.Models;
// Esse comando importa o namespace onde está o modelo Produto, permitindo utilizá-lo aqui.

namespace MauiAppMinhasCompras.Views;
// Define o namespace da parte visual da aplicação, onde ficam as páginas (Views).

public partial class EditarProduto : ContentPage
// Declara a classe EditarProduto, que herda de ContentPage, ou seja, é uma página da interface do app.
{
    public EditarProduto()
    // Construtor da página EditarProduto
    {
        InitializeComponent();
        // Inicializa todos os componentes visuais definidos no arquivo XAML associado a esta página.
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    // Método que será chamado quando o botão da toolbar for clicado (provavelmente o botão "Salvar")
    {
        try
        {
            Produto produto_anexado = BindingContext as Produto;
            // Aqui pegamos o produto que está vinculado à tela (produto que está sendo editado).

            Produto p = new Produto
            {
                Id = produto_anexado.Id,
                // Mantemos o mesmo ID, pois é o produto já existente.
                Descricao = txt_descricao.Text,
                // Pegamos o texto da caixa de descrição e atribuímos à propriedade Descricao do produto.
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                // Convertendo o texto da caixa de quantidade para um número (double).
                Preco = Convert.ToDouble(txt_preco.Text)
                // Convertendo o texto da caixa de preço para double.
            };

            await App.Db.Update(p);
            // Aqui chamamos o método Update da classe de banco de dados para atualizar o registro.

            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");
            // Exibe uma mensagem de confirmação de que deu tudo certo.

            await Navigation.PopAsync();
            // Fecha a página atual e volta para a tela anterior.

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
            // Caso dê algum erro, exibe uma mensagem de erro na tela.
        }
    }
}
