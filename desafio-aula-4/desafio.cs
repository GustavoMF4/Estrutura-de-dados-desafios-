using System;
using System.Collections.Generic;

 
class Pokemon
{
    public string Nome;
    public string Tipo;
    public int HP;
    public int Ataque;
    public int Defesa;

    public Pokemon(string nome, string tipo, int hp, int ataque, int defesa)
    {
        Nome = nome;
        Tipo = tipo;
        HP = hp;
        Ataque = ataque;
        Defesa = defesa;
    }

    public virtual void Atacar(Pokemon alvo)
    {
        int dano = Ataque - alvo.Defesa;
        if (dano < 1) dano = 1;

        alvo.HP -= dano;
        if (alvo.HP < 0) alvo.HP = 0;

        Console.WriteLine($"{Nome} atacou {alvo.Nome} e causou {dano} de dano.");
        Console.WriteLine($"{alvo.Nome} agora está com {alvo.HP} de vida.\n");
    }
}


class PokemonTFogo : Pokemon
{
    public PokemonTFogo(string nome, int hp, int ataque, int defesa)
        : base(nome, "Fogo", hp, ataque, defesa) { }

    public override void Atacar(Pokemon alvo)
    {
        int dano = (Ataque - alvo.Defesa) + 2;
        if (dano < 1) dano = 1;

        alvo.HP -= dano;
        if (alvo.HP < 0) alvo.HP = 0;

        Console.WriteLine($"{Nome} atacou {alvo.Nome} e causou {dano} de dano.");
        Console.WriteLine($"{alvo.Nome} agora está com {alvo.HP} de vida.\n");
    }
}


class PokemonTAgua : Pokemon
{
    public PokemonTAgua(string nome, int hp, int ataque, int defesa)
        : base(nome, "Agua", hp, ataque, defesa) { }

    public override void Atacar(Pokemon alvo)
    {
        base.Atacar(alvo);
        HP += 2;
    }
}


class Treinador
{
    public string Nome;
    public List<Pokemon> Pokemons = new List<Pokemon>();

    public Treinador(string nome)
    {
        Nome = nome;
    }

    public void AdicionarPokemon(Pokemon p)
    {
        Pokemons.Add(p);
    }

    public Pokemon EscolherPokemon(int i)
    {
        return Pokemons[i];
    }
}


public class Program
{
    public static void Main()
    {
        Treinador t1 = new Treinador("Red");
        Treinador t2 = new Treinador("Membro equipe da Rocket");

        // 2 Pokémon cada
        t1.AdicionarPokemon(new PokemonTFogo("Arcanine", 90, 110, 80));
        t1.AdicionarPokemon(new Pokemon("Jolteon", "Eletrico", 65, 65, 60));

        t2.AdicionarPokemon(new PokemonTAgua("Gyarados", 95, 125, 79));
        t2.AdicionarPokemon(new Pokemon("Darkrai", "dark", 70, 90, 90));

        Pokemon p1 = t1.EscolherPokemon(0);
        Pokemon p2 = t2.EscolherPokemon(0);

        Console.WriteLine($"{t1.Nome} escolheu {p1.Nome}!");
        Console.WriteLine($"{t2.Nome} escolheu {p2.Nome}!\n");

        
        while (p1.HP > 0 && p2.HP > 0)
        {
            p1.Atacar(p2);
            if (p2.HP <= 0) break;

            p2.Atacar(p1);
        }

        
        if (p1.HP > 0)
            Console.WriteLine($"{p1.Nome} venceu a batalha!");
        else
            Console.WriteLine($"{p2.Nome} venceu a batalha!");
    }
}