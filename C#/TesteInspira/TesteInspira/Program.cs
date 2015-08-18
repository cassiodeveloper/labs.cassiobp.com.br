using System;
using System.Linq;

namespace TesteInspira
{
    class Program
    {
        static void Main(string[] args)
        {
            string entrada = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed viverra nunc tortor, et scelerisque libero porttitor id. Nulla sed massa pharetra, suscipit ex sed, accumsan elit. Nunc turpis enim, porttitor vitae nibh id, volutpat malesuada purus. Aliquam laoreet orci eu nisi condimentum, non porttitor nulla pretium. Suspendisse potenti. Vestibulum dui nulla, faucibus a tempus ac, finibus quis metus. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.
Cras finibus at tellus sit amet interdum. Phasellus sit amet pretium purus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; In eget augue efficitur, porta eros non, ullamcorper lectus. Sed eu nisi sed odio sodales vehicula. Suspendisse mollis, enim ut pulvinar tempus, ligula massa ultricies odio, vitae maximus dolor justo id arcu. Donec et mi sodales, porttitor ex sit amet, consectetur mi. Suspendisse vulputate imperdiet felis, vitae mollis est tempor vitae. Nunc finibus bibendum lorem ac tempus.
Fusce imperdiet elit vitae elementum blandit. Duis et aliquam erat. Nam ac ligula sed nunc ullamcorper iaculis. Nam venenatis sollicitudin iaculis. Pellentesque tincidunt neque et ante gravida faucibus. Ut non porttitor massa. Donec non feugiat est, at hendrerit tellus. In consequat eros sed nulla volutpat, tempus rutrum odio eleifend. Morbi nulla nunc, bibendum non interdum eu, pretium id nisi. Cras dolor augue, placerat eu scelerisque sit amet, eleifend nec erat. Nam non enim non nisl fermentum dictum nec tincidunt nisl.
Proin ac ligula a leo convallis venenatis. Fusce pretium turpis sit amet dapibus imperdiet. Phasellus sit amet auctor ex, eu bibendum risus. Donec blandit volutpat nisi sit amet pharetra. Suspendisse volutpat ac dui in sollicitudin. Nunc vulputate sodales eros ac tincidunt. Ut elementum finibus enim in viverra. Vivamus consequat imperdiet massa vulputate cursus. Sed eleifend erat non metus egestas, non luctus risus euismod. Sed urna ante, sollicitudin non scelerisque sit amet, maximus id sem. Nam a laoreet ante. Nam consectetur volutpat mi, ut condimentum tellus facilisis vitae. Quisque feugiat elit sed lectus hendrerit, in auctor orci sodales. Nullam malesuada purus sed consequat placerat.
Maecenas a placerat felis. Phasellus metus lectus, consequat eget arcu eu, posuere aliquet lacus. Praesent tempor justo at ullamcorper faucibus. Suspendisse potenti. Integer vel aliquet enim. Nunc tristique dignissim fringilla. Proin tincidunt hendrerit quam. Morbi quis ex eu mi tincidunt lacinia. Praesent mollis scelerisque neque, rhoncus tempor nisl condimentum a. Sed mollis sollicitudin neque, id luctus turpis vulputate eget.";

            ContarFrasesMaisDeDezPalavras(entrada);
            ContarVogaisEConsoantes(entrada);

            Console.ReadLine();
        }

        private static void ContarFrasesMaisDeDezPalavras(string entrada)
        {
            string[] frases = entrada.Split('.');
            int qtdPalavras = 0;
            int totalMaiorDez = 0;

            for (int i = 0; i < frases.Length; i++)
            {
                // conta as palavras
                qtdPalavras = frases[i].Split(' ').Length;

                // verifica quantas frases possuem mais de dez palavras
                if (qtdPalavras > 10)
                    totalMaiorDez++;
            }

            Console.WriteLine("Existem " + totalMaiorDez + " frases com mais de 10 palavras.");
        }

        private static void ContarVogaisEConsoantes(string texto)
        {
            string[] palavras = texto.Split(' ');

            int qntVogais = 0;
            int total = 0;

            for (int i = 0; i < palavras.Length; i++)
            {
                string palavra = palavras[i];

                for (int x = 0; x < palavra.Length; x++)
                {
                    qntVogais = 0;

                    if (palavra.ElementAt(x).ToString().ToLower() == "a" || palavra.ElementAt(x).ToString().ToLower() == "e" || palavra.ElementAt(x).ToString().ToLower() == "i" || palavra.ElementAt(x).ToString().ToLower() == "o" || palavra.ElementAt(x).ToString().ToLower() == "u")
                        qntVogais++;                    
                }

                int qntLetras = palavra.Length;
                int qntConsoantes = qntLetras - qntVogais;              

                if (qntConsoantes == qntVogais)
                    total++;
            }

            Console.WriteLine("Existem " + total + " de palavras com nº de consoantes igual ao de vogais.");
        }
    }
}