using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if(stockModel == null)
            {
                return null;
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;
        }

        public async Task<List<Stock>> GetAllStocksAsync(QueryObject query)
        {
            var stockModel = _context.Stocks.Include(c => c.Comments).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stockModel = stockModel.Where(c => c.CompanyName.Contains(query.CompanyName));
            }

            if(!string.IsNullOrWhiteSpace(query.Symbol)){
                stockModel = stockModel.Where(c => c.Symbol.Contains(query.Symbol));
            }

            if(!string.IsNullOrEmpty(query.SortBy)){

                if(query.SortBy!.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stockModel = query.IsDesending ? stockModel.OrderByDescending(c => c.Symbol) : stockModel.OrderBy(c => c.Symbol);
                }
            }

            return await stockModel.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stockModel = await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);

            if(stockModel == null)
            {
                return null;
            }

            return stockModel;

        }

        public Task<bool> IsStockExsist(int id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if(existingModel == null)
            {
                return null;
            }

            existingModel.Symbol = stockDto.Symbol;
            existingModel.CompanyName = stockDto.CompanyName;
            existingModel.Purchase  = stockDto.Purchase;
            existingModel.LastDiv = stockDto.LastDiv;
            existingModel.Industry = stockDto.Industry;
            existingModel.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();

            return existingModel;
        }
    }
}