using AmbiClean.Models;
using MySql.Data.MySqlClient;

namespace AmbiClean.Repositories
{
    class ChecklistRepository
    {

        private readonly Database.Database _db;

        public ChecklistRepository()
        {
            _db = Database.Database.Instance;
        }

        public bool Inserir(Checklist checklist)
        {
            try
            {
                using var connection = _db.GetConnection();
                string query = @"INSERT INTO Checklist 
                                 (Nome, Descricao, Ativo)
                                 VALUES (@nome, @descricao, @ativo)";
                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@nome", checklist.Nome);
                cmd.Parameters.AddWithValue("@descricao", checklist.Descricao);
                cmd.Parameters.AddWithValue("@ativo", checklist.Ativo);


                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir checklist: {ex.Message}");
                return false;
            }
        }

        public List<Checklist> BuscarTodos()
        {
            var checklist = new List<Checklist>();

            using var conn = _db.GetConnection();

            var sql = "SELECT Nome, Descricao, Ativo FROM Checklist";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                checklist.Add(new Checklist
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Descricao = reader.GetString(2),
                    Ativo = reader.GetBoolean(3)
                });
            }
            return checklist;
        }

        public Checklist BuscarPorId(int id)
        {
            using var conn = _db.GetConnection();

            var sql = "SELECT Nome, Descricao, Ativo FROM Checklist WHERE id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Checklist
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Descricao = reader.GetString(2),
                    Ativo = reader.GetBoolean(3)
                };
            }

            return null;
        }

        public void Atualizar(Checklist checklist)
        {
            using var conn = _db.GetConnection();

            var sql = @"UPDATE Usuario SET 
                        Nome = @nome,
                        Descricao = @descricao,
                        Ativo = @ativo";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nome", checklist.Nome);
            cmd.Parameters.AddWithValue("@descricao", checklist.Descricao);
            cmd.Parameters.AddWithValue("@ativo", checklist.Ativo);

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
