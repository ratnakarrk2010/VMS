

using System;


namespace Project.Db
{
    /// <summary> 
    /// Represents the <c>CommonFunctions</c> table. 
    /// </summary> 
    public class CommonCollection : CommonCollection_Base
    {
        /// <summary> 
        /// Initializes a new instance of the <see cref="CommonFunctionsCollection"/> class. 
        /// </summary> 
        /// <param name="db">The database object.</param> 
        internal CommonCollection(DbConnection db)
            : base(db)
        {
            // EMPTY 
        }
    }
    // End of CommonFunctionsCollection class 
    //End Namespace 
}