﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CasaDoCodigo.Areas.Catalogo.Models
{
    [DataContract]
    public abstract class BaseModel
    {
        [DataMember]
        public int Id { get; protected set; }
    }
}
