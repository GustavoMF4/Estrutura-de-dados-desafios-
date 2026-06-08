using System;
using System.Collections.Generic;

class Program
{
    
    static Dictionary<string, List<string>> cidades = new Dictionary<string, List<string>>();

    static void Main(string[] args)
    {
        MontarMapa();

        int opcao = 0;

        while (opcao != 5)
        {
            Console.WriteLine("\n===== BORA VIAJAR =====");
            Console.WriteLine("1 - Listar cidades");
            Console.WriteLine("2 - Verificar conexao direta");
            Console.WriteLine("3 - Existe rota? (DFS)");
            Console.WriteLine("4 - Menor rota (BFS)");
            Console.WriteLine("5 - Sair");

            Console.Write("Escolha: ");
            opcao = int.Parse(Console.ReadLine());
            
            Console.WriteLine("  ");


            if (opcao == 1)
            {
                MostrarCidades();
            }
            else if (opcao == 2)
            {
                ConexaoDireta();
            }
            else if (opcao == 3)
            {
                ProcurarDFS();
            }
            else if (opcao == 4)
            {
                ProcurarBFS();
            }
            else if (opcao == 5)
            {
                Console.WriteLine("Tenha uma Boa viagem!");
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }
        }
    }

    
    static void MontarMapa()
    {
        cidades.Add("Sao Paulo", new List<string>() { "Rio de Janeiro", "Curitiba", "Belo Horizonte" });
        cidades.Add("Rio de Janeiro", new List<string>() { "Sao Paulo", "Belo Horizonte", "Vitoria" });
        cidades.Add("Belo Horizonte", new List<string>() { "Sao Paulo", "Rio de Janeiro", "Brasilia" });
        cidades.Add("Curitiba", new List<string>() { "Sao Paulo", "Florianopolis" });
        cidades.Add("Florianopolis", new List<string>() { "Curitiba", "Porto Alegre" });
        cidades.Add("Porto Alegre", new List<string>() { "Florianopolis" });
        cidades.Add("Brasilia", new List<string>() { "Belo Horizonte", "Goiania" });
        cidades.Add("Goiania", new List<string>() { "Brasilia" });
        cidades.Add("Vitoria", new List<string>() { "Rio de Janeiro" });
        cidades.Add("Salvador", new List<string>() { "Recife" });
        cidades.Add("Recife", new List<string>() { "Salvador", "Fortaleza" });
        cidades.Add("Fortaleza", new List<string>() { "Recife" });
    }
    
    static void MostrarCidades()
    {
        foreach (var item in cidades)
        {
            Console.Write(item.Key + " -> ");

            foreach (string vizinho in item.Value)
            {
                Console.Write(vizinho + " ");
            }

            Console.WriteLine();
        }
    }

    
    static void ConexaoDireta()
    {
        Console.Write(" Qual a Primeira cidade?  ");
        string cidade1 = Console.ReadLine();

        Console.Write("Qual a Segunda cidade ? ");
        string cidade2 = Console.ReadLine();

        if (cidades.ContainsKey(cidade1) && cidades[cidade1].Contains(cidade2))
        {
            Console.WriteLine("Possuem conexao direta!");
        }
        else
        {
            Console.WriteLine("Infelizmente nao possuem conexao direta.");
        }
    }

    
    static void ProcurarDFS()
    {
        Console.Write("De onde esta partindo? ");
        string origem = Console.ReadLine();

        Console.Write("Onde quer chegar? ");
        string destino = Console.ReadLine();

        HashSet<string> visitado = new HashSet<string>();

        Console.Write("Visitando: ");

        bool achou = DFS(origem, destino, visitado);

        Console.WriteLine();

        if (achou)
        {
            Console.WriteLine("caminho Encontrado.");
        }
        else
        {
            Console.WriteLine("Nao existe rota.");
        }
    }

   
    static bool DFS(string atual, string destino, HashSet<string> visitado)
    {
        Console.Write(atual + " ");

        if (atual == destino)
        {
            return true;
        }

        visitado.Add(atual);

        foreach (string cidade in cidades[atual])
        {
            if (!visitado.Contains(cidade))
            {
                if (DFS(cidade, destino, visitado))
                {
                    return true;
                }
            }
        }

        return false;
    }

    
    static void ProcurarBFS()
    {
        Console.Write("De onde estaria partindo?: ");
        string origem = Console.ReadLine();

        Console.Write("Onde quer gostaria de  chegar?: ");
        string destino = Console.ReadLine();

        Queue<string> fila = new Queue<string>();
        HashSet<string> visitado = new HashSet<string>();
        Dictionary<string, string> veioDe = new Dictionary<string, string>();

        fila.Enqueue(origem);
        visitado.Add(origem);

        bool achou = false;

        while (fila.Count > 0)
        {
            string atual = fila.Dequeue();

            if (atual == destino)
            {
                achou = true;
                break;
            }

            foreach (string cidade in cidades[atual])
            {
                if (!visitado.Contains(cidade))
                {
                    visitado.Add(cidade);
                    veioDe[cidade] = atual;
                    fila.Enqueue(cidade);
                }
            }
        }

        if (!achou)
        {
            Console.WriteLine("Nao existe rota.");
            return;
        }

        List<string> caminho = new List<string>();

        string aux = destino;

        while (aux != origem)
        {
            caminho.Add(aux);
            aux = veioDe[aux];
        }

        caminho.Add(origem);

        caminho.Reverse();

        Console.WriteLine("\nMenor rota:");

        for (int i = 0; i < caminho.Count; i++)
        {
            if (i == caminho.Count - 1)
            {
                Console.Write(caminho[i]);
            }
            else
            {
                Console.Write(caminho[i] + " -> ");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Paradas: " + (caminho.Count - 1));
    }
}