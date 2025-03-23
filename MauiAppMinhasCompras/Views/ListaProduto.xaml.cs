using MauiAppMinhasCompras.Models; // Importa o namespace para acessar os modelos de dados do aplicativo
using System.Collections.ObjectModel; // Importa a coleção observável para usar listas reativas

namespace MauiAppMinhasCompras.Views; // Define o namespace da view, onde a interface está localizada

// A classe ListaProduto herda de ContentPage, o que significa que é uma página de conteúdo no aplicativo
public partial class ListaProduto : ContentPage
{
    // Cria uma coleção observável para armazenar os produtos
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    // Construtor da classe, que é chamado quando a página é criada
    public ListaProduto()
    {
        InitializeComponent(); // Inicializa os componentes da interface gráfica

        // Define a fonte de itens da lista (lst_produtos) como a coleção 'lista'
        lst_produtos.ItemsSource = lista;
    }

    // Método que é chamado quando a página aparece na tela
    protected override async void OnAppearing()
    {
        try
        {
           lista.Clear(); // Limpa a lista de produtos
            // Busca todos os produtos no banco de dados
            List<Produto> tmp = await App.Db.Getall();

            // Adiciona cada produto recuperado à coleção observável 'lista'
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            // Exibe um alerta em caso de erro
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    // Método chamado quando o botão de adicionar um novo produto na barra de ferramentas é clicado
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Navega para a página de cadastro de um novo produto
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            // Exibe um alerta em caso de erro
            DisplayAlert("Ops", ex.Message, "ok");
        }
    }

    // Método que é chamado toda vez que o texto da caixa de pesquisa (txt_search) for alterado
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            // Recupera o novo texto da caixa de pesquisa
            string q = e.NewTextValue;

            // Limpa a lista atual
            lista.Clear();

            // Pesquisa produtos no banco de dados com base no texto da pesquisa
            List<Produto> tmp = await App.Db.Search(q);

            // Adiciona os produtos encontrados à lista
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            // Exibe um alerta em caso de erro
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    // Método chamado quando o botão "Total dos Produtos" na barra de ferramentas é clicado
    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        // Calcula o total dos produtos somando o valor de cada produto
        double soma = lista.Sum(i => i.Total);

        // Exibe o valor total em um formato de moeda
        string msg = $"O total é {soma:C}";

        // Exibe o alerta com o total
        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    // Método chamado quando um item do menu (por exemplo, para excluir um produto) é clicado
    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obtém o MenuItem clicado
            MenuItem selecionado = sender as MenuItem;

            // Obtém o produto associado ao MenuItem
            Produto p = selecionado.BindingContext as Produto;

            // Exibe um alerta para confirmar a exclusão do produto
            bool confirm = await DisplayAlert(
                "Tem Certeza?", $"Remover Produto {p.Descricao}?", "Sim", "Não");

            // Se o usuário confirmar a exclusão
            if (confirm)
            {
                // Remove o produto do banco de dados
                await App.Db.Delete(p.Id);

                // Remove o produto da lista visível
                lista.Remove(p);
            }
        }
        catch (Exception ex)
        {
            // Exibe um alerta em caso de erro
           await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {

                Produto p = e.SelectedItem as Produto;

            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p,
            });
        }
        catch (Exception ex)
        {

        }



    }
}
