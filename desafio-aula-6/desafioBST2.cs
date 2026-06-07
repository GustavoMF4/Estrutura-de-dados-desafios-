using System;


public class Node {
    public int Key { get; set; }
    public Node esq { get; set; }
    public Node dir { get; set; }

    public Node(int key) {
        Key = key;
    }
}

public class BST{ 

private Node root; 
public void Insert(int value){
    root = InsertRecursive(root, value);
}
    private Node InsertRecursive(Node Atual, int value){
        if(Atual == null){
            return new Node(value);
        }
        if (value < Atual.Key){ 
         Atual.esq = InsertRecursive(Atual.esq, value);   
        }
        else if(value > Atual.Key){
            Atual.dir = InsertRecursive(Atual.dir, value);
        }
        return Atual;
    }
    
    
    public Node Buscar(int value){
        return BuscarRecursivo(root, value);
        
    }
    
    private Node BuscarRecursivo(Node Atual, int value){
        if(Atual == null || Atual.Key == value){ 
             return Atual;
        }
        
        if(value < Atual.Key){
            return BuscarRecursivo(Atual.esq, value); 
        }
        else {
            return BuscarRecursivo(Atual.dir, value);
        }
        
        
    }
    
    public Node MaximoRecursivo()
{
    return MaximoRecursivo(root);
}

private Node MaximoRecursivo(Node atual)
{
    if (atual == null)
        return null;

    if (atual.dir == null)
        return atual;

    return MaximoRecursivo(atual.dir);
}

public Node MaximoIterativo()
{
    if (root == null)
        return null;

    Node atual = root;

    while (atual.dir != null)
    {
        atual = atual.dir;
    }

    return atual;
}

public Node MinimoIterativo()
{
    if (root == null)
        return null;

    Node atual = root;

    while (atual.esq != null)
    {
        atual = atual.esq;
    }

    return atual;
}

public Node MinimoRecursivo()
{
    return MinimoRecursivo(root);
}

private Node MinimoRecursivo(Node atual)
{
    if (atual == null)
        return null;

    if (atual.esq == null)
        return atual;

    return MinimoRecursivo(atual.esq);
}

public void PrintInOrder()
{
    PrintInOrder(root);
    Console.WriteLine();
}

private void PrintInOrder(Node atual)
{
    if (atual == null)
        return;

    PrintInOrder(atual.esq);
    Console.Write(atual.Key + " ");
    PrintInOrder(atual.dir);
}

public void CoolPrint()
{
    CoolPrint(root, 0);
}

private void CoolPrint(Node atual, int nivel)
{
    if (atual == null)
        return;

    Console.WriteLine(new string(' ', nivel * 4) + atual.Key);

    CoolPrint(atual.esq, nivel + 1);
    CoolPrint(atual.dir, nivel + 1);
}

}

public class Program {
    public static void Main(string[] args) {
        BST bst = new BST();
        
        bst.Insert(15);
        bst.Insert(10);
        bst.Insert(8);
        bst.Insert(12);
        bst.Insert(20);
        bst.Insert(21);
 
Console.WriteLine("Menor recursivo: " +
    bst.MinimoRecursivo().Key);

Console.WriteLine("Menor interativo: " +
    bst.MinimoIterativo().Key);

Console.WriteLine("Maior recursivo: " +
    bst.MaximoRecursivo().Key);

Console.WriteLine("Maior interativo: " +
    bst.MaximoIterativo().Key);
        
        Console.WriteLine("In-order traversal (Sorted keys):");
        bst.PrintInOrder();
        Console.WriteLine("Visualizacao mais legal:");
        bst.CoolPrint();
    }
}