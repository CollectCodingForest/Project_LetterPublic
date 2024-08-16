using System;

[Flags]
public enum BindingDocumentsFlags
{
    Default = 0,
    IdentityCard = 1,
    BasicInvoice = 2,
    Parcel = 4,
    CustomsClearance = 8,
    ParcelApplication = 16,
    ParcelPermit = 32,
    //FlattenHierarchy = 64,
    //InvokeMethod = 256,
    //CreateInstance = 512,
    //GetField = 1024,
    //SetField = 2048,
    //GetProperty = 4096,
    //SetProperty = 8192,
    //PutDispProperty = 16384,
    //PutRefDispProperty = 32768,
    //ExactBinding = 65536,
    //SuppressChangeType = 131072,
    //OptionalParamBinding = 262144,
    //IgnoreReturn = 16777216,
    //DoNotWrapExceptions = 33554432
}