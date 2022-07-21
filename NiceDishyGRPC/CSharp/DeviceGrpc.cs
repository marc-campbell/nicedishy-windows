// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: spacex/api/device/device.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace SpaceX.API.Device {
  public static partial class Device
  {
    static readonly string __ServiceName = "SpaceX.API.Device.Device";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::SpaceX.API.Device.ToDevice> __Marshaller_SpaceX_API_Device_ToDevice = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::SpaceX.API.Device.ToDevice.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::SpaceX.API.Device.FromDevice> __Marshaller_SpaceX_API_Device_FromDevice = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::SpaceX.API.Device.FromDevice.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::SpaceX.API.Device.Request> __Marshaller_SpaceX_API_Device_Request = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::SpaceX.API.Device.Request.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::SpaceX.API.Device.Response> __Marshaller_SpaceX_API_Device_Response = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::SpaceX.API.Device.Response.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::SpaceX.API.Device.ToDevice, global::SpaceX.API.Device.FromDevice> __Method_Stream = new grpc::Method<global::SpaceX.API.Device.ToDevice, global::SpaceX.API.Device.FromDevice>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "Stream",
        __Marshaller_SpaceX_API_Device_ToDevice,
        __Marshaller_SpaceX_API_Device_FromDevice);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::SpaceX.API.Device.Request, global::SpaceX.API.Device.Response> __Method_Handle = new grpc::Method<global::SpaceX.API.Device.Request, global::SpaceX.API.Device.Response>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Handle",
        __Marshaller_SpaceX_API_Device_Request,
        __Marshaller_SpaceX_API_Device_Response);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::SpaceX.API.Device.DeviceReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of Device</summary>
    [grpc::BindServiceMethod(typeof(Device), "BindService")]
    public abstract partial class DeviceBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task Stream(grpc::IAsyncStreamReader<global::SpaceX.API.Device.ToDevice> requestStream, grpc::IServerStreamWriter<global::SpaceX.API.Device.FromDevice> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::SpaceX.API.Device.Response> Handle(global::SpaceX.API.Device.Request request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for Device</summary>
    public partial class DeviceClient : grpc::ClientBase<DeviceClient>
    {
      /// <summary>Creates a new client for Device</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public DeviceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for Device that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public DeviceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected DeviceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected DeviceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncDuplexStreamingCall<global::SpaceX.API.Device.ToDevice, global::SpaceX.API.Device.FromDevice> Stream(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Stream(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncDuplexStreamingCall<global::SpaceX.API.Device.ToDevice, global::SpaceX.API.Device.FromDevice> Stream(grpc::CallOptions options)
      {
        return CallInvoker.AsyncDuplexStreamingCall(__Method_Stream, null, options);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::SpaceX.API.Device.Response Handle(global::SpaceX.API.Device.Request request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Handle(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::SpaceX.API.Device.Response Handle(global::SpaceX.API.Device.Request request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Handle, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::SpaceX.API.Device.Response> HandleAsync(global::SpaceX.API.Device.Request request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return HandleAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::SpaceX.API.Device.Response> HandleAsync(global::SpaceX.API.Device.Request request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Handle, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override DeviceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new DeviceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(DeviceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Stream, serviceImpl.Stream)
          .AddMethod(__Method_Handle, serviceImpl.Handle).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, DeviceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Stream, serviceImpl == null ? null : new grpc::DuplexStreamingServerMethod<global::SpaceX.API.Device.ToDevice, global::SpaceX.API.Device.FromDevice>(serviceImpl.Stream));
      serviceBinder.AddMethod(__Method_Handle, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::SpaceX.API.Device.Request, global::SpaceX.API.Device.Response>(serviceImpl.Handle));
    }

  }
}
#endregion
