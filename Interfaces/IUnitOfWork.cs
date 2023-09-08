using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Michelin.Infrastructure
{
    public interface IUnitOfWork
    {
        void UploadImage(IFormFile file);
    }
}
