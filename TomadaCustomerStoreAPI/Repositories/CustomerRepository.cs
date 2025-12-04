using Dapper;
using Microsoft.Data.SqlClient;
using TomadaStore.Models.DTOs;
using TomadaStore.Models.Models;
using TomadaStoreCustomerAPI.Data;
using TomadaStoreCustomerAPI.Services;

namespace TomadaStoreCustomerAPI.Repositories
{
    public class CustomerRepository : ICustumerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly SqlConnection _connection;
        public CustomerRepository(ILogger<CustomerRepository> logger, ConnectionDB connection)
        {
            _logger = logger;
            _connection = connection.GetConnection();
        }

        public async Task<List<CustomerResponseDTO>> GetAllCustomerAsync()
        {
            try
            {
                var sqlSelect = @"SELECT Id, FirstName, LastName, Email, PhoneNumber, Situacao
                                  FROM Customers";

                var customers = await _connection.QueryAsync<CustomerResponseDTO>(sqlSelect);
                return customers.ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error getting customers: {ex.Message}");
                throw;
            }
        }

        public async Task<CustomerResponseDTO> GetCustomerByIdAsync(int id)
        {
            try
            {
                var sql = @"SELECT Id, FirstName, LastName, Email, PhoneNumber, Situacao
                        FROM Customers
                        WHERE Id = @Id";

                var customer = await _connection.QueryFirstOrDefaultAsync<CustomerResponseDTO>(sql,
                    new { Id = id });
                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting customer: {ex.Message}");
                throw;
            }
        }

        public async Task InsertCustomerAsync(CustomerRequestDTO customer)
        {
            try
            {
                var insertSql = @"INSERT INTO Customers (FirstName, LastName, Email, PhoneNumber, Situacao)
                          VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Situacao);";
                await _connection.ExecuteAsync(insertSql, new { customer.FirstName, 
                                                                customer.LastName, 
                                                                customer.Email, 
                                                                customer.PhoneNumber,
                                                                Situacao = true});
    }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error insert customer: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting customer: {ex.Message}");  
            }
        }

        public async Task UpdateSituacao(int id)
        {
            var sql = @"UPDATE Customers
                        SET Situacao = CASE Situacao WHEN 0 THEN 1
                        WHEN 1 THEN 0
                        END
                        WHERE Id = @Id;";

            await _connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
