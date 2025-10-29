using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using AmbiClean.Database;
using AmbiClean.Models;

namespace AmbiClean.Repositories
{
    public class ExecucaoTarefaRepository
    {
        private readonly Database.Database _db;

        public ExecucaoTarefaRepository()
        {
            _db = Database.Database.Instance;
        }

        // 🔹 Inserir nova execução de tarefa
        public bool Inserir(ExecucaoTarefa execucao)
        {
            try
            {
                using var connection = _db.GetConnection();
                string query = @"INSERT INTO ExecucaoTarefa 
                                 (TarefaId, UsuarioId, AgendadoInicio, AgendadoFim, Inicio, Fim, Status, Observacoes, CriadoEm)
                                 VALUES (@tarefaId, @usuarioId, @agendadoInicio, @agendadoFim, @inicio, @fim, @status, @observacoes, @criadoEm)";

                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@tarefaId", execucao.TarefaId);
                cmd.Parameters.AddWithValue("@usuarioId", execucao.UsuarioId);
                cmd.Parameters.AddWithValue("@agendadoInicio", execucao.AgendadoInicio);
                cmd.Parameters.AddWithValue("@agendadoFim", execucao.AgendadoFim);
                cmd.Parameters.AddWithValue("@inicio", execucao.Inicio);
                cmd.Parameters.AddWithValue("@fim", execucao.Fim);
                cmd.Parameters.AddWithValue("@status", execucao.Status);
                cmd.Parameters.AddWithValue("@observacoes", execucao.Observacoes);
                cmd.Parameters.AddWithValue("@criadoEm", execucao.CriadoEm);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir execução de tarefa: {ex.Message}");
                return false;
            }
        }

        // 🔹 Buscar todas as execuções
        public List<ExecucaoTarefa> BuscarTodos()
        {
            var execucoes = new List<ExecucaoTarefa>();

            using var conn = _db.GetConnection();
            var sql = @"SELECT Id, TarefaId, UsuarioId, AgendadoInicio, AgendadoFim, Inicio, Fim, Status, Observacoes, CriadoEm 
                        FROM ExecucaoTarefa";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                execucoes.Add(new ExecucaoTarefa(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetDateTime(3),
                    reader.GetDateTime(4),
                    reader.GetDateTime(5),
                    reader.GetDateTime(6),
                    reader.GetString(7),
                    reader.IsDBNull(8) ? null : reader.GetString(8),
                    reader.GetDateTime(9)
                ));
            }

            return execucoes;
        }

        // 🔹 Buscar execução por ID
        public ExecucaoTarefa BuscarPorId(int id)
        {
            using var conn = _db.GetConnection();

            var sql = @"SELECT Id, TarefaId, UsuarioId, AgendadoInicio, AgendadoFim, Inicio, Fim, Status, Observacoes, CriadoEm 
                        FROM ExecucaoTarefa WHERE Id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new ExecucaoTarefa(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetDateTime(3),
                    reader.GetDateTime(4),
                    reader.GetDateTime(5),
                    reader.GetDateTime(6),
                    reader.GetString(7),
                    reader.IsDBNull(8) ? null : reader.GetString(8),
                    reader.GetDateTime(9)
                );
            }

            return null;
        }

        // 🔹 Atualizar execução existente
        public void Atualizar(ExecucaoTarefa execucao)
        {
            using var conn = _db.GetConnection();

            var sql = @"UPDATE ExecucaoTarefa SET 
                        TarefaId = @tarefaId,
                        UsuarioId = @usuarioId,
                        AgendadoInicio = @agendadoInicio,
                        AgendadoFim = @agendadoFim,
                        Inicio = @inicio,
                        Fim = @fim,
                        Status = @status,
                        Observacoes = @observacoes,
                        CriadoEm = @criadoEm
                        WHERE Id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", execucao.Id);
            cmd.Parameters.AddWithValue("@tarefaId", execucao.TarefaId);
            cmd.Parameters.AddWithValue("@usuarioId", execucao.UsuarioId);
            cmd.Parameters.AddWithValue("@agendadoInicio", execucao.AgendadoInicio);
            cmd.Parameters.AddWithValue("@agendadoFim", execucao.AgendadoFim);
            cmd.Parameters.AddWithValue("@inicio", execucao.Inicio);
            cmd.Parameters.AddWithValue("@fim", execucao.Fim);
            cmd.Parameters.AddWithValue("@status", execucao.Status);
            cmd.Parameters.AddWithValue("@observacoes", execucao.Observacoes);
            cmd.Parameters.AddWithValue("@criadoEm", execucao.CriadoEm);

            cmd.ExecuteNonQuery();
        }

        // 🔹 Excluir execução
        public void Excluir(int id)
        {
            using var conn = _db.GetConnection();

            var sql = "DELETE FROM ExecucaoTarefa WHERE Id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
