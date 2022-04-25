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
  public partial class TCommonReply : TBase
  {
    private bool _IsSuccess;
    private string _Message;

    [DataMember(Order = 0)]
    public bool IsSuccess
    {
      get
      {
        return _IsSuccess;
      }
      set
      {
        __isset.IsSuccess = true;
        this._IsSuccess = value;
      }
    }

    [DataMember(Order = 0)]
    public string Message
    {
      get
      {
        return _Message;
      }
      set
      {
        __isset.Message = true;
        this._Message = value;
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
      public bool IsSuccess;
      [DataMember]
      public bool Message;
    }

    #region XmlSerializer support

    public bool ShouldSerializeIsSuccess()
    {
      return __isset.IsSuccess;
    }

    public bool ShouldSerializeMessage()
    {
      return __isset.Message;
    }

    #endregion XmlSerializer support

    public TCommonReply() {
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
              if (field.Type == TType.Bool) {
                IsSuccess = iprot.ReadBool();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.String) {
                Message = iprot.ReadString();
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
        TStruct struc = new TStruct("TCommonReply");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (__isset.IsSuccess) {
          field.Name = "IsSuccess";
          field.Type = TType.Bool;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          oprot.WriteBool(IsSuccess);
          oprot.WriteFieldEnd();
        }
        if (Message != null && __isset.Message) {
          field.Name = "Message";
          field.Type = TType.String;
          field.ID = 2;
          oprot.WriteFieldBegin(field);
          oprot.WriteString(Message);
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
      var other = that as TCommonReply;
      if (other == null) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.IsSuccess == other.__isset.IsSuccess) && ((!__isset.IsSuccess) || (System.Object.Equals(IsSuccess, other.IsSuccess))))
        && ((__isset.Message == other.__isset.Message) && ((!__isset.Message) || (System.Object.Equals(Message, other.Message))));
    }

    public override int GetHashCode() {
      int hashcode = 0;
      unchecked {
        hashcode = (hashcode * 397) ^ (!__isset.IsSuccess ? 0 : (IsSuccess.GetHashCode()));
        hashcode = (hashcode * 397) ^ (!__isset.Message ? 0 : (Message.GetHashCode()));
      }
      return hashcode;
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("TCommonReply(");
      bool __first = true;
      if (__isset.IsSuccess) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("IsSuccess: ");
        __sb.Append(IsSuccess);
      }
      if (Message != null && __isset.Message) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Message: ");
        __sb.Append(Message);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}