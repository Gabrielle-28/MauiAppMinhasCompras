using MauiAppMinhasCompras.Models; // Importa o namespace para acessar os modelos de dados do aplicativo
using System.Collections.ObjectModel; // Importa a cole��o observ�vel para usar listas reativas

namespace MauiAppMinhasCompras.Views; // Define o namespace da view, onde a interface est� localizada

// A classe ListaProduto herda de ContentPage, o que significa que � uma p�gina de conte�do no aplicativo
public partial class ListaProduto : ContentPage
{
    // Cria uma cole��o observ�vel para armazenar os produtos
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    // Construtor da classe, que � chamado quando a p�gina � criada
    public ListaProduto()
    {
        InitializeComponent(); // Inicializa os componentes da interface gr�fica

        // Define a fonte de itens da lista (lst_produtos) como a cole��o 'lista'
        lst_produtos.ItemsSource = lista;
    }

    // M�todo que � chamado quando a p�gina aparece na tela
    protected override async void OnAppearing()
    {
        try
        {
           lista.Clear(); // Limpa a lista de produtos
            // Busca todos os produtos no banco de dados
            List<Produto> tmp = await App.Db.Getall();

            // Adiciona cada produto recuperado � cole��o observ�vel 'lista'
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            // Exibe um alerta em caso de erro
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    // M�todo chamado quando o bot�o de adicionar um novo produto na barra de ferramentas � clicado
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Navega para a p�gina de cadastro de um novo produto
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            // Exibe um alerta em caso de erro
            DisplayAlert("Ops", ex.Message, "ok");
        }
    }

    // M�todo que � chamado toda vez que o texto da caixa de pesquisa (txt_search) for alterado
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

            // Adiciona os produtos encontrados � lista
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            // Exibe um alerta em caso de erro
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    // M�todo chamado quando o bot�o "Total dos Produtos" na barra de ferramentas � clicado
    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        // Calcula o total dos produtos somando o valor de cada produto
        double soma = lista.Sum(i => i.Total);

        // Exibe o valor total em um formato de moeda
        string msg = $"O total � {soma:C}";

        // Exibe o alerta com o total
        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    // M�todo chamado quando um item do menu (por exemplo, para excluir um produto) � clicado
    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obt�m o MenuItem clicado
            MenuItem selecionado = sender as MenuItem;

            // Obt�m o produto associado ao MenuItem
            Produto p = selecionado.BindingContext as Produto;

            // Exibe um alerta para confirmar a exclus�o do produto
            bool confirm = await DisplayAlert(
                "Tem Certeza?", $"Remover Produto {p.Descricao}?", "Sim", "N�o");

            // Se o usu�rio confirmar a exclus�o
            if (confirm)
            {
                // Remove o produto do banco de dados
                await App.Db.Delete(p.Id);

                // Remove o produto da lista vis�vel
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
