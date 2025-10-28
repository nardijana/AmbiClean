using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using AmbiClean.Database;
using AmbiClean.Models;

namespace AmbiClean.Repositories
{
    public class TurnoRepository
    {
        private readonly Database.Database _db;

        public TurnoRepository()
        {
            _db = Database.Database.Instance;
        }

        // 🔹 Inserir novo turno
        public bool Inserir(Turno turno)
        {
            try
            {
                using var connection = _db.GetConnection();
                string query = @"INSERT INTO Turno 
                                 (Nome, HoraInicio, HoraFim, Descricao)
                                 VALUES (@nome, @inicio, @fim, @descricao)";

                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@nome", turno.Nome);
                cmd.Parameters.AddWithValue("@inicio", turno.HoraInicio);
                cmd.Parameters.AddWithValue("@fim", turno.HoraFim);
                cmd.Parameters.AddWithValue("@descricao", turno.Descricao);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir turno: {ex.Message}");
                return false;
            }
        }

        // 🔹 Buscar todos os turnos
        public List<Turno> BuscarTodos()
        {
            var turnos = new List<Turno>();

            using var conn = _db.GetConnection();
            var sql = "SELECT Id, Nome, HoraInicio, HoraFim, Descricao FROM Turno";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                turnos.Add(new Turno(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetTimeSpan(2),
                    reader.GetTimeSpan(3),
                    reader.IsDBNull(4) ? null : reader.GetString(4)
                ));
            }

            return turnos;
        }

        // 🔹 Buscar turno por ID
        public Turno BuscarPorId(int id)
        {
            using var conn = _db.GetConnection();

            var sql = "SELECT Id, Nome, HoraInicio, HoraFim, Descricao FROM Turno WHERE Id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Turno(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetTimeSpan(2),
                    reader.GetTimeSpan(3),
                    reader.IsDBNull(4) ? null : reader.GetString(4)
                );
            }

            return null;
        }

        // 🔹 Atualizar turno
        public void Atualizar(Turno turno)
        {
            using var conn = _db.GetConnection();

            var sql = @"UPDATE Turno SET 
                        Nome = @nome,
                        HoraInicio = @inicio,
                        HoraFim = @fim,
                        Descricao = @descricao
                        WHERE Id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", turno.Id);
            cmd.Parameters.AddWithValue("@nome", turno.Nome);
            cmd.Parameters.AddWithValue("@inicio", turno.HoraInicio);
            cmd.Parameters.AddWithValue("@fim", turno.HoraFim);
            cmd.Parameters.AddWithValue("@descricao", turno.Descricao);

            cmd.ExecuteNonQuery();
        }

        // 🔹 Excluir turno
        public void Excluir(int id)
        {
            using var conn = _db.GetConnection();

            var sql = "DELETE FROM Turno WHERE Id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
