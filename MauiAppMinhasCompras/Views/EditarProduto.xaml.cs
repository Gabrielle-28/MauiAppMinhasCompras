using MauiAppMinhasCompras.Models;
// Esse comando importa o namespace onde est� o modelo Produto, permitindo utiliz�-lo aqui.

namespace MauiAppMinhasCompras.Views;
// Define o namespace da parte visual da aplica��o, onde ficam as p�ginas (Views).

public partial class EditarProduto : ContentPage
// Declara a classe EditarProduto, que herda de ContentPage, ou seja, � uma p�gina da interface do app.
{
    public EditarProduto()
    // Construtor da p�gina EditarProduto
    {
        InitializeComponent();
        // Inicializa todos os componentes visuais definidos no arquivo XAML associado a esta p�gina.
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    // M�todo que ser� chamado quando o bot�o da toolbar for clicado (provavelmente o bot�o "Salvar")
    {
        try
        {
            Produto produto_anexado = BindingContext as Produto;
            // Aqui pegamos o produto que est� vinculado � tela (produto que est� sendo editado).

            Produto p = new Produto
            {
                Id = produto_anexado.Id,
                // Mantemos o mesmo ID, pois � o produto j� existente.
                Descricao = txt_descricao.Text,
                // Pegamos o texto da caixa de descri��o e atribu�mos � propriedade Descricao do produto.
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                // Convertendo o texto da caixa de quantidade para um n�mero (double).
                Preco = Convert.ToDouble(txt_preco.Text)
                // Convertendo o texto da caixa de pre�o para double.
            };

            await App.Db.Update(p);
            // Aqui chamamos o m�todo Update da classe de banco de dados para atualizar o registro.

            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");
            // Exibe uma mensagem de confirma��o de que deu tudo certo.

            await Navigation.PopAsync();
            // Fecha a p�gina atual e volta para a tela anterior.

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
            // Caso d� algum erro, exibe uma mensagem de erro na tela.
        }
    }
}
