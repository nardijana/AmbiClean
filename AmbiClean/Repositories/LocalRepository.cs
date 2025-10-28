using AmbiClean.Models;
using MySql.Data.MySqlClient;

namespace AmbiClean.Repositories
{
    class LocalRepository
    {
        private readonly Database.Database _db;

        public LocalRepository()
        {
            _db = Database.Database.Instance;
        }

        public bool Inserir(Local local)
        {
            try
            {
                using var connection = _db.GetConnection();
                string query = @"INSERT INTO Local 
                                 (Nome, Endereco, Ativo)
                                 VALUES (@nome, @endereco, @ativo)";
                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@nome", local.Nome);
                cmd.Parameters.AddWithValue("@endereco", local.Endereco);
                cmd.Parameters.AddWithValue("@ativo", local.Ativo);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir local: {ex.Message}");
                return false;
            }
        }

        public List<Local> BuscarTodos()
        {
            var local = new List<Local>();

            using var conn = _db.GetConnection();

            var sql = "SELECT Nome, Endereco, Ativo FROM Local";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                local.Add(new Local
                {
                    Nome = reader.GetString(0),
                    Endereco = reader.GetString(1),
                    Ativo = reader.GetBoolean(2)
                });
            }
            return local;
        }

        public Local BuscarPorId(int id)
        {
            using var conn = _db.GetConnection();

            var sql = "SELECT Nome, Endereco, Ativo FROM Local WHERE id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Local
                {
                    Nome = reader.GetString(0),
                    Endereco = reader.GetString(1),
                    Ativo = reader.GetBoolean(2)
                };
            }

            return null;
        }

        public void Atualizar(Local local)
        {
            using var conn = _db.GetConnection();

            var sql = @"UPDATE Usuario SET 
                        Nome = @nome,
                        Endereco = @endereco,
                        Ativo = @ativo";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nome", local.Nome);
            cmd.Parameters.AddWithValue("@endereco", local.Endereco);
            cmd.Parameters.AddWithValue("@ativo", local.Ativo);

            cmd.ExecuteNonQuery();
        }

        public void Excluir(int id)
        {
            using var conn = _db.GetConnection();

            var sql = "DELETE FROM Checklist WHERE id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
