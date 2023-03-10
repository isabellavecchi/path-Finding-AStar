/*
 * Aluna:       Isabella Vecchi Ferreira
 * Matrícula:   11621ECP002
 */


using System;
using System.Collections.Generic;
using System.Linq;


namespace a_estrela
{
    class Program
    {
        static List<Ponto> pontos;
        static List<Ponto> openSet;
        static List<Ponto> closedSet;
        static List<Ponto> path;
        Random randNum = new Random();
        
        static void Main(string[] args)
        {
            double distancia(Ponto a, Ponto b)
            {
                double dx = Math.Pow((a.x - b.x), 2);
                double dy = Math.Pow((a.y - b.y), 2);
                double d  = Math.Sqrt(dx + dy);
                //double d = b.x - a.x + b.y - a.y;
                return d;

            }

            int rows = 10;
            int cols = 10;

            pontos = new List<Ponto>();

            openSet = new List<Ponto>();
            closedSet = new List<Ponto>();

            path = new List<Ponto>();

            //adicionando os Pontos
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    //adicionando obstáculos
                    if ((i != 0 || j != 0) || (i != rows - 1 || j != cols - 1))
                    {
                        double rand = new Random().NextDouble();
                        if (rand < 0.3)
                        {
                            pontos.Add(new Ponto(i, j,true));
                        }
                        else
                        {
                            pontos.Add(new Ponto(i, j));
                        }
                    }
                }
            }

            //adicionando os vizinhos
            foreach (Ponto o in pontos)
            {
                if (o.x > 0)
                {
                    o.vizinho.Add(pontos.Find(ponto => ponto.x == o.x - 1 && ponto.y == o.y));
                }
                if (o.x < rows-1)
                {
                    o.vizinho.Add(pontos.Find(ponto => ponto.x == o.x + 1 && ponto.y == o.y));
                }
                if (o.y > 0)
                {
                    o.vizinho.Add(pontos.Find(ponto => ponto.x == o.x && ponto.y == o.y - 1));
                }
                if (o.y < cols-1)
                {
                    o.vizinho.Add(pontos.Find(ponto => ponto.x == o.x && ponto.y == o.y + 1));
                }
                if (o.x < rows - 1 && o.y < cols - 1)
                {
                    o.vizinho.Add(pontos.Find(ponto => ponto.x == o.x + 1 && ponto.y == o.y + 1));
                }
                if (o.x < rows - 1 && o.y > 0)
                {
                    o.vizinho.Add(pontos.Find(ponto => ponto.x == o.x + 1 && ponto.y == o.y - 1));
                }
                if (o.x > 0 && o.y > 0)
                {
                    o.vizinho.Add(pontos.Find(ponto => ponto.x == o.x - 1 && ponto.y == o.y - 1));
                }
                if (o.x > 0 && o.y < cols - 1)
                {
                    o.vizinho.Add(pontos.Find(ponto => ponto.x == o.x - 1 && ponto.y == o.y + 1));
                }
            }

            Ponto start = pontos.Find(ponto => ponto.x == 0 && ponto.y == 0);
            Ponto end = pontos.Find(ponto => ponto.x == rows - 1 && ponto.y == cols - 1);

            start.g = 0;
            start.h = distancia(start, end);
            start.f = start.h;
            openSet.Add(start);

            Ponto winner = start;

            //  ROLANDO O PROGRAMA
            while (openSet.Count != 0)  //checando se o programa acabou
            {
                /*foreach(Ponto p in openSet){
                    if (p.f <= winner.f)
                    //menor ou igual baseado no outro vídeo que mandou
                    //https://www.youtube.com/watch?v=-L-WgKMFuhE
                    {
                        winner = p;
                    }
                }*/

                //melhor ainda, para percorrer toda a lista
                winner = openSet.Find(o => o.f == openSet.Min(x => x.f));

                Ponto current = winner;
                openSet.Remove(openSet.Find(ponto => ponto.x == current.x && ponto.y == current.y));
                closedSet.Add(current);

                //colocando vizinhos no openSet
                foreach (Ponto v in current.vizinho)
                {
                    if (!(closedSet.Contains(v)) && !v.wall)
                    {
                        //setando o custo de passos percorridos no caminho (g)
                        double tempG = current.g + distancia(current,v);

                        if (openSet.Contains(v))
                        {
                            if(tempG < v.g)
                            {
                                v.g = tempG;
                                v.anterior = current;
                            }

                        } else
                        {
                            v.g = tempG;
                            //setando a heurística (h)
                            v.h = distancia(v, end);
                            v.f = v.g + v.h;
                            v.anterior = current;
                            //adicionando vizinho não visitado ao openSet
                            openSet.Add(v);
                        }
                    }
                }
            }

            //verificando se o caminho está bloqueado
            if (!(closedSet.Contains(end)))
            {
                Console.WriteLine("Não tem Solução.");
            }
            else
            {
                //função recursiva para pegar o caminho
                void getAnterior(Ponto temp)
                {
                    Ponto temp2 = temp.anterior;
                    if (temp != closedSet.Find(ponto => ponto.x == start.x && ponto.y == start.y))
                    {
                        getAnterior(temp2);
                    }
                    path.Add(temp);
                }

                Ponto temp = closedSet.Find(ponto => ponto.x == end.x && ponto.y == end.y);
                getAnterior(temp);

                Console.WriteLine("Caminho percorrido:");
                foreach (Ponto p in path)
                {
                    Console.WriteLine(p.x + ", " + p.y);
                }

                //imprimindo caminho na tela
                for (int i = -1; i < rows + 1; i++)
                {
                    for (int j = -1; j < cols + 1; j++)
                    {
                        if (i == -1 || i == rows)
                        {
                            if (j == cols)
                            {
                                Console.Write("_\n");
                            }
                            else
                            {
                                Console.Write("_\t");
                            }
                        }
                        else if (j == -1 && i != -1 && i != rows)
                        {
                            Console.Write("|\t");
                        }
                        else if (j == cols && i != -1 && i != rows)
                        {
                            Console.Write("|\n");
                        }
                        else
                        {
                            Ponto posicao = pontos.Find(ponto => ponto.x == i && ponto.y == j);
                            /*Console.Write(posicao.x + ", " + posicao.y + " -> f: " + posicao.f + "\t");
                            Console.Write('\t');*/
                            if (posicao.wall)
                            {
                                Console.Write("X\t");
                            } else if (path.Contains(posicao))
                            {
                                Console.Write(posicao.x + ", " + posicao.y + "\t");
                            }
                            else
                            {
                                Console.Write('\t');
                            }
                        }
                    }
                }
            }
        }
    }
}
