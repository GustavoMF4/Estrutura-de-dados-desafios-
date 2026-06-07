#include<stdio.h>
#include <stdlib.h>
#include <time.h>

struct no{
int info; 
struct no *prox;
};


struct no *novoNo(int info){
    struct no *novo = malloc(sizeof(struct no));

    if(novo == NULL)
    {
        return NULL;
    }
    novo->info = info;
    novo->prox = NULL;

    return novo;
}



struct no *insert_first(struct no *Lista, int info){
    struct no *novo = novoNo(info);

    if( novo == NULL){
        return Lista; 
    }
        novo->prox = Lista; 
        return novo;
    }
    

    void printList (struct no *Lista){
        struct no *curr = Lista;
        
        if (curr == NULL){
            printf ("VAZIA");
        }

        while(curr != NULL){
            printf("%d",curr->info); 
            printf(" ");
            curr = curr->prox; 
        }
        printf("\n");
    }
   
    
struct no *reverse_list(struct no *lista)
{
    struct no *anterior = NULL;
    struct no *atual = lista;
    struct no *proximo = NULL;

    while(atual != NULL)
    {
        proximo = atual->prox;  
        atual->prox = anterior; 
        anterior = atual;       
        atual = proximo;         
    }

    return anterior;
}



    int main()
    {
        struct no *Lista = NULL; 

       
        
         Lista = insert_first(Lista, 3);
         Lista = insert_first(Lista, 1);
         Lista = insert_first(Lista, 7);

        printList(Lista);
        
        Lista = reverse_list(Lista);
        printList(Lista);
        
        
        return 0;
    }