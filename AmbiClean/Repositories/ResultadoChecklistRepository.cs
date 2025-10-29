using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using AmbiClean.Database;
using AmbiClean.Models;

namespace AmbiClean.Repositories
{
    public class ResultadoChecklistRepository
    {
        private readonly Database.Database _db;

        public ResultadoChecklistRepository()
        {
            _db = Database.Database.Instance;
        }

        // 🔹 Inserir novo resultado
        public bool Inserir(ResultadoChecklist resultado)
        {
            try
            {
                using var connection = _db.GetConnection();
                string query = @"INSERT INTO ResultadoChecklist 
                                 (ExecucaoId, ChecklistId, Marcado, Comentario, MarcadoEm)
                                 VALUES (@execucaoId, @checklistId, @marcado, @comentario, @marcadoEm)";

                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@execucaoId", resultado.ExecucaoId);
                cmd.Parameters.AddWithValue("@checklistId", resultado.ChecklistId);
                cmd.Parameters.AddWithValue("@marcado", resultado.Marcado);
                cmd.Parameters.AddWithValue("@comentario", resultado.Comentario);
                cmd.Parameters.AddWithValue("@marcadoEm", resultado.MarcadoEm);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir resultado de checklist: {ex.Message}");
                return false;
            }
        }

        // 🔹 Buscar todos os resultados
        public List<ResultadoChecklist> BuscarTodos()
        {
            var resultados = new List<ResultadoChecklist>();

            using var conn = _db.GetConnection();
            var sql = "SELECT Id, ExecucaoId, ChecklistId, Marcado, Comentario, MarcadoEm FROM ResultadoChecklist";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                resultados.Add(new ResultadoChecklist(
                    reader.GetInt32("Id"),
                    reader.GetInt32("ExecucaoId"),
                    reader.GetInt32("ChecklistId"),
                    reader.GetBoolean("Marcado"),
                    reader.GetString("Comentario"),
                    reader.GetDateTime("MarcadoEm")
                ));
            }

            return resultados;
        }

        // 🔹 Buscar por ID
        public ResultadoChecklist BuscarPorId(int id)
        {
            using var conn = _db.GetConnection();
            var sql = "SELECT Id, ExecucaoId, ChecklistId, Marcado, Comentario, MarcadoEm FROM ResultadoChecklist WHERE Id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new ResultadoChecklist(
                    reader.GetInt32("Id"),
                    reader.GetInt32("ExecucaoId"),
                    reader.GetInt32("ChecklistId"),
                    reader.GetBoolean("Marcado"),
                    reader.GetString("Comentario"),
                    reader.GetDateTime("MarcadoEm")
                );
            }

            return null;
        }

        // 🔹 Buscar por Execução
        public List<ResultadoChecklist> BuscarPorExecucaoId(int execucaoId)
        {
            var resultados = new List<ResultadoChecklist>();

            using var conn = _db.GetConnection();
            var sql = "SELECT Id, ExecucaoId, ChecklistId, Marcado, Comentario, MarcadoEm FROM ResultadoChecklist WHERE ExecucaoId = @execucaoId";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@execucaoId", execucaoId);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                resultados.Add(new ResultadoChecklist(
                    reader.GetInt32("Id"),
                    reader.GetInt32("ExecucaoId"),
                    reader.GetInt32("ChecklistId"),
                    reader.GetBoolean("Marcado"),
                    reader.GetString("Comentario"),
                    reader.GetDateTime("MarcadoEm")
                ));
            }

            return resultados;
        }

        // 🔹 Atualizar resultado existente
        public void Atualizar(ResultadoChecklist resultado)
        {
            using var conn = _db.GetConnection();

            var sql = @"UPDATE ResultadoChecklist SET 
                        ExecucaoId = @execucaoId,
                        ChecklistId = @checklistId,
                        Marcado = @marcado,
                        Comentario = @comentario,
                        MarcadoEm = @marcadoEm
                        WHERE Id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", resultado.Id);
            cmd.Parameters.AddWithValue("@execucaoId", resultado.ExecucaoId);
            cmd.Parameters.AddWithValue("@checklistId", resultado.ChecklistId);
            cmd.Parameters.AddWithValue("@marcado", resultado.Marcado);
            cmd.Parameters.AddWithValue("@comentario", resultado.Comentario);
            cmd.Parameters.AddWithValue("@marcadoEm", resultado.MarcadoEm);

            cmd.ExecuteNonQuery();
        }

        // 🔹 Excluir resultado
        public void Excluir(int id)
        {
            using var conn = _db.GetConnection();

            var sql = "DELETE FROM ResultadoChecklist WHERE Id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
