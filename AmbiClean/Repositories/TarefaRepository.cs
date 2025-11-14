using AmbiClean.Models;
using MySql.Data.MySqlClient;

namespace AmbiClean.Repositories
{
    class TarefaRepository
    {
        private readonly Database.Database _db;

        public TarefaRepository()
        {
            _db = Database.Database.Instance;
        }

        public bool Inserir(Tarefa tarefa)
        {
            try
            {
                using var connection = _db.GetConnection();
                string query = @"INSERT INTO Tarefa 
                                 (AreaId, ChecklistId, Nome, Descricao, MinutosEstimados, Ativo)
                                 VALUES (@areaId, @checklistId, @nome, @descricao, @minutosEstimados, @ativo)";
                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@areaId", tarefa.AreaId);
                cmd.Parameters.AddWithValue("@checklistId", tarefa.ChecklistId);
                cmd.Parameters.AddWithValue("@nome", tarefa.Nome);
                cmd.Parameters.AddWithValue("@descricao", tarefa.Descricao);
                cmd.Parameters.AddWithValue("@minutosEstimados", tarefa.MinutosEstimados);
                cmd.Parameters.AddWithValue("@ativo", tarefa.Ativo);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir tarefa: {ex.Message}");
                return false;
            }
        }

        public List<Tarefa> BuscarTodos()
        {
            var tarefa = new List<Tarefa>();

            using var conn = _db.GetConnection();

            var sql = "SELECT AreaId, ChecklistId, Nome, Descricao, MinutosEstimados, Ativo FROM Tarefa";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                tarefa.Add(new Tarefa
                {
                    AreaId = reader.GetInt32(0),
                    ChecklistId = reader.GetInt32(1),
                    Nome = reader.GetString(2),
                    Descricao = reader.GetString(3),
                    MinutosEstimados = reader.GetInt32(4),
                    Ativo = reader.GetBoolean(5)
                });
            }
            return tarefa;
        }

        public Tarefa BuscarPorId(int id)
        {
            using var conn = _db.GetConnection();

            var sql = "SELECT AreaId, ChecklistId, Nome, Descricao, MinutosEstimados, Ativo FROM Tarefa WHERE id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Tarefa
                {
                    AreaId = reader.GetInt32(0),
                    ChecklistId = reader.GetInt32(1),
                    Nome = reader.GetString(2),
                    Descricao = reader.GetString(3),
                    MinutosEstimados = reader.GetInt32(4),
                    Ativo = reader.GetBoolean(5)
                };
            }

            return null;
        }

        public void Atualizar(Tarefa tarefa)
        {
            using var conn = _db.GetConnection();

            var sql = @"UPDATE Tarefa SET 
                        AreaId = @areaId,
                        ChecklistId = @checklistId,
                        Nome = @nome,
                        Descricao = @descricao,
                        MinutosEstimados = @minutosEstimados,
                        Ativo = @ativo";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@areaId", tarefa.AreaId);
            cmd.Parameters.AddWithValue("@checklistId", tarefa.ChecklistId);
            cmd.Parameters.AddWithValue("@Nome", tarefa.Nome);
            cmd.Parameters.AddWithValue("@descricao", tarefa.Descricao);
            cmd.Parameters.AddWithValue("@minutosEstimados", tarefa.MinutosEstimados);
            cmd.Parameters.AddWithValue("@ativo", tarefa.Ativo);

            cmd.ExecuteNonQuery();
        }

        public void Excluir(int id)
        {
            using var conn = _db.GetConnection();

            var sql = "DELETE FROM Tarefa WHERE id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
