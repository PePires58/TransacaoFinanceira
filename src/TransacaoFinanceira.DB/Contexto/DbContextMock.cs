using System.Collections.Generic;
using TransacaoFinanceira.Entidades;

namespace TransacaoFinanceira.DB.Contexto
{
    public class DbContextMock
    {
        public DbContextMock()
        {
            Contas =
            [
                new(938485762, 180),
                new(347586970, 1200),
                new(2147483649, 0),
                new(675869708, 4900),
                new(238596054, 478),
                new(573659065, 787),
                new(210385733, 10),
                new(674038564, 400),
                new(563856300, 1200)
            ];


            ContasDb = [];
            foreach (var conta in Contas)
                ContasDb.Add(conta.Id, conta);

        }

        public Dictionary<long, Conta> ContasDb { get; set; }
        private List<Conta> Contas { get; set; }
    }
}
