namespace ToDoMVC.UI
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using Incoding.Data;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class Todo : IncEntityBase
    {
        #region Properties

        public virtual string Title { get; set; }

        public virtual bool Active { get; set; }

        public virtual string ClientId { get; set; }

        public virtual DateTime CreateDt { get; set; }

        #endregion

        #region Nested classes

        [UsedImplicitly, Obsolete(ObsoleteMessage.ClassNotForDirectUsage, true), ExcludeFromCodeCoverage]
        public class Map : NHibernateEntityMap<Todo>
        {
            ////ncrunch: no coverage start
            #region Constructors

            protected Map()
            {
                IdGenerateByGuid(r => r.Id);
                MapEscaping(r => r.Title);
                MapEscaping(r => r.Active);
                MapEscaping(r => r.ClientId);
                MapEscaping(r => r.CreateDt);
            }

            #endregion

            ////ncrunch: no coverage end        
        }

        #endregion
    }
}