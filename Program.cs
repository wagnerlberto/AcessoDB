using System;
using AcessoDB.Fintech;
using System.Data.SqlClient;

namespace AcessoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var conta1 = new ContaBancaria();
            var conta2 = new ContaBancaria();
            var conta3 = new ContaBancaria();

            // conta1.Numero = 11;
            // conta1.Titular = "Bruno";
            // conta1.Depositar(111);
            // conta1.Criar();

            // conta2.Numero = 22;
            // conta2.Titular = "Gois";
            // conta2.Depositar(222);
            // conta2.Criar();

            // conta.Numero = 11;
            // conta.Titular = "Br";
            // conta.Depositar(100.23);

            // conta.Alterar();

            // conta1.Numero = 11;
            // conta1.Excluir();
            // conta2.Numero = 22;
            // conta2.Excluir();

            // conta3.Numero = 22;
            // conta3.Recuperar();
            // Console.WriteLine("Número: {0} |Titular: {1} |Saldo: {2}",
            // conta3.Numero,conta3.Titular,conta3.PegarSaldo());

            SqlDataReader reader = ContaBancaria.RecuperarTodos();
            while (reader.Read())
            {
                Console.WriteLine("{0} {1}", reader.GetInt32(0), reader.GetString(1));
            }

        }
    }
}
