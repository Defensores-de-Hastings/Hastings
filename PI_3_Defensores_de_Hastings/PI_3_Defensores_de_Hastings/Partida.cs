using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using KingMeServer;

namespace PI_3_Defensores_de_Hastings
{
    class Partida
    {
        public int id { get; set; }
        public string nome { get; set; }
        public DateTime data { get; set; }
        public char status { get; set; }

        public static List<Partida> ListarPartidas()
        {
            string retorno = Jogo.ListarPartidas("A");

            // Replace carriage returns
            retorno = retorno.Replace("\r", "");
            if (retorno.Length > 0)
            {
                retorno = retorno.Substring(0, retorno.Length - 1);
            }

            string[] partidas = retorno.Split('\n');

            // Pre-allocate list capacity for better performance
            List<Partida> ListaPartidas = new List<Partida>(partidas.Length);    

            for(int i = 0; i < partidas.Length; i++)
            {
                string partida = partidas[i];
                
                // Skip empty entries from the Split operation
                if (string.IsNullOrWhiteSpace(partida))
                {
                    continue;
                }
                
                string[] dados = partida.Split(',');

                // Use object initializer for cleaner code
                Partida match = new Partida
                {
                    id = Convert.ToInt32(dados[0]),
                    nome = dados[1],
                    data = Convert.ToDateTime(dados[2]),
                    status = Convert.ToChar(dados[3])
                };

                ListaPartidas.Add(match);
            }   

            return ListaPartidas;
        } 
        
    }
}
