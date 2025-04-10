using System;

class Program
{
    // Estrutura para representar um produto
    struct Produto
    {
        public string Nome;
        public double Preco;
    }

    // Estrutura para itens no pedido
    struct ItemPedido
    {
        public Produto Produto;
        public int Quantidade;
    }

    static void Main(string[] args)
    {
        // Produtos cadastrados estaticamente
        Produto[] cardapio = new Produto[3];
        cardapio[0] = new Produto { Nome = "X-Burguer", Preco = 25.00 };
        cardapio[1] = new Produto { Nome = "Refrigerante", Preco = 8.00 };
        cardapio[2] = new Produto { Nome = "Sorvete", Preco = 12.00 };

        ItemPedido[] pedido = new ItemPedido[10];
        int totalItens = 0;

        bool executando = true;

        while (executando)
        {
            ExibirMenu();
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "a":
                    ListarProdutos(cardapio);
                    break;

                case "b":
                    totalItens = AdicionarProduto(cardapio, pedido, totalItens);
                    break;

                case "c":
                    totalItens = RemoverProduto(pedido, totalItens);
                    break;

                case "d":
                    VisualizarPedido(pedido, totalItens);
                    break;

                case "e":
                    FinalizarPedido(pedido, totalItens);
                    executando = false;
                    break;

                default:
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    break;
            }
        }
    }

    /*
     * Função que exibe o menu principal do sistema
     */
    static void ExibirMenu()
    {
        Console.WriteLine("\n=== LANCHONETE VIRTUAL ===");
        Console.WriteLine("a. Listar produtos disponíveis");
        Console.WriteLine("b. Adicionar produto ao pedido");
        Console.WriteLine("c. Remover produto do pedido");
        Console.WriteLine("d. Visualizar pedido atual");
        Console.WriteLine("e. Finalizar pedido e sair");
        Console.Write("Escolha uma opção: ");
    }

    /*
     * Lista todos os produtos do cardápio
     */
    static void ListarProdutos(Produto[] cardapio)
    {
        Console.WriteLine("\nProdutos disponíveis:");
        for (int i = 0; i < cardapio.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {cardapio[i].Nome} - R$ {cardapio[i].Preco:F2}");
        }
    }

    /*
     * Adiciona um produto ao pedido atual
     */
    static int AdicionarProduto(Produto[] cardapio, ItemPedido[] pedido, int totalItens)
    {
        ListarProdutos(cardapio);
        Console.Write("Digite o número do produto: ");
        int codigo = int.Parse(Console.ReadLine()) - 1;

        Console.Write("Digite a quantidade: ");
        int qtd = int.Parse(Console.ReadLine());

        if (codigo >= 0 && codigo < cardapio.Length)
        {
            pedido[totalItens] = new ItemPedido { Produto = cardapio[codigo], Quantidade = qtd };
            totalItens++;
            Console.WriteLine("Produto adicionado com sucesso!");
        }

        return totalItens;
    }

    /*
     * Remove um item do pedido
     */
    static int RemoverProduto(ItemPedido[] pedido, int totalItens)
    {
        VisualizarPedido(pedido, totalItens);
        if (totalItens > 0)
        {
            Console.Write("Digite o número do item a remover: ");
            int indice = int.Parse(Console.ReadLine()) - 1;

            if (indice >= 0 && indice < totalItens)
            {
                for (int i = indice; i < totalItens - 1; i++)
                {
                    pedido[i] = pedido[i + 1];
                }

                totalItens--;
                Console.WriteLine("Produto removido com sucesso!");
            }
        }
        return totalItens;
    }

    /*
     * Mostra o pedido atual com valores
     */
    static void VisualizarPedido(ItemPedido[] pedido, int totalItens)
    {
        Console.WriteLine("\nSeu pedido atual:");
        if (totalItens == 0)
        {
            Console.WriteLine("Nenhum item no pedido.");
            return;
        }

        for (int i = 0; i < totalItens; i++)
        {
            Console.WriteLine($"{i + 1}. {pedido[i].Produto.Nome} - {pedido[i].Quantidade}x - R$ {pedido[i].Produto.Preco * pedido[i].Quantidade:F2}");
        }
    }

    /*
     * Finaliza o pedido e exibe o resumo
     */
    static void FinalizarPedido(ItemPedido[] pedido, int totalItens)
    {
        if (totalItens == 0)
        {
            Console.WriteLine("\nPedido vazio. Programa encerrado.");
            return;
        }

        double valorBruto = 0;
        int qtdTotalItens = 0;

        for (int i = 0; i < totalItens; i++)
        {
            valorBruto += pedido[i].Produto.Preco * pedido[i].Quantidade;
            qtdTotalItens += pedido[i].Quantidade;
        }

        double desconto = 0;

        if (valorBruto > 100.00)
        {
            desconto = valorBruto * 0.10;
            Console.WriteLine("\nDesconto de 10% aplicado (pedido acima de R$ 100,00)!");
        }

        double valorFinal = valorBruto - desconto;

        // Brinde adicional
        if (qtdTotalItens >= 5)
        {
            Console.WriteLine("🎁 Parabéns! Você ganhou um brinde por comprar 5 itens ou mais!");
        }

        Console.WriteLine("\n=== RESUMO DO PEDIDO ===");
        Console.WriteLine($"Total de itens: {qtdTotalItens}");
        Console.WriteLine($"Valor bruto: R$ {valorBruto:F2}");
        Console.WriteLine($"Desconto: R$ {desconto:F2}");
        Console.WriteLine($"Valor final: R$ {valorFinal:F2}");
        Console.WriteLine("Obrigado pela preferência!");
    }
}
