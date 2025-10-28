using AmbiClean.Models;
using MySql.Data.MySqlClient;

namespace AmbiClean.Repositories
{
    class AreaRepository
    {
        private readonly Database.Database _db;

        public AreaRepository()
        {
            _db = Database.Database.Instance;
        }

        public bool Inserir(Area area)
        {
            try
            {
                using var connection = _db.GetConnection();
                string query = @"INSERT INTO Area 
                                 (LocalId, Nome, Descricao)
                                 VALUES (@localId, @nome, @descricao)";
                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@localId", area.LocalId);
                cmd.Parameters.AddWithValue("@nome", area.Nome);
                cmd.Parameters.AddWithValue("@descricao", area.Descricao);


                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir area: {ex.Message}");
                return false;
            }
        }

        public List<Area> BuscarTodos()
        {
            var area = new List<Area>();

            using var conn = _db.GetConnection();

            var sql = "SELECT LocalId, Nome, Descricao FROM Area";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                area.Add(new Area
                {
                    LocalId = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Descricao = reader.GetString(2)
                });
            }
            return area;
        }

        public Area BuscarPorId(int id)
        {
            using var conn = _db.GetConnection();

            var sql = "SELECT Nome, Descricao, Ativo FROM Checklist WHERE id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Area
                {
                    LocalId = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Descricao = reader.GetString(2)
                };
            }

            return null;
        }

        public void Atualizar(Area area)
        {
            using var conn = _db.GetConnection();

            var sql = @"UPDATE Usuario SET 
                        LocalId = @localId,
                        Nome = @nome,
                        Descricao = @descricao";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@localId", area.LocalId);
            cmd.Parameters.AddWithValue("@nome", area.Nome);
            cmd.Parameters.AddWithValue("@descricao", area.Descricao);

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
