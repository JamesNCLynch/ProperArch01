using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ProperArch01.Persistence.Interfaces;
using System.Data.Entity.Validation;

namespace ProperArch01.Persistence.Commands
{
    public class BaseWriter
    {
        protected readonly IMapper _mapper;
        protected readonly IProperArch01DbContext _db;

        public BaseWriter(IProperArch01DbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public virtual void SaveChanges()
        {
            try
            {
                _db.SaveChanges();
            }
            catch(DbEntityValidationException dbex)
            {
                // write logging here
            }
            catch(Exception e)
            {
                // write logging here
            }
        }
    }
}