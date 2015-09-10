
# region Document Header
//Created By       : Anji 
//Created Date     : 08 May 2014
//Description      : to read values from data reader
//------------------------------------------------------------------------------------------------------------------------------------------------
//Modified By       Modified Date       Description(Reference of Bug ID if any)         Reviewed By     Reviewed Date 
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
# endregion

namespace CSS2.Models
{

    #region Usings
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data;
    using System.Data.SqlClient;
    #endregion

    /// <summary>
    /// This is a DataReader that 'fixes' any null values before
    /// they are returned to our business code.
    /// </summary>
    public sealed class SafeDataReader : IDataReader
    {
        private readonly IDataReader _dataReader;

        /// <summary>
        /// Initializes the SafeDataReader object to use data from
        /// the provided DataReader object.
        /// </summary>
        /// <param name="dataReader">The source DataReader object containing the data.</param>
        public SafeDataReader(IDataReader dataReader)
        {
            _dataReader = dataReader;
        }

        /// <summary>
        /// Gets a string value from the datareader.    
        /// Returns "" for null.
        /// </summary>
        public string GetString(int i)
        {
            return _dataReader.IsDBNull(i) ? string.Empty : _dataReader.GetString(i);
        }

        /// <summary>
        /// Gets a string value from the datareader.    
        /// Returns "" for null.
        /// </summary>
        public string GetString(string name)
        {
            return Convert.IsDBNull(_dataReader[name]) ? string.Empty : Convert.ToString(_dataReader[name]);
        }

        /// <summary>
        /// Gets a value of type <see cref="System.Object" /> from the datareader.    
        /// Returns Nothing for null.
        /// </summary>
        public object GetValue(int i)
        {
            return _dataReader.IsDBNull(i) ? null : _dataReader.GetValue(i);
        }

        /// <summary>
        /// Gets a value of type <see cref="System.Object" /> from the datareader.    
        /// Returns Nothing for null.
        /// </summary>
        public object GetValue(string name)
        {
            return Convert.IsDBNull(_dataReader[name]) ? null : _dataReader[name];
        }

        /// <summary>
        /// Gets an integer from the datareader.    
        /// Returns 0 for null.
        /// </summary>
        public int GetInt32(int i)
        {
            return _dataReader.IsDBNull(i) ? 0 : _dataReader.GetInt32(i);
        }

        /// <summary>
        /// Gets an integer from the datareader.    
        /// Returns 0 for null.
        /// </summary>
        public int GetInt32(string name)
        {
            return Convert.IsDBNull(_dataReader[name]) ? 0 : Convert.ToInt32(_dataReader[name]);
        }

        /// <summary>
        /// Gets a double from the datareader.   
        /// Returns 0 for null.
        /// </summary>
        public double GetDouble(int i)
        {
            return _dataReader.IsDBNull(i) ? 0 : _dataReader.GetDouble(i);
        }

        /// <summary>
        /// Gets a double from the datareader.	  
        /// Returns 0 for null.
        /// </summary>
        public double GetDouble(string name)
        {
            return Convert.IsDBNull(_dataReader[name]) ? 0 : (Double)_dataReader[name];
        }

        /// <summary>
        /// Gets a DateTime from the datareader.   
        /// A null value is converted into the min possible date.
        /// </summary>    
        public DateTime GetDateTime(int i)
        {
            return _dataReader.IsDBNull(i) ? DateTime.MinValue : Convert.ToDateTime(_dataReader.GetString(i));
        }

        /// <summary>
        /// Gets a DateTime from the datareader.   
        /// A null value is converted into the min possible date.
        /// </summary>    
        public DateTime GetDateTime(string name)
        {
            return Convert.IsDBNull(_dataReader[name]) ? DateTime.MinValue : (DateTime)_dataReader[name];
        }

        /// <summary>
        /// Gets a Guid value from the datareader.
        /// </summary>
        public Guid GetGuid(int i)
        {
            return _dataReader.IsDBNull(i) ? Guid.Empty : _dataReader.GetGuid(i);
        }

        /// <summary>
        /// Gets a Guid value from the datareader.
        /// </summary>
        public Guid GetGuid(string name)
        {
            return Convert.IsDBNull(_dataReader[name]) ? Guid.Empty : (Guid)_dataReader[name];
        }

        /// <summary>
        /// Gets a boolean value from the datareader.
        /// </summary>
        public bool GetBoolean(int i)
        {
            return !_dataReader.IsDBNull(i) && _dataReader.GetBoolean(i);
        }

        /// <summary>
        /// Gets a boolean value from the datareader.
        /// </summary>
        public bool GetBoolean(string name)
        {
            return !Convert.IsDBNull(_dataReader[name]) && (Boolean)(_dataReader[name]);
        }

        /// <summary>
        /// Gets a byte value from the datareader.
        /// </summary>
        public byte GetByte(int i)
        {
            return _dataReader.IsDBNull(i) ? (byte)0 : _dataReader.GetByte(i);
        }

        /// <summary>
        /// Gets a byte value from the datareader.
        /// </summary>
        public byte GetByte(string name)
        {
            return Convert.IsDBNull(_dataReader[name]) ? (byte)0 : (Byte)_dataReader[name];
        }

        /// <summary>
        /// Invokes the GetBytes method of the underlying datareader.
        /// </summary>
        public Int64 GetBytes(int i, Int64 fieldOffset,
                              byte[] buffer, int bufferoffset, int length)
        {
            return _dataReader.IsDBNull(i) ? 0 : _dataReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        }

        /// <summary>
        /// Gets a char value from the datareader.
        /// </summary>
        public char GetChar(int i)
        {
            return _dataReader.IsDBNull(i) ? char.MinValue : _dataReader.GetChar(i);
        }

        /// <summary>
        /// Gets a char value from the datareader.
        /// </summary>
        public char GetChar(string name)
        {
            return Convert.IsDBNull(_dataReader[name]) ? char.MinValue : (char)_dataReader[name];
        }

        /// <summary>
        /// Invokes the GetChars method of the underlying datareader.
        /// </summary>
        public Int64 GetChars(int i, Int64 fieldoffset,
                              char[] buffer, int bufferoffset, int length)
        {
            return _dataReader.IsDBNull(i) ? 0 : _dataReader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
        }

        /// <summary>
        /// Invokes the GetData method of the underlying datareader.
        /// </summary>
        public IDataReader GetData(int i)
        {
            return _dataReader.GetData(i);
        }

        /// <summary>
        /// Invokes the GetDataTypeName method of the underlying datareader.
        /// </summary>
        public string GetDataTypeName(int i)
        {
            return _dataReader.GetDataTypeName(i);
        }


        /// <summary>
        /// Gets a decimal value from the datareader.
        /// </summary>
        public decimal GetDecimal(int i)
        {
            return _dataReader.IsDBNull(i) ? 0 : _dataReader.GetDecimal(i);
        }

        /// <summary>
        /// Gets a decimal value from the datareader.
        /// </summary>
        public decimal GetDecimal(string name)
        {
            return Convert.IsDBNull(_dataReader[name]) ? 0 : Convert.ToDecimal(_dataReader[name]);
        }

        /// <summary>
        /// Invokes the GetFieldType method of the underlying datareader.
        /// </summary>
        public Type GetFieldType(int i)
        {
            return _dataReader.GetFieldType(i);
        }

        /// <summary>
        /// Gets a Single value from the datareader.
        /// </summary>
        public float GetFloat(int i)
        {
            return _dataReader.IsDBNull(i) ? 0 : _dataReader.GetFloat(i);
        }

        /// <summary>
        /// Gets a Single value from the datareader.
        /// </summary>
        public float GetFloat(string name)
        {
            return Convert.IsDBNull(_dataReader[name]) ? 0 : (float)_dataReader[name];
        }

        /// <summary>
        /// Gets a Short value from the datareader.
        /// </summary>
        public short GetInt16(int i)
        {
            return _dataReader.IsDBNull(i) ? (short)0 : _dataReader.GetInt16(i);
        }


        /// <summary>
        /// Gets a Short value from the datareader.
        /// </summary>
        public short GetInt16(string name)
        {
            return Convert.IsDBNull(_dataReader[name]) ? (short)0 : (short)_dataReader[name];
        }


        /// <summary>
        /// Gets a Long value from the datareader.
        /// </summary>
        public Int64 GetInt64(int i)
        {

            return _dataReader.IsDBNull(i) ? 0 : Convert.ToInt64(_dataReader.GetString(i));
        }

        /// <summary>
        /// Gets a Long value from the datareader.
        /// </summary>
        public Int64 GetInt64(string name)
        {
            return Convert.IsDBNull(_dataReader[name]) ? 0 : Convert.ToInt64(Convert.ToString(_dataReader[name]));
        }

        /// <summary>
        /// Invokes the GetName method of the underlying datareader.
        /// </summary>
        public string GetName(int i)
        {
            return _dataReader.GetName(i);
        }

        /// <summary>
        /// Gets an ordinal value from the datareader.
        /// </summary>
        public int GetOrdinal(string name)
        {
            return _dataReader.GetOrdinal(name);
        }

        /// <summary>
        /// Invokes the GetSchemaTable method of the underlying datareader.
        /// </summary>
        public DataTable GetSchemaTable()
        {
            return _dataReader.GetSchemaTable();
        }


        /// <summary>
        /// Invokes the GetValues method of the underlying datareader.
        /// </summary>
        public int GetValues(object[] values)
        {
            return _dataReader.GetValues(values);
        }

        /// <summary>
        /// Reads the next row of data from the datareader.
        /// </summary>
        public bool Read()
        {
            //return !_dataReader.IsDBNull(i) && _dataReader.GetBoolean(i);
            return _dataReader.Read();
        }

        /// <summary>
        /// Moves to the next result set in the datareader.
        /// </summary>
        public bool NextResult()
        {
            return _dataReader.NextResult();
        }

        /// <summary>
        /// Closes the datareader.
        /// </summary>
        public void Close()
        {
            _dataReader.Close();
        }

        /// <summary>
        /// Returns the depth property value from the datareader.
        /// </summary>
        public int Depth
        {
            get { return _dataReader.Depth; }
        }

        /// <summary>
        /// Returns the FieldCount property from the datareader.
        /// </summary>
        public int FieldCount
        {
            get { return _dataReader.FieldCount; }
        }


        /// <summary>
        /// Returns the IsClosed property value from the datareader.
        /// </summary>
        public bool IsClosed
        {
            get { return _dataReader.IsClosed; }
        }

        /// <summary>
        /// Invokes the IsDBNull method of the underlying datareader.
        /// </summary>
        public bool IsDBNull(int i)
        {
            return _dataReader.IsDBNull(i);
        }

        /// <summary>
        /// Returns a value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns Nothing if the value is null.
        /// </remarks>
        public object this[string name]
        {
            get
            {
                var val = _dataReader[name];
                return DBNull.Value.Equals(val) ? null : val;
            }
        }

        /// <summary>
        /// Returns a value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns Nothing if the value is null.
        /// </remarks>
        public object this[int i]
        {
            get
            {
                return _dataReader.IsDBNull(i) ? null : _dataReader[i];
            }
        }

        /// <summary>
        /// Returns the RecordsAffected property value from the underlying datareader.
        /// </summary>
        public int RecordsAffected
        {
            get { return _dataReader.RecordsAffected; }
        }



        #region IDisposable Members

        public void Dispose()
        {
            _dataReader.Dispose();
        }

        #endregion
    }
}