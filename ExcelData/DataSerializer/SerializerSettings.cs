using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    public class SerializerSettings
    {
        private readonly INameProvider nameProvider;
        private readonly ITypeProvider typeProvider;       

        public SerializerSettings(
            INameProvider nameProvider,
            ITypeProvider typeProvider)
        {
            if (nameProvider == null)
                throw new ArgumentNullException("nameProvider");
            if (typeProvider == null)
                throw new ArgumentNullException("typeProvider");

            this.nameProvider = nameProvider;
            this.typeProvider = typeProvider;
        }

        /// <summary>
        /// Gets <see cref="INameProvider"/>.
        /// </summary>
        public INameProvider NameProvider
        {
            get { return nameProvider; }
        }

        /// <summary>
        /// Gets <see cref="ITypeProvider"/>.
        /// </summary>
        public ITypeProvider TypeProvider
        {
            get { return typeProvider; }
        }
    }
}
