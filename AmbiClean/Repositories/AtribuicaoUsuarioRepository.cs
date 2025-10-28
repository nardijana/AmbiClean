using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using AmbiClean.Database;
using AmbiClean.Models;

namespace AmbiClean.Repositories
{
    public class AtribuicaoUsuarioAreaRepository
    {
        private readonly Database.Database _db;

        public AtribuicaoUsuarioAreaRepository()
        {
            _db = Database.Database.Instance;
        }

        // 🔹 Inserir nova atribuição
        public bool Inserir(AtribuicaoUsuarioArea atribuicao)
        {
            try
            {
                using var connection = _db.GetConnection();
                string query = @"INSERT INTO AtribuicaoUsuarioArea 
                                 (UsuarioId, AreaId, AtribuidoEm)
                                 VALUES (@usuarioId, @areaId, @atribuidoEm)";

                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@usuarioId", atribuicao.UsuarioId);
                cmd.Parameters.AddWithValue("@areaId", atribuicao.AreaId);
                cmd.Parameters.AddWithValue("@atribuidoEm", atribuicao.AtribuidoEm);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir atribuição: {ex.Message}");
                return false;
            }
        }

        // 🔹 Buscar todas as atribuições
        public List<AtribuicaoUsuarioArea> BuscarTodos()
        {
            var atribuicoes = new List<AtribuicaoUsuarioArea>();

            using var conn = _db.GetConnection();
            var sql = "SELECT Id, UsuarioId, AreaId, AtribuidoEm FROM AtribuicaoUsuarioArea";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                atribuicoes.Add(new AtribuicaoUsuarioArea(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetDateTime(3)
                ));
            }

            return atribuicoes;
        }

        // 🔹 Buscar atribuição por ID
        public AtribuicaoUsuarioArea BuscarPorId(int id)
        {
            using var conn = _db.GetConnection();
            var sql = "SELECT Id, UsuarioId, AreaId, AtribuidoEm FROM AtribuicaoUsuarioArea WHERE Id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new AtribuicaoUsuarioArea(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetDateTime(3)
                );
            }

            return null;
        }

        // 🔹 Atualizar atribuição existente
        public void Atualizar(AtribuicaoUsuarioArea atribuicao)
        {
            using var conn = _db.GetConnection();

            var sql = @"UPDATE AtribuicaoUsuarioArea SET 
                        UsuarioId = @usuarioId,
                        AreaId = @areaId,
                        AtribuidoEm = @atribuidoEm
                        WHERE Id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", atribuicao.Id);
            cmd.Parameters.AddWithValue("@usuarioId", atribuicao.UsuarioId);
            cmd.Parameters.AddWithValue("@areaId", atribuicao.AreaId);
            cmd.Parameters.AddWithValue("@atribuidoEm", atribuicao.AtribuidoEm);

            cmd.ExecuteNonQuery();
        }

        // 🔹 Excluir atribuição
        public void Excluir(int id)
        {
            using var conn = _db.GetConnection();

            var sql = "DELETE FROM AtribuicaoUsuarioArea WHERE Id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
