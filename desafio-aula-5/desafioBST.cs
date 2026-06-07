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

}

public class Program {
    public static void Main() {
        BST tree = new BST();

        tree.Insert(2);
        tree.Insert(9);
        tree.Insert(12);
        tree.Insert(7);
        tree.Insert(27);
        tree.Insert(30);
        tree.Insert(72);
        tree.Insert(5);
        tree.Insert(3);
        tree.Insert(8);

        Node result = tree.Buscar(10);

        if (result != null)
            Console.WriteLine("Encontrado: " + result.Key);
        else
            Console.WriteLine("Nao encontrado");
    }
}