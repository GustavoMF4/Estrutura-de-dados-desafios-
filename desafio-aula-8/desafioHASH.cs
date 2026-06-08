using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Dictionary<string, int> frequencia =
            new Dictionary<string, int>();

        int totalPalavras = 0;

        Console.WriteLine(
            "Digitar texto:" // linha vazia para continuar
        );

        while (true)
        {
            string linha = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(linha))
                break;

            linha = linha.ToLower();

            char[] pontuacoes =
            {
                '.', ',', '!', '?',
                ';', ':', '"', '\'',
                '(', ')'
            };

            foreach (char c in pontuacoes)
            {
                linha = linha.Replace(c.ToString(), "");
            }

            string[] palavras =
                linha.Split(
                    ' ',
                    StringSplitOptions.RemoveEmptyEntries
                );

            foreach (string palavra in palavras)
            {
                totalPalavras++;

                if (frequencia.ContainsKey(palavra))
                {
                    frequencia[palavra]++;
                }
                else
                {
                    frequencia[palavra] = 1;
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine("=== Resultado ===");

        Console.WriteLine(
            $"Total de palavras: {totalPalavras}"
        );

        Console.WriteLine(
            $"Palavras distintas: {frequencia.Count}"
        );

        Console.WriteLine();
        Console.WriteLine(
            "Top 10 palavras mais frequentes:"
        );

        var topPalavras =
            frequencia
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .Take(10);

        int posicao = 1;

        foreach (var item in topPalavras)
        {
            Console.WriteLine(
                $"{posicao,2}. \"{item.Key}\" - {item.Value}"
            );

            posicao++;
        }
    }
}