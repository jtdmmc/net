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
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

namespace mvi.services.interfaces.account
{

  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class TCommonReply : TBase
  {
    private bool _IsSuccess;
    private string _Message;

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


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool IsSuccess;
      public bool Message;
    }

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
