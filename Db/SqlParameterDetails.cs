using System;
using System.Data;
using System.Collections;

public struct SqlParameterDetails
{
   // public static Hashtable AddParameterDetails = new Hashtable();

    public object Value;  
    public SqlDbType DataType;
    public SqlParameterDetails(object _a, SqlDbType _b)
    {
        Value = _a;
        DataType = _b;
    }

}

