using System;
using System.Linq;
using System.Collections.Generic;
using BilletAPI.Models.Repository;

namespace BilletAPI.Models.DataManager
{
    public class ZipCodeManager : IDataRepository<ZipCode>
    {
        readonly BilletSystemAPIContext _sKPDbContext;

        public ZipCodeManager(BilletSystemAPIContext sKPDbContext)
        {
            _sKPDbContext = sKPDbContext;
        }

        public IEnumerable<ZipCode> GetAll()
        {
            return _sKPDbContext.ZipCodes.ToList();
        }

        public ZipCode Get(long id)
        {
            return _sKPDbContext.ZipCodes.FirstOrDefault(e => e.PostalCode == id);
        }

        public void Add(ZipCode zipCode)
        {
            _sKPDbContext.Add(zipCode);
        }

        public void Update(ZipCode zipCodeToUpdate, ZipCode zipCode)
        {
            zipCodeToUpdate.PostalCode = zipCode.PostalCode;
            zipCodeToUpdate.City = zipCode.City;

            _sKPDbContext.SaveChanges();
        }

        public void Delete(ZipCode zipCode)
        {
            _sKPDbContext.Remove(zipCode);
            _sKPDbContext.SaveChanges();
        }
    }
}
