using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using AmbiClean.Database;
using AmbiClean.Models;

namespace AmbiClean.Repositories
{
    public class ItemChecklistRepository
    {
        private readonly Database.Database _db;

        public ItemChecklistRepository()
        {
            _db = Database.Database.Instance;
        }

        // 🔹 Inserir novo item de checklist
        public bool Inserir(ItemChecklist item)
        {
            try
            {
                using var connection = _db.GetConnection();
                string query = @"INSERT INTO ItemChecklist 
                                 (ChecklistId, Descricao, Posicao)
                                 VALUES (@checklistId, @descricao, @posicao)";

                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@checklistId", item.ChecklistId);
                cmd.Parameters.AddWithValue("@descricao", item.Descricao);
                cmd.Parameters.AddWithValue("@posicao", item.Posicao);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir item de checklist: {ex.Message}");
                return false;
            }
        }

        // 🔹 Buscar todos os itens
        public List<ItemChecklist> BuscarTodos()
        {
            var itens = new List<ItemChecklist>();

            using var conn = _db.GetConnection();
            var sql = "SELECT Id, ChecklistId, Descricao, Posicao FROM ItemChecklist";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                itens.Add(new ItemChecklist(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetString(2),
                    reader.GetInt32(3)
                ));
            }

            return itens;
        }

        // 🔹 Buscar item por ID
        public ItemChecklist BuscarPorId(int id)
        {
            using var conn = _db.GetConnection();
            var sql = "SELECT Id, ChecklistId, Descricao, Posicao FROM ItemChecklist WHERE Id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new ItemChecklist(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetString(2),
                    reader.GetInt32(3)
                );
            }

            return null;
        }

        // 🔹 Buscar itens por ID do Checklist
        public List<ItemChecklist> BuscarPorChecklistId(int checklistId)
        {
            var itens = new List<ItemChecklist>();

            using var conn = _db.GetConnection();
            var sql = "SELECT Id, ChecklistId, Descricao, Posicao FROM ItemChecklist WHERE ChecklistId = @checklistId ORDER BY Posicao";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@checklistId", checklistId);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                itens.Add(new ItemChecklist(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetString(2),
                    reader.GetInt32(3)
                ));
            }

            return itens;
        }

        // 🔹 Atualizar item existente
        public void Atualizar(ItemChecklist item)
        {
            using var conn = _db.GetConnection();

            var sql = @"UPDATE ItemChecklist SET 
                        ChecklistId = @checklistId,
                        Descricao = @descricao,
                        Posicao = @posicao
                        WHERE Id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", item.Id);
            cmd.Parameters.AddWithValue("@checklistId", item.ChecklistId);
            cmd.Parameters.AddWithValue("@descricao", item.Descricao);
            cmd.Parameters.AddWithValue("@posicao", item.Posicao);

            cmd.ExecuteNonQuery();
        }

        // 🔹 Excluir item
        public void Excluir(int id)
        {
            using var conn = _db.GetConnection();

            var sql = "DELETE FROM ItemChecklist WHERE Id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
