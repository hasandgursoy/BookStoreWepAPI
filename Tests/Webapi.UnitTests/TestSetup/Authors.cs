using System;
using Webapi.Entities;
using WebApi.DBOperations;

namespace Webapi.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDBContext context)
        {     
        
         context.Authors.AddRange(
                    new Author {
                        Name = "Charlotte",
                        SurName = "Perkins",
                        DateOfBirth = new DateTime(1860,5,8),
                        IsBookPublished=true,
                    },
                    new Author {
                        Name="Eric",
                        SurName="Ries",
                        DateOfBirth = new DateTime(1978,9,22),
                        IsBookPublished=true,
                    },
                    new Author{
                        Name ="Frank",
                        SurName ="Herbert",
                        DateOfBirth = new DateTime(1920,10,8),
                        IsBookPublished=true,
                    }
                );
        
        }
    }
}