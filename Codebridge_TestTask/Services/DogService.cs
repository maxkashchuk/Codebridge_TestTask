using Codebridge_TestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace Codebridge_TestTask.Services
{
    public interface IDogService
    {
        public string GetPing();
        public Task<bool> AddDog(DogPOST dog_post);
        public Task<List<DogDB>> GetDogs();
        public Task<List<DogDB>> PaginationSortDogs(string attribute, string order, int pageNumber, int limit);
        public Task<DogDB> CheckName(string name);
        public bool CheckNegative(int number);
    }
    public class DogService : IDogService
    {
        private readonly ApplicationContext _context;

        public DogService(ApplicationContext _context)
        {
            this._context = _context;
        }

        public string GetPing()
        {
            return "Dogs house service. Version 1.0.1";
        }

        public async Task<bool> AddDog(DogPOST dog_post)
        {
            DogDB dog = new DogDB() 
            { 
                Color = dog_post.Color,
                Name = dog_post.Name,
                TailLength = dog_post.TailLength,
                Weight = dog_post.Weight
            };
            await _context.Dogs.AddAsync(dog);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<DogDB>> GetDogs()
        {
            return await _context.Dogs.ToListAsync();
        }

        public async Task<List<DogDB>> PaginationSortDogs(string attribute, string order, int pageNumber, int limit)
        {
            List<DogDB> dogs = await _context.Dogs.Skip((pageNumber - 1) * limit).Take(limit).ToListAsync();

            switch(attribute)
            {
                case "Name":
                    dogs = (order == "desc") ? dogs.OrderByDescending(el => el.Name).ToList() :
                        dogs.OrderBy(el => el.Name).ToList();
                    break;
                case "Color":
                    dogs = (order == "desc") ? dogs.OrderByDescending(el => el.Color).ToList() :
                        dogs.OrderBy(el => el.Color).ToList();
                    break;
                case "TailLength":
                    dogs = (order == "desc") ? dogs.OrderByDescending(el => el.TailLength).ToList() :
                        dogs.OrderBy(el => el.TailLength).ToList();
                    break;
                case "Weight":
                    dogs = (order == "desc") ? dogs.OrderByDescending(el => el.Weight).ToList() :
                        dogs.OrderBy(el => el.Weight).ToList();
                    break;
            }

            return dogs;
        }

        public async Task<DogDB> CheckName(string name)
        {
            return await _context.Dogs.Where(el => el.Name == name).FirstOrDefaultAsync();
        }

        public bool CheckNegative(int number)
        {
            return number < 0 ? true : false;
        }
    }
}
