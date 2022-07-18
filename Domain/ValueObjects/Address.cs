using Domain.DDDBlocks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public class Address : ValueObject
    {
      
        public string City { get; }
        public string ZipCode { get; }
        public string Street { get; }
        public int HouseNumber { get; }
        public int? ApartmentNumber { get; }

        private Address()
        {
            //For EF
        }
        public Address(string city, string zipCode, string street, int houseNumber, int? apartmentNumber)
        {
            City = city;
            ZipCode = zipCode;
            Street = street;
            HouseNumber = houseNumber;
            ApartmentNumber = apartmentNumber;
        }

        public override IEnumerable<object> GetPropertiesToCompare()
        {
            yield return City;
            yield return ZipCode;
            yield return Street;
            yield return HouseNumber;
            yield return ApartmentNumber;
        }
    }
}
