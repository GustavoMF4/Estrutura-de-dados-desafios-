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
   
    

struct no *remove_value(struct no *Lista, int value){
    struct no *atual = Lista;
    struct no *anterior = NULL;
    

    while(atual != NULL){
        if(atual->info == value){
            if(anterior == NULL){
                Lista = atual->prox;
            }
            else
            {
                anterior->prox = atual->prox;
            }
            free(atual);
            return Lista;
        }
        anterior = atual;
        atual = atual->prox;
    }
    return Lista;
}



    int main()
    {
        struct no *Lista = NULL; 

       
        
         Lista = insert_first(Lista, 3);
         Lista = insert_first(Lista, 1);
         Lista = insert_first(Lista, 7);

        printList(Lista);
        
        // depois de remover
        Lista = remove_value(Lista, 1);
        printList(Lista);
        
        Lista = remove_value(Lista, 3);
        printList(Lista);
        
        Lista = remove_value(Lista, 7);
        printList(Lista);
        
        
        return 0;
    }