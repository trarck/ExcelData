using System;

namespace ExcelData.DataSerializer
{
    public class SerializerSettings
    {
        private readonly INameProvider nameProvider;
        private readonly IPrimitiveTypeProvider primitiveProvider;
        private readonly ICollectionTypeProvider collectionProvider;
        private readonly ICompositeTypeProvider compositeProvider;

        public SerializerSettings(
            INameProvider nameProvider,
            IPrimitiveTypeProvider primitiveProvider,
            ICollectionTypeProvider collectionProvider,
            ICompositeTypeProvider compositeProvider)
        {
            if (nameProvider == null)
                throw new ArgumentNullException("nameProvider");
            if (primitiveProvider == null)
                throw new ArgumentNullException("primitiveProvider");
            if (collectionProvider == null)
                throw new ArgumentNullException("collectionProvider");
            if (compositeProvider == null)
                throw new ArgumentNullException("compositeProvider");

            this.nameProvider = nameProvider;
            this.primitiveProvider = primitiveProvider;
            this.collectionProvider = collectionProvider;
            this.compositeProvider = compositeProvider;
        }

        /// <summary>
        /// Gets <see cref="INameProvider"/>.
        /// </summary>
        public INameProvider NameProvider
        {
            get { return nameProvider; }
        }

        /// <summary>
        /// Gets <see cref="IPrimitiveTypeProvider"/>.
        /// </summary>
        public IPrimitiveTypeProvider PrimitiveProvider
        {
            get { return primitiveProvider; }
        }

        /// <summary>
        /// Gets <see cref="ICollectionTypeProvider"/>.
        /// </summary>
        public ICollectionTypeProvider CollectionProvider
        {
            get { return collectionProvider; }
        }

        /// <summary>
        /// Gets <see cref="ICompositeTypeProvider"/>.
        /// </summary>
        public ICompositeTypeProvider CompositeProvider
        {
            get { return compositeProvider; }
        }
    }
}
