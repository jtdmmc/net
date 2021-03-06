/**
 * Autogenerated by Thrift Compiler (0.11.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
#if !SILVERLIGHT
using System.Xml.Serialization;
#endif
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

namespace MVI.DotnetSoftwarePlatform.Api.Thrift
{

  #if !SILVERLIGHT
  [Serializable]
  #endif
  [DataContract(Namespace="")]
  public partial class TPageAccount : TBase
  {
    private int _TotalCount;
    private int _PageSize;
    private int _CurrPage;
    private List<TAccount> _Data;

    [DataMember(Order = 0)]
    public int TotalCount
    {
      get
      {
        return _TotalCount;
      }
      set
      {
        __isset.TotalCount = true;
        this._TotalCount = value;
      }
    }

    [DataMember(Order = 0)]
    public int PageSize
    {
      get
      {
        return _PageSize;
      }
      set
      {
        __isset.PageSize = true;
        this._PageSize = value;
      }
    }

    [DataMember(Order = 0)]
    public int CurrPage
    {
      get
      {
        return _CurrPage;
      }
      set
      {
        __isset.CurrPage = true;
        this._CurrPage = value;
      }
    }

    [DataMember(Order = 0)]
    public List<TAccount> Data
    {
      get
      {
        return _Data;
      }
      set
      {
        __isset.Data = true;
        this._Data = value;
      }
    }


    [XmlIgnore] // XmlSerializer
    [DataMember(Order = 1)]  // XmlObjectSerializer, DataContractJsonSerializer, etc.
    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    [DataContract]
    public struct Isset {
      [DataMember]
      public bool TotalCount;
      [DataMember]
      public bool PageSize;
      [DataMember]
      public bool CurrPage;
      [DataMember]
      public bool Data;
    }

    #region XmlSerializer support

    public bool ShouldSerializeTotalCount()
    {
      return __isset.TotalCount;
    }

    public bool ShouldSerializePageSize()
    {
      return __isset.PageSize;
    }

    public bool ShouldSerializeCurrPage()
    {
      return __isset.CurrPage;
    }

    public bool ShouldSerializeData()
    {
      return __isset.Data;
    }

    #endregion XmlSerializer support

    public TPageAccount() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.I32) {
                TotalCount = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.I32) {
                PageSize = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 3:
              if (field.Type == TType.I32) {
                CurrPage = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 4:
              if (field.Type == TType.List) {
                {
                  Data = new List<TAccount>();
                  TList _list8 = iprot.ReadListBegin();
                  for( int _i9 = 0; _i9 < _list8.Count; ++_i9)
                  {
                    TAccount _elem10;
                    _elem10 = new TAccount();
                    _elem10.Read(iprot);
                    Data.Add(_elem10);
                  }
                  iprot.ReadListEnd();
                }
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("TPageAccount");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (__isset.TotalCount) {
          field.Name = "TotalCount";
          field.Type = TType.I32;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(TotalCount);
          oprot.WriteFieldEnd();
        }
        if (__isset.PageSize) {
          field.Name = "PageSize";
          field.Type = TType.I32;
          field.ID = 2;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(PageSize);
          oprot.WriteFieldEnd();
        }
        if (__isset.CurrPage) {
          field.Name = "CurrPage";
          field.Type = TType.I32;
          field.ID = 3;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(CurrPage);
          oprot.WriteFieldEnd();
        }
        if (Data != null && __isset.Data) {
          field.Name = "Data";
          field.Type = TType.List;
          field.ID = 4;
          oprot.WriteFieldBegin(field);
          {
            oprot.WriteListBegin(new TList(TType.Struct, Data.Count));
            foreach (TAccount _iter11 in Data)
            {
              _iter11.Write(oprot);
            }
            oprot.WriteListEnd();
          }
          oprot.WriteFieldEnd();
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override bool Equals(object that) {
      var other = that as TPageAccount;
      if (other == null) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.TotalCount == other.__isset.TotalCount) && ((!__isset.TotalCount) || (System.Object.Equals(TotalCount, other.TotalCount))))
        && ((__isset.PageSize == other.__isset.PageSize) && ((!__isset.PageSize) || (System.Object.Equals(PageSize, other.PageSize))))
        && ((__isset.CurrPage == other.__isset.CurrPage) && ((!__isset.CurrPage) || (System.Object.Equals(CurrPage, other.CurrPage))))
        && ((__isset.Data == other.__isset.Data) && ((!__isset.Data) || (TCollections.Equals(Data, other.Data))));
    }

    public override int GetHashCode() {
      int hashcode = 0;
      unchecked {
        hashcode = (hashcode * 397) ^ (!__isset.TotalCount ? 0 : (TotalCount.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.PageSize ? 0 : (PageSize.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.CurrPage ? 0 : (CurrPage.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.Data ? 0 : (TCollections.GetHashCode(Data)));
      }
      return hashcode;
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("TPageAccount(");
      bool __first = true;
      if (__isset.TotalCount) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("TotalCount: ");
        __sb.Append(TotalCount);
      }
      if (__isset.PageSize) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("PageSize: ");
        __sb.Append(PageSize);
      }
      if (__isset.CurrPage) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("CurrPage: ");
        __sb.Append(CurrPage);
      }
      if (Data != null && __isset.Data) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Data: ");
        __sb.Append(Data);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
