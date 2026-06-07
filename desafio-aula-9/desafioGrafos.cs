#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>

#define JOGADOR 1
#define MONSTRO 2

#define MAX_NOME 50
#define MAX_CLASSE 50
#define MAX_HABILIDADE 50

typedef struct habilidade {
    char nome[MAX_HABILIDADE];
    float modificador;
} Habilidade;

typedef struct personagem {
    char nome[MAX_NOME];
    int tipo; 
    char classe[MAX_CLASSE];
    int vida;
    int dano;
    int iniciativa;
    int temHabilidade;
    Habilidade habilidade;
} Personagem;

typedef struct no {
    Personagem personagem;
    struct no *prox;
} No;

typedef No *Celula;

typedef struct lista {
    Celula inicio;
    Celula fim;
} Lista;

typedef Lista *ListaCircular;



ListaCircular novaListaCircular(void) {
    ListaCircular lista = (ListaCircular) malloc(sizeof(Lista));
    if (lista == NULL) return NULL;
    lista->inicio = NULL;
    lista->fim = NULL;
    return lista;
}

Celula novaCelula(Personagem p) {
    Celula nova = (Celula) malloc(sizeof(No));
    if (nova == NULL) return NULL;
    nova->personagem = p;
    nova->prox = NULL;
    return nova;
}

int listaVazia(ListaCircular lista) {
    return lista == NULL || lista->inicio == NULL;
}

void insereNoFimCircular(ListaCircular lista, Personagem p) {
    Celula nova = novaCelula(p);
    if (lista == NULL || nova == NULL) return;

    if (lista->inicio == NULL) {
        lista->inicio = nova;
        lista->fim = nova;
        nova->prox = nova;
    } else {
        nova->prox = lista->inicio;
        lista->fim->prox = nova;
        lista->fim = nova;
    }
}

/* Ordem crescente de iniciativa, como no enunciado da aula */
void insereOrdenadoPorIniciativa(ListaCircular lista, Personagem p) {
    Celula nova, aux;

    if (lista == NULL) return;

    nova = novaCelula(p);
    if (nova == NULL) return;

    if (lista->inicio == NULL) {
        lista->inicio = nova;
        lista->fim = nova;
        nova->prox = nova;
        return;
    }

    if (p.iniciativa < lista->inicio->personagem.iniciativa) {
        nova->prox = lista->inicio;
        lista->inicio = nova;
        lista->fim->prox = lista->inicio;
        return;
    }

    if (p.iniciativa >= lista->fim->personagem.iniciativa) {
        nova->prox = lista->inicio;
        lista->fim->prox = nova;
        lista->fim = nova;
        return;
    }

    aux = lista->inicio;
    while (aux->prox != lista->inicio &&
           aux->prox->personagem.iniciativa <= p.iniciativa) {
        aux = aux->prox;
    }

    nova->prox = aux->prox;
    aux->prox = nova;
}

void printListaCircular(ListaCircular lista) {
    Celula aux;

    if (listaVazia(lista)) {
        printf("[lista vazia]\n");
        return;
    }

    aux = lista->inicio;
    do {
        printf("%s{%s, Classe=%s, HP=%d, D=%d, Ini=%d}",
               aux->personagem.nome,
               aux->personagem.tipo == JOGADOR ? "Jogador" : "Monstro",
               aux->personagem.classe,
               aux->personagem.vida,
               aux->personagem.dano,
               aux->personagem.iniciativa);
        if (aux->personagem.temHabilidade) {
            printf("[Hab=%s x%.1f]",
                   aux->personagem.habilidade.nome,
                   aux->personagem.habilidade.modificador);
        }
        aux = aux->prox;
        if (aux != lista->inicio) printf(" -> ");
    } while (aux != lista->inicio);
    printf("\n");
}

int contarTipo(ListaCircular lista, int tipo) {
    Celula aux;
    int total = 0;

    if (listaVazia(lista)) return 0;

    aux = lista->inicio;
    do {
        if (aux->personagem.tipo == tipo) total++;
        aux = aux->prox;
    } while (aux != lista->inicio);

    return total;
}

void liberarLista(ListaCircular lista) {
    Celula atual, prox;

    if (lista == NULL) return;
    if (lista->inicio == NULL) {
        free(lista);
        return;
    }

    atual = lista->inicio->prox;
    while (atual != lista->inicio) {
        prox = atual->prox;
        free(atual);
        atual = prox;
    }

    free(lista->inicio);
    free(lista);
}

/* =========================================================
   FUNCOES PARA IMPLEMENTAR
   ========================================================= */

Celula buscaInimigoMaisProximo(Celula atual) {
     if (atual == NULL)
        return NULL;

    int tipoInimigo;

    if (atual->personagem.tipo == JOGADOR)
        tipoInimigo = MONSTRO;
    else
        tipoInimigo = JOGADOR;

    Celula aux = atual->prox;

    while (aux != atual) {
        if (aux->personagem.tipo == tipoInimigo)
            return aux;

        aux = aux->prox;
    }

    return NULL;
}


void removeDaListaCircular(ListaCircular lista, Celula alvo) {
   if (listaVazia(lista) || alvo == NULL)
        return;

   
    if (lista->inicio == alvo &&
        lista->fim == alvo) {

        free(alvo);
        lista->inicio = NULL;
        lista->fim = NULL;
        return;
    }

    Celula ant = lista->inicio;

    while (ant->prox != alvo &&
           ant->prox != lista->inicio) {
        ant = ant->prox;
    }

    if (ant->prox != alvo)
        return;

    
    if (alvo == lista->inicio) {
        lista->inicio = alvo->prox;
        lista->fim->prox = lista->inicio;
    }


    if (alvo == lista->fim) {
        lista->fim = ant;
        lista->fim->prox = lista->inicio;
    }

  
    ant->prox = alvo->prox;
    
    free (alvo);

}


Celula executaUmTurno(ListaCircular lista, Celula atual) {
     if (listaVazia(lista) || atual == NULL)
        return NULL;

    Celula alvo = buscaInimigoMaisProximo(atual);

    if (alvo == NULL)
        return atual->prox;

    Celula proximoTurno = atual->prox;

    int dano = atual->personagem.dano;

    if (atual->personagem.temHabilidade) {

        int chance = rand() % 100;

        if (chance < 20) {

            printf("%s usa habilidade %s!\n",
                   atual->personagem.nome,
                   atual->personagem.habilidade.nome);

            dano = (int)(dano *
                         atual->personagem.habilidade.modificador);
        }
    }

    printf("%s ataca %s causando %d de dano.\n",
           atual->personagem.nome,
           alvo->personagem.nome,
           dano);

    alvo->personagem.vida -= dano;

    if (alvo->personagem.vida <= 0) {

        printf("%s foi derrotado!\n",
               alvo->personagem.nome);

        if (proximoTurno == alvo)
            proximoTurno = alvo->prox;

        removeDaListaCircular(lista, alvo);
    }
  return proximoTurno;
}

//=========================================================



void teste1_iniciativa(void) {
    ListaCircular lista = novaListaCircular();

    Personagem a  = {"Jogador A", JOGADOR, "Guerreiro", 30,  8, 20, 1, {"Ataque Heroico", 1.5f}};
    Personagem b  = {"Jogador B", JOGADOR, "Mago",      20, 10, 10, 1, {"Bola de Fogo",   2.0f}};
    Personagem c  = {"Jogador C", JOGADOR, "Ladino",    25,  6,  5, 0, {"",               0.0f}};
    Personagem m1 = {"Monstro 1", MONSTRO, "Orc",       18,  7,  7, 0, {"",               0.0f}};
    Personagem m2 = {"Monstro 2", MONSTRO, "Goblin",    15,  4,  1, 0, {"",               0.0f}};

    insereOrdenadoPorIniciativa(lista, a);
    insereOrdenadoPorIniciativa(lista, b);
    insereOrdenadoPorIniciativa(lista, c);
    insereOrdenadoPorIniciativa(lista, m1);
    insereOrdenadoPorIniciativa(lista, m2);

    printf("=== TESTE 1: ORDEM DE INICIATIVA ===\n");
    printListaCircular(lista);

    liberarLista(lista);
}

void teste2_um_turno_sem_morte(void) {
    ListaCircular lista = novaListaCircular();

    Personagem a = {"Aragorn", JOGADOR, "Guerreiro", 30, 10, 4, 0, {"", 0.0f}};
    Personagem g = {"Gandalf", JOGADOR, "Mago",      20, 12, 8, 0, {"", 0.0f}};
    Personagem o = {"Orc",     MONSTRO, "Orc",       18,  5, 6, 0, {"", 0.0f}};

    insereOrdenadoPorIniciativa(lista, a);
    insereOrdenadoPorIniciativa(lista, g);
    insereOrdenadoPorIniciativa(lista, o);

    printf("=== TESTE 2: UM TURNO SEM MORTE ===\n");
    printf("Antes do turno:\n");
    printListaCircular(lista);

    executaUmTurno(lista, lista->inicio);

    printf("Depois do turno:\n");
    printListaCircular(lista);

    liberarLista(lista);
}

void teste3_remocao_apos_derrota(void) {
    ListaCircular lista = novaListaCircular();
    Celula atual;

    Personagem a = {"Aragorn", JOGADOR, "Guerreiro", 30, 20, 4, 0, {"", 0.0f}};
    Personagem g = {"Gandalf", JOGADOR, "Mago",      20, 12, 8, 0, {"", 0.0f}};
    Personagem o = {"Orc",     MONSTRO, "Orc",       15,  5, 6, 0, {"", 0.0f}};

    insereOrdenadoPorIniciativa(lista, a);
    insereOrdenadoPorIniciativa(lista, g);
    insereOrdenadoPorIniciativa(lista, o);

    printf("=== TESTE 3: REMOCAO APOS DERROTA ===\n");
    printf("Antes do turno:\n");
    printListaCircular(lista);

    atual = lista->inicio;
    executaUmTurno(lista, atual);

    printf("Depois do turno:\n");
    printListaCircular(lista);

    liberarLista(lista);
}

void teste4_varios_turnos_com_habilidade(void) {
    ListaCircular lista = novaListaCircular();
    Celula atual;
    int turno;

    Personagem a  = {"Jogador A", JOGADOR, "Guerreiro", 25,  8, 2, 1, {"Golpe Heroico", 1.5f}};
    Personagem b  = {"Jogador B", JOGADOR, "Mago",      18, 10, 5, 1, {"Raio Arcano",   2.0f}};
    Personagem m1 = {"Monstro 1", MONSTRO, "Orc",       16,  6, 3, 0, {"",              0.0f}};
    Personagem m2 = {"Monstro 2", MONSTRO, "Goblin",    12,  4, 1, 0, {"",              0.0f}};

    insereOrdenadoPorIniciativa(lista, a);
    insereOrdenadoPorIniciativa(lista, b);
    insereOrdenadoPorIniciativa(lista, m1);
    insereOrdenadoPorIniciativa(lista, m2);

    atual = lista->inicio;

    
    srand(42);

    printf("=== TESTE 4: VARIOS TURNOS COM HABILIDADE ===\n");
    printListaCircular(lista);

    for (turno = 1; turno <= 6; turno++) {
        if (contarTipo(lista, JOGADOR) == 0 || contarTipo(lista, MONSTRO) == 0) {
            break;
        }

        printf("\nTurno %d\n", turno);
        atual = executaUmTurno(lista, atual);
        printListaCircular(lista);
    }

    liberarLista(lista);
}


int main(void) {


    teste1_iniciativa();
    printf("\n");

    teste2_um_turno_sem_morte();
    printf("\n");

    teste3_remocao_apos_derrota();
    printf("\n");

    teste4_varios_turnos_com_habilidade();


    return 0;
}