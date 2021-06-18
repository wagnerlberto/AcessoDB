using System;
using System.Data.SqlClient;
using System.Text;

namespace AcessoDB.Fintech
{
    class ContaBancaria
    {
        private int numero;
        private string titular;
        private double saldo;

        public void Depositar(double valor)
        {
            this.saldo += valor;
        }

        public double PegarSaldo()
        {
            return this.saldo;
        }

        public void Sacar(double valor)
        {
            if (this.PegarSaldo() >= valor)
                this.saldo -= valor;
        }

        public void Transferir(double valor, ContaBancaria contaDestino)
        {
            this.Sacar(valor);
            contaDestino.Depositar(valor);
        }

        public string Titular
        {
            get { return this.titular; }
            set
            {
                if (!value.Equals(""))
                    this.titular = value;
            }
        }

        public int Numero
        {
            get { return this.numero; }
            set
            {
                if (value > 0)
                    this.numero = value;
            }
        }

        public void Criar()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.UserID = "sa";
            builder.Password = "Mssql2021";
            builder.InitialCatalog = "EmpresaDB";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Clear();
                sb.Append("INSERT ContaBancaria (numero, titular, saldo)");
                sb.Append(" VALUES (@numero, @titular, @saldo);");
                String sql = sb.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@numero", this.numero);
                    command.Parameters.AddWithValue("@titular", this.titular);
                    command.Parameters.AddWithValue("@saldo", this.saldo);
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public void Alterar()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.UserID = "sa";
            builder.Password = "Mssql2021";
            builder.InitialCatalog = "EmpresaDB";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Clear();
                sb.Append("UPDATE ContaBancaria");
                sb.Append(" SET titular = @titular, saldo = @saldo");
                sb.Append(" WHERE numero = @numero;");
                String sql = sb.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@numero", this.numero);
                    command.Parameters.AddWithValue("@titular", this.titular);
                    command.Parameters.AddWithValue("@saldo", this.saldo);
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public void Excluir()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.UserID = "sa";
            builder.Password = "Mssql2021";
            builder.InitialCatalog = "EmpresaDB";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Clear();
                sb.Append("DELETE ContaBancaria");
                sb.Append(" WHERE numero = @numero;");
                String sql = sb.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@numero", this.numero);
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public void Recuperar()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.UserID = "sa";
            builder.Password = "Mssql2021";
            builder.InitialCatalog = "EmpresaDB";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();

                var sql = "";
                sql += "SELECT *";
                sql += " FROM ContaBancaria";
                sql += " WHERE numero = @numero;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@numero", this.numero);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if ( reader.Read() ) {
                          this.numero = reader.GetInt32(0);
                          this.titular = reader.GetString(1);
                        }else{
                          this.numero = 0;
                          this.titular = "";
                        }
                    }
                }
            }
        }

        public static SqlDataReader RecuperarTodos()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.UserID = "sa";
            builder.Password = "Mssql2021";
            builder.InitialCatalog = "EmpresaDB";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();

                var sql = "";
                sql += "SELECT *";
                sql += " FROM ContaBancaria";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    return command.ExecuteReader();
                }
            }
        }

    }
}
