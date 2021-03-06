﻿using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Abstract
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Author FindAuthorByName(string name);
    }
}
