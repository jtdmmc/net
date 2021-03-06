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
  public partial class TRole : TBase
  {
    private int _RoleId;
    private string _RoleName;
    private List<TPower> _Powers;

    [DataMember(Order = 0)]
    public int RoleId
    {
      get
      {
        return _RoleId;
      }
      set
      {
        __isset.RoleId = true;
        this._RoleId = value;
      }
    }

    [DataMember(Order = 0)]
    public string RoleName
    {
      get
      {
        return _RoleName;
      }
      set
      {
        __isset.RoleName = true;
        this._RoleName = value;
      }
    }

    [DataMember(Order = 0)]
    public List<TPower> Powers
    {
      get
      {
        return _Powers;
      }
      set
      {
        __isset.Powers = true;
        this._Powers = value;
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
      public bool RoleId;
      [DataMember]
      public bool RoleName;
      [DataMember]
      public bool Powers;
    }

    #region XmlSerializer support

    public bool ShouldSerializeRoleId()
    {
      return __isset.RoleId;
    }

    public bool ShouldSerializeRoleName()
    {
      return __isset.RoleName;
    }

    public bool ShouldSerializePowers()
    {
      return __isset.Powers;
    }

    #endregion XmlSerializer support

    public TRole() {
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
                RoleId = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.String) {
                RoleName = iprot.ReadString();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 3:
              if (field.Type == TType.List) {
                {
                  Powers = new List<TPower>();
                  TList _list0 = iprot.ReadListBegin();
                  for( int _i1 = 0; _i1 < _list0.Count; ++_i1)
                  {
                    TPower _elem2;
                    _elem2 = new TPower();
                    _elem2.Read(iprot);
                    Powers.Add(_elem2);
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
        TStruct struc = new TStruct("TRole");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (__isset.RoleId) {
          field.Name = "RoleId";
          field.Type = TType.I32;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(RoleId);
          oprot.WriteFieldEnd();
        }
        if (RoleName != null && __isset.RoleName) {
          field.Name = "RoleName";
          field.Type = TType.String;
          field.ID = 2;
          oprot.WriteFieldBegin(field);
          oprot.WriteString(RoleName);
          oprot.WriteFieldEnd();
        }
        if (Powers != null && __isset.Powers) {
          field.Name = "Powers";
          field.Type = TType.List;
          field.ID = 3;
          oprot.WriteFieldBegin(field);
          {
            oprot.WriteListBegin(new TList(TType.Struct, Powers.Count));
            foreach (TPower _iter3 in Powers)
            {
              _iter3.Write(oprot);
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
      var other = that as TRole;
      if (other == null) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.RoleId == other.__isset.RoleId) && ((!__isset.RoleId) || (System.Object.Equals(RoleId, other.RoleId))))
        && ((__isset.RoleName == other.__isset.RoleName) && ((!__isset.RoleName) || (System.Object.Equals(RoleName, other.RoleName))))
        && ((__isset.Powers == other.__isset.Powers) && ((!__isset.Powers) || (TCollections.Equals(Powers, other.Powers))));
    }

    public override int GetHashCode() {
      int hashcode = 0;
      unchecked {
        hashcode = (hashcode * 397) ^ (!__isset.RoleId ? 0 : (RoleId.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.RoleName ? 0 : (RoleName.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.Powers ? 0 : (TCollections.GetHashCode(Powers)));
      }
      return hashcode;
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("TRole(");
      bool __first = true;
      if (__isset.RoleId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("RoleId: ");
        __sb.Append(RoleId);
      }
      if (RoleName != null && __isset.RoleName) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("RoleName: ");
        __sb.Append(RoleName);
      }
      if (Powers != null && __isset.Powers) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Powers: ");
        __sb.Append(Powers);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
