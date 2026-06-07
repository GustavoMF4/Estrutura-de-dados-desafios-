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
   
    
    struct no *remove_last(struct no *Lista){

    if(Lista == NULL){ 
        return NULL; 
    }

    if(Lista->prox == NULL){
        free(Lista); 
        return NULL;
    }

    struct no *curr = Lista;
    while(curr ->prox->prox != NULL){
        curr = curr->prox;
    }

    free(curr->prox); 
    curr->prox = NULL; 

    return Lista;
}


    int main()
    {
        struct no *Lista = NULL; 

       
        
         Lista = insert_first(Lista, 3);
         Lista = insert_first(Lista, 1);
         Lista = insert_first(Lista, 7);

        printList(Lista);
        
        //depois de remover
        Lista = remove_last(Lista);
        printList(Lista);
        return 0;
    }