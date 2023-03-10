/*
 * Aluna:       Isabella Vecchi Ferreira
 * Matrícula:   11621ECP002
 */


using System;
using System.Collections.Generic;
using System.Text;

namespace a_estrela
{
    class Ponto
    {
        //atributos
        public int x;
        public int y;
        public double f;
        public double g;
        public double h;
        public List<Ponto> vizinho;
        public Ponto anterior;
        public Boolean wall;

        //constructor
        public Ponto ()
        {

        }
        public Ponto(int i, int j)
        {
            x = i;
            y = j;
            g = 0;
            h = 0;
            f = 0;
            vizinho = new List<Ponto>();
            anterior = null;
            wall = false;
        }
        public Ponto(int i, int j, Boolean wall)
        {
            x = i;
            y = j;
            g = 0;
            h = 0;
            f = 0;
            vizinho = new List<Ponto>();
            anterior = null;
            this.wall = wall;
        }

        public Ponto(int i, int j, double _g)
        {
            x = i;
            y = j;
            f = 0;
            g = _g;
            h = 0;
            vizinho = new List<Ponto>();
            anterior = null;
            wall = false;
        }
    }
}
