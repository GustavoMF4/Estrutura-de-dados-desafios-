using System;
using System.Collections.Generic;
using System.Diagnostics;

public class No
{
    public int Key { get; set; }
    public No Esq { get; set; }
    public No Dir { get; set; }

    public No(int valor)
    {
        this.Key = valor;
    }
}

public class AVLNo
{
  public int Altura { get; set; }
  public int Chave { get; set; }

  public int FatorDeBalanceamento
  {
    get
    {
      int esquerda = 0, direita = 0;
      if (Esq != null) esquerda = Esq.Altura;
      if (Dir != null) direita = Dir.Altura;
      return esquerda - direita;
    }
  }
  public AVLNo Esq { get; set; }
  public AVLNo Dir { get; set; }

  public AVLNo(int valor)
  {
    this.Chave = valor;
    this.Altura = 1;
  }

  public void CalculaAltura()
  {
    //this.Altura = Math.Max(this.Esq?.Altura ?? 0, this.Dir?.Altura ?? 0) + 1;
    int alturaEsq = 0, alturaDir = 0;
    if (this.Esq != null) alturaEsq = this.Esq.Altura;
    if (this.Dir != null) alturaDir = this.Dir.Altura;

    this.Altura = 1 + Math.Max(alturaDir, alturaEsq);
  }

}

// ################## BST ##################

public class BST
{
    private No Raiz { get; set; }

    public BST()
    {
        this.Raiz = null;
    }

    private No delete(No node, int chave)
    {
        if (node == null)
            return null;

        if (chave < node.Key)
        {
            node.Esq = this.delete(node.Esq, chave);
        }
        else if (chave > node.Key)
        {
            node.Dir = this.delete(node.Dir, chave);
        }
        else
        {
            if (node.Esq == null)
            {
                return node.Dir;
            }
            else if (node.Dir == null)
            {
                return node.Esq;
            }
            else
            {
                int valorSucessor = (int)this.min(node.Dir);
                node.Key = valorSucessor;
                node.Dir = this.delete(node.Dir, valorSucessor);
            }
        }

        return node;
    }

    private No InsertRecursivo(No raiz, int chave)
    {
        if (raiz == null)
        {
            return new No(chave);
        }

        if (chave > raiz.Key)
        {
            raiz.Dir = this.InsertRecursivo(raiz.Dir, chave);
        }
        else
        {
            raiz.Esq = this.InsertRecursivo(raiz.Esq, chave);
        }

        return raiz;
    }

    private No SearchRecursivo(No raiz, int chave)
    {
        if (raiz == null)
        {
            return null;
        }

        if (raiz.Key == chave)
            return raiz;
        else if (chave > raiz.Key)
            return this.SearchRecursivo(raiz.Dir, chave);
        else
            return this.SearchRecursivo(raiz.Esq, chave);
    }

    private int? min(No node)
    {
        No aux = node;
        while (aux != null && aux.Esq != null)
        {
            aux = aux.Esq;
        }

        return aux == null ? null : aux.Key;
    }

    private void printNicely(No node, string spacing)
    {
        if (node != null)
        {
            Console.WriteLine(spacing + node.Key);
            this.printNicely(node.Esq, spacing + "..");
            this.printNicely(node.Dir, spacing + "..");
        }
    }

    private void printInOrder(No node)
    {
        if (node != null)
        {
            this.printInOrder(node.Esq);
            Console.Write(node.Key + " ");
            this.printInOrder(node.Dir);
        }
    }

    public No Search(int valor)
    {
        return this.SearchRecursivo(this.Raiz, valor);
    }

    public void Insert(int valor)
    {
        this.Raiz = this.InsertRecursivo(this.Raiz, valor);
    }

    public void Delete(int valor)
    {
        this.Raiz = this.delete(this.Raiz, valor);
    }

    public int? Max()
    {
        No aux = this.Raiz;
        while (aux != null && aux.Dir != null)
        {
            aux = aux.Dir;
        }

        return aux == null ? null : aux.Key;
    }

    public int? Min()
    {
        return this.min(this.Raiz);
    }

    public void PrintInOrder()
    {
        this.printInOrder(this.Raiz);
    }

    public void PrintNicely()
    {
        this.printNicely(this.Raiz, ".");
    }
    
    private int AlturaRecursiva(No node)
{
    if (node == null)
        return 0;

    return 1 + Math.Max(
        AlturaRecursiva(node.Esq),
        AlturaRecursiva(node.Dir)
    );
}

public int Altura()
{
    return AlturaRecursiva(this.Raiz);
}

}

//########### AVL ####################

public class AVL
{
  private AVLNo Raiz { get; set; }

  private void printNicely(AVLNo node, string spacing)
  {
    if (node != null)
    {
      Console.WriteLine(spacing + node.Chave);
      this.printNicely(node.Esq, spacing + "..");
      this.printNicely(node.Dir, spacing + "..");
    }
  }

  public void PrintNicely()
  {
    this.printNicely(this.Raiz, ".");
  }

  private AVLNo BuscaRecursivo(AVLNo raiz, int chave)
  {
    if (raiz == null)
    {
      return null;
    }

    if (raiz.Chave == chave)
      return raiz;
    else if (chave > raiz.Chave)
      return BuscaRecursivo(raiz.Dir, chave);
    else
      return BuscaRecursivo(raiz.Esq, chave);
  }

  public AVLNo Busca(int valor)
  {
    return BuscaRecursivo(Raiz, valor);
  }

  private AVLNo RotacionaEsquerda(AVLNo raiz)
  {
    if (raiz == null) return raiz;

    AVLNo novaRaiz = raiz.Dir;
    raiz.Dir = novaRaiz.Esq;
    novaRaiz.Esq = raiz;
    raiz.CalculaAltura();
    novaRaiz.CalculaAltura();

    return novaRaiz;
  }
  private AVLNo RotacionaDireita(AVLNo raiz)
  {
    if (raiz == null) return raiz;

    AVLNo novaRaiz = raiz.Esq;
    raiz.Esq = novaRaiz.Dir;
    novaRaiz.Dir = raiz;
    raiz.CalculaAltura();
    novaRaiz.CalculaAltura();

    return novaRaiz;
  }
  private AVLNo InserirRecursivo(AVLNo raiz, int chave)
  {
    if (raiz == null) return new AVLNo(chave);

    // Igual BST padrão: vamos buscar onde inserir o nó
    // navegando pela árvore
    if (chave > raiz.Chave)
      raiz.Dir = InserirRecursivo(raiz.Dir, chave);
    else
      raiz.Esq = InserirRecursivo(raiz.Esq, chave);

    // Aqui nós modificamos a raiz, adicionando algo à direita ou 
    // à esquerda. Por isso, recalculamos sua altura.
    raiz.CalculaAltura();
    // Agora, vamos verificar se o nó está desbalanceado
    if (raiz.FatorDeBalanceamento == 2)
    {
      if (raiz.Esq?.FatorDeBalanceamento < 0)
        raiz.Esq = RotacionaEsquerda(raiz.Esq);

      raiz = RotacionaDireita(raiz);
    }
    else if (raiz.FatorDeBalanceamento == -2)
    {
      if (raiz.Dir.FatorDeBalanceamento > 0)
        raiz.Dir = RotacionaDireita(raiz.Dir);

      raiz = RotacionaEsquerda(raiz);
    }

    return raiz;
  }

  public void Insert(int valor)
  {
    Raiz = InserirRecursivo(Raiz, valor);
  }
  
  public int Altura()
{
    return Raiz == null ? 0 : Raiz.Altura;
}
}

class Program
{
    static void Main()
    {
       while (true)
        {
            Console.WriteLine("MENU:");
            Console.WriteLine("1) nova simulacao ou 2) sair");

            int opcao = int.Parse(Console.ReadLine());

            if (opcao == 2)
            {
                Console.WriteLine("SIMULACAO ENCERRADA");
                break;
            }

            Console.Write("Digitar quantidade de amostras: ");
            int A = int.Parse(Console.ReadLine());

            Console.Write("Digitar quantidade de elementos para cada amostra: ");
            int N = int.Parse(Console.ReadLine());

            double somaBST = 0;
            double somaAVL = 0;

            double tempoBST = 0;
            double tempoAVL = 0;

            Random rnd = new Random();

            for (int i = 0; i < A; i++)
            {
                BST bst = new BST();
                AVL avl = new AVL();

                HashSet<int> numeros = new HashSet<int>();

                while (numeros.Count < N)
                {
                    numeros.Add(rnd.Next(1, 1000000));
                }

                Stopwatch sw = Stopwatch.StartNew();

                foreach (int valor in numeros)
                {
                    bst.Insert(valor);
                }

                sw.Stop();
                tempoBST += sw.Elapsed.TotalMilliseconds;

                sw.Restart();

                foreach (int valor in numeros)
                {
                    avl.Insert(valor);
                }

                sw.Stop();
                tempoAVL += sw.Elapsed.TotalMilliseconds;

                somaBST += bst.Altura();
                somaAVL += avl.Altura();
            }

            double mediaBST = somaBST / A;
            double mediaAVL = somaAVL / A;

            double mediaGeral =
                (somaBST + somaAVL) / (2 * A);

            double mediaTempoBST = tempoBST / A;
            double mediaTempoAVL = tempoAVL / A;

            double mediaTempoGeral =
                (tempoBST + tempoAVL) / (2 * A);

            Console.WriteLine();
            Console.WriteLine($"simulcao com A = {A} e N = {N}");
            Console.WriteLine("----------------------");

            Console.WriteLine($"Altura media geral: {mediaGeral:F2}");
            Console.WriteLine($"Tempo medio geral: {mediaTempoGeral:F2}");

            Console.WriteLine("  ");

            Console.WriteLine($"Altura media BST: {mediaBST:F2}");
             Console.WriteLine($"Tempo medio geral: {mediaTempoBST:F2}");
            

            Console.WriteLine("   ");

            Console.WriteLine($"Altura media AVL: {mediaAVL:F2}");
            Console.WriteLine($"Tempo medio geral: {mediaTempoAVL:F2}");

            Console.WriteLine();
        }
    }
}
