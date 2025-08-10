// Copyright 2019-2021 Robotec.ai
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Runtime.InteropServices;

namespace ROS2
{
  /// <summary>
  /// An internal class to manage all (unmodified) native calls to rcl and rcutils
  /// </summary>
  internal static class NativeRcl
  {
    private static readonly int MainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
    private static void EnsureMainThread()
    {
        if (System.Threading.Thread.CurrentThread.ManagedThreadId != MainThreadId)
        {
            throw new InvalidOperationException("NativeRcl functions must be called from the main thread.");
        }
    }

    // Initialize loader and native handles up-front so delegate bindings below can safely use them
    private static DllLoadUtils dllLoadUtils;
    private static IntPtr nativeRCL;
    private static IntPtr nativeRCUtils;
    private static bool initialized = false;

    public static void Init()
    {
        EnsureMainThread();
        if (initialized) return;
        dllLoadUtils = dllLoadUtils ?? DllLoadUtilsFactory.GetDllLoadUtils();
        if (nativeRCL == IntPtr.Zero) nativeRCL = dllLoadUtils.LoadLibraryNoSuffix("rcl");
        if (nativeRCUtils == IntPtr.Zero) nativeRCUtils = dllLoadUtils.LoadLibraryNoSuffix("rcutils");
        initialized = true;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate rcl_context_t GetZeroInitializedContextType();
    internal static GetZeroInitializedContextType rcl_get_zero_initialized_context =
        () => { EnsureMainThread(); return ((GetZeroInitializedContextType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_get_zero_initialized_context"),
        typeof(GetZeroInitializedContextType)))(); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate rcl_init_options_t GetZeroInitializedInitOptionsType();
    internal static GetZeroInitializedInitOptionsType rcl_get_zero_initialized_init_options =
        () => { EnsureMainThread(); return ((GetZeroInitializedInitOptionsType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_get_zero_initialized_init_options"),
        typeof(GetZeroInitializedInitOptionsType)))(); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int InitOptionsInitType(ref rcl_init_options_t init_options, rcl_allocator_t allocator);
    internal static InitOptionsInitType rcl_init_options_init =
        (ref rcl_init_options_t init_options, rcl_allocator_t allocator) => { EnsureMainThread(); return ((InitOptionsInitType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_init_options_init"),
        typeof(InitOptionsInitType)))(ref init_options, allocator); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int ShutdownType(ref rcl_context_t context);
    internal static ShutdownType rcl_shutdown =
        (ref rcl_context_t context) => { EnsureMainThread(); return ((ShutdownType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_shutdown"),
        typeof(ShutdownType)))(ref context); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate bool ContextIsValidType(ref rcl_context_t context);
    internal static ContextIsValidType rcl_context_is_valid =
        (ref rcl_context_t context) => { EnsureMainThread(); return ((ContextIsValidType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_context_is_valid"),
        typeof(ContextIsValidType)))(ref context); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int InitType(int argc, [In, Out] string[] argv, ref rcl_init_options_t option, ref rcl_context_t context);
    internal static InitType rcl_init =
        (int argc, string[] argv, ref rcl_init_options_t option, ref rcl_context_t context) => { EnsureMainThread(); return ((InitType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_init"),
        typeof(InitType)))(argc, argv, ref option, ref context); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int ContextFiniType(ref rcl_context_t context);
    internal static ContextFiniType rcl_context_fini =
        (ref rcl_context_t context) => { EnsureMainThread(); return ((ContextFiniType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_context_fini"),
        typeof(ContextFiniType)))(ref context); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate rcl_node_t GetZeroInitializedNodeType();
    internal static GetZeroInitializedNodeType rcl_get_zero_initialized_node =
        () => { EnsureMainThread(); return ((GetZeroInitializedNodeType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_get_zero_initialized_node"),
        typeof(GetZeroInitializedNodeType)))(); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int NodeInitType(ref rcl_node_t node, string name, string node_namespace, ref rcl_context_t context, IntPtr default_options);
    internal static NodeInitType rcl_node_init =
        (ref rcl_node_t node, string name, string node_namespace, ref rcl_context_t context, IntPtr default_options) => { EnsureMainThread(); return ((NodeInitType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_node_init"),
        typeof(NodeInitType)))(ref node, name, node_namespace, ref context, default_options); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int NodeFiniType(ref rcl_node_t node);
    internal static NodeFiniType rcl_node_fini =
        (ref rcl_node_t node) => { EnsureMainThread(); return ((NodeFiniType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_node_fini"),
        typeof(NodeFiniType)))(ref node); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr NodeGetNameType(ref rcl_node_t node);
    internal static NodeGetNameType rcl_node_get_name =
        (ref rcl_node_t node) => { EnsureMainThread(); return ((NodeGetNameType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_node_get_name"),
        typeof(NodeGetNameType)))(ref node); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr NodeGetNamespaceType(ref rcl_node_t node);
    internal static NodeGetNamespaceType rcl_node_get_namespace =
        (ref rcl_node_t node) => { EnsureMainThread(); return ((NodeGetNamespaceType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_node_get_namespace"),
        typeof(NodeGetNamespaceType)))(ref node); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr ClientGetDefaultOptionsType();
    internal static ClientGetDefaultOptionsType rcl_client_get_default_options =
        () => { EnsureMainThread(); return ((ClientGetDefaultOptionsType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_client_get_default_options"),
        typeof(ClientGetDefaultOptionsType)))(); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate rcl_client_t GetZeroInitiazizedClientType();
    internal static GetZeroInitiazizedClientType rcl_get_zero_initialized_client =
        () => { EnsureMainThread(); return ((GetZeroInitiazizedClientType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_get_zero_initialized_client"),
        typeof(GetZeroInitiazizedClientType)))(); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int ClientInitType(ref rcl_client_t client, ref rcl_node_t node, IntPtr type_support_ptr, string topic_name, IntPtr client_options);
    internal static ClientInitType rcl_client_init =
        (ref rcl_client_t client, ref rcl_node_t node, IntPtr type_support_ptr, string topic_name, IntPtr client_options) => { EnsureMainThread(); return ((ClientInitType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_client_init"),
        typeof(ClientInitType)))(ref client, ref node, type_support_ptr, topic_name, client_options); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int ClientFiniType(ref rcl_client_t client, ref rcl_node_t node);
    internal static ClientFiniType rcl_client_fini =
        (ref rcl_client_t client, ref rcl_node_t node) => { EnsureMainThread(); return ((ClientFiniType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_client_fini"),
        typeof(ClientFiniType)))(ref client, ref node); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int SendRequestType(ref rcl_client_t client, IntPtr message, ref long sequence_number);
    internal static SendRequestType rcl_send_request =
        (ref rcl_client_t client, IntPtr message, ref long sequence_number) => { EnsureMainThread(); return ((SendRequestType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_send_request"),
        typeof(SendRequestType)))(ref client, message, ref sequence_number); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int TakeResponceType(ref rcl_client_t client, ref rcl_rmw_request_id_t request_header, IntPtr ros_response);
    internal static TakeResponceType rcl_take_response =
        (ref rcl_client_t client, ref rcl_rmw_request_id_t request_header, IntPtr ros_response) => { EnsureMainThread(); return ((TakeResponceType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_take_response"),
        typeof(TakeResponceType)))(ref client, ref request_header, ros_response); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int ServiceIsAvailableType(ref rcl_node_t node, ref rcl_client_t client, ref bool is_available);
    internal static ServiceIsAvailableType rcl_service_server_is_available =
        (ref rcl_node_t node, ref rcl_client_t client, ref bool is_available) => { EnsureMainThread(); return ((ServiceIsAvailableType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_service_server_is_available"),
        typeof(ServiceIsAvailableType)))(ref node, ref client, ref is_available); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr ServiceGetDefaultOptionsType();
    internal static ServiceGetDefaultOptionsType rcl_service_get_default_options =
        () => { EnsureMainThread(); return ((ServiceGetDefaultOptionsType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_service_get_default_options"),
        typeof(ServiceGetDefaultOptionsType)))(); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate rcl_service_t GetZeroInitiazizedServiceType();
    internal static GetZeroInitiazizedServiceType rcl_get_zero_initialized_service =
        () => { EnsureMainThread(); return ((GetZeroInitiazizedServiceType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_get_zero_initialized_service"),
        typeof(GetZeroInitiazizedServiceType)))(); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int ServiceInitType(ref rcl_service_t service, ref rcl_node_t node, IntPtr type_support_ptr, string topic_name, IntPtr service_options);
    internal static ServiceInitType rcl_service_init =
        (ref rcl_service_t service, ref rcl_node_t node, IntPtr type_support_ptr, string topic_name, IntPtr service_options) => { EnsureMainThread(); return ((ServiceInitType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_service_init"),
        typeof(ServiceInitType)))(ref service, ref node, type_support_ptr, topic_name, service_options); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int ServiceFiniType(ref rcl_service_t client, ref rcl_node_t node);
    internal static ServiceFiniType rcl_service_fini =
        (ref rcl_service_t client, ref rcl_node_t node) => { EnsureMainThread(); return ((ServiceFiniType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_service_fini"),
        typeof(ServiceFiniType)))(ref client, ref node); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int TakeRequestType(ref rcl_service_t service, ref rcl_rmw_request_id_t request_header, IntPtr message_handle);
    internal static TakeRequestType rcl_take_request =
        (ref rcl_service_t service, ref rcl_rmw_request_id_t request_header, IntPtr message_handle) => { EnsureMainThread(); return ((TakeRequestType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_take_request"),
        typeof(TakeRequestType)))(ref service, ref request_header, message_handle); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int SendResponceType( ref rcl_service_t service, ref rcl_rmw_request_id_t request_header, IntPtr responce_info);
    ///internal delegate int SendResponceType( ref rcl_service_t service, ref rcl_rmw_request_id_t request_header, ref IntPtr responce_info);
    internal static SendResponceType rcl_send_response =
        (ref rcl_service_t service, ref rcl_rmw_request_id_t request_header, IntPtr responce_info) => { EnsureMainThread(); return ((SendResponceType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_send_response"),
        typeof(SendResponceType)))(ref service, ref request_header, responce_info); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr PublisherGetDefaultOptionsType();
    internal static PublisherGetDefaultOptionsType rcl_publisher_get_default_options =
        () => { EnsureMainThread(); return ((PublisherGetDefaultOptionsType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_publisher_get_default_options"),
        typeof(PublisherGetDefaultOptionsType)))(); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate rcl_publisher_t GetZeroInitiazizedPublisherType();
    internal static GetZeroInitiazizedPublisherType rcl_get_zero_initialized_publisher =
        () => { EnsureMainThread(); return ((GetZeroInitiazizedPublisherType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_get_zero_initialized_publisher"),
        typeof(GetZeroInitiazizedPublisherType)))(); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int PublisherInitType(ref rcl_publisher_t publisher, ref rcl_node_t node, IntPtr type_support_ptr, string topic_name, IntPtr publisher_options);
    internal static PublisherInitType rcl_publisher_init =
        (ref rcl_publisher_t publisher, ref rcl_node_t node, IntPtr type_support_ptr, string topic_name, IntPtr publisher_options) => { EnsureMainThread(); return ((PublisherInitType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_publisher_init"),
        typeof(PublisherInitType)))(ref publisher, ref node, type_support_ptr, topic_name, publisher_options); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int PublisherFiniType(ref rcl_publisher_t publisher, ref rcl_node_t node);
    internal static PublisherFiniType rcl_publisher_fini =
        (ref rcl_publisher_t publisher, ref rcl_node_t node) => { EnsureMainThread(); return ((PublisherFiniType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_publisher_fini"),
        typeof(PublisherFiniType)))(ref publisher, ref node); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int PublishType(ref rcl_publisher_t publisher, IntPtr message, IntPtr allocator);
    internal static PublishType rcl_publish =
        (ref rcl_publisher_t publisher, IntPtr message, IntPtr allocator) => { EnsureMainThread(); return ((PublishType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_publish"),
        typeof(PublishType)))(ref publisher, message, allocator); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate rcl_subscription_t GetZeroInitializedSubcriptionType();
    internal static GetZeroInitializedSubcriptionType rcl_get_zero_initialized_subscription =
        () => { EnsureMainThread(); return ((GetZeroInitializedSubcriptionType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_get_zero_initialized_subscription"),
        typeof(GetZeroInitializedSubcriptionType)))(); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int SubscriptionInitType(ref rcl_subscription_t subscription, ref rcl_node_t node, IntPtr type_support_ptr, string topic_name, IntPtr subscription_options);
    internal static SubscriptionInitType rcl_subscription_init =
        (ref rcl_subscription_t subscription, ref rcl_node_t node, IntPtr type_support_ptr, string topic_name, IntPtr subscription_options) => { EnsureMainThread(); return ((SubscriptionInitType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_subscription_init"),
        typeof(SubscriptionInitType)))(ref subscription, ref node, type_support_ptr, topic_name, subscription_options); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int SubscriptionFiniType(ref rcl_subscription_t subscription, ref rcl_node_t node);
    internal static SubscriptionFiniType rcl_subscription_fini =
        (ref rcl_subscription_t subscription, ref rcl_node_t node) => { EnsureMainThread(); return ((SubscriptionFiniType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_subscription_fini"),
        typeof(SubscriptionFiniType)))(ref subscription, ref node); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate bool SubscriptionIsValidType(ref rcl_subscription_t subscription);
    internal static SubscriptionIsValidType rcl_subscription_is_valid =
        (ref rcl_subscription_t subscription) => { EnsureMainThread(); return ((SubscriptionIsValidType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_subscription_is_valid"),
        typeof(SubscriptionIsValidType)))(ref subscription); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int TakeType(ref rcl_subscription_t subscription, IntPtr message_handle, IntPtr message_info, IntPtr allocation);
    internal static TakeType rcl_take =
        (ref rcl_subscription_t subscription, IntPtr message_handle, IntPtr message_info, IntPtr allocation) => { EnsureMainThread(); return ((TakeType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_take"),
        typeof(TakeType)))(ref subscription, message_handle, message_info, allocation); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate rcl_wait_set_t GetZeroInitializedWaitSetType();
    internal static GetZeroInitializedWaitSetType rcl_get_zero_initialized_wait_set =
        () => { EnsureMainThread(); return ((GetZeroInitializedWaitSetType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_get_zero_initialized_wait_set"),
        typeof(GetZeroInitializedWaitSetType)))(); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int WaitSetResizeType(ref rcl_wait_set_t wait_set,
                                            UIntPtr number_of_subscriptions,
                                            UIntPtr number_of_guard_conditions,
                                            UIntPtr number_of_timers,
                                            UIntPtr number_of_clients,
                                            UIntPtr number_of_services,
                                            UIntPtr number_of_events);
    internal static WaitSetResizeType rcl_wait_set_resize =
        (ref rcl_wait_set_t wait_set,
         UIntPtr number_of_subscriptions,
         UIntPtr number_of_guard_conditions,
         UIntPtr number_of_timers,
         UIntPtr number_of_clients,
         UIntPtr number_of_services,
         UIntPtr number_of_events) => { EnsureMainThread(); return ((WaitSetResizeType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_wait_set_resize"),
        typeof(WaitSetResizeType)))(ref wait_set, number_of_subscriptions, number_of_guard_conditions, number_of_timers, number_of_clients, number_of_services, number_of_events); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int WaitSetInitType(ref rcl_wait_set_t wait_set,
                                          UIntPtr number_of_subscriptions,
                                          UIntPtr number_of_guard_conditions,
                                          UIntPtr number_of_timers,
                                          UIntPtr number_of_clients,
                                          UIntPtr number_of_services,
                                          UIntPtr number_of_events,
                                          ref rcl_context_t context,
                                          rcl_allocator_t allocator);
    internal static WaitSetInitType rcl_wait_set_init =
        (ref rcl_wait_set_t wait_set,
         UIntPtr number_of_subscriptions,
         UIntPtr number_of_guard_conditions,
         UIntPtr number_of_timers,
         UIntPtr number_of_clients,
         UIntPtr number_of_services,
         UIntPtr number_of_events,
         ref rcl_context_t context,
         rcl_allocator_t allocator) => { EnsureMainThread(); return ((WaitSetInitType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_wait_set_init"),
        typeof(WaitSetInitType)))(ref wait_set, number_of_subscriptions, number_of_guard_conditions, number_of_timers, number_of_clients, number_of_services, number_of_events, ref context, allocator); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int WatiSetFiniType(ref rcl_wait_set_t wait_set);
    internal static WatiSetFiniType rcl_wait_set_fini =
        (ref rcl_wait_set_t wait_set) => { EnsureMainThread(); return ((WatiSetFiniType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_wait_set_fini"),
        typeof(WatiSetFiniType)))(ref wait_set); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int WaitSetClearType(ref rcl_wait_set_t wait_set);
    internal static WaitSetClearType rcl_wait_set_clear =
        (ref rcl_wait_set_t wait_set) => { EnsureMainThread(); return ((WaitSetClearType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_wait_set_clear"),
        typeof(WaitSetClearType)))(ref wait_set); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int WaitSetAddSubscriptionType(ref rcl_wait_set_t wait_set, ref rcl_subscription_t subscription, ref UIntPtr index);
    internal static WaitSetAddSubscriptionType rcl_wait_set_add_subscription =
        (ref rcl_wait_set_t wait_set, ref rcl_subscription_t subscription, ref UIntPtr index) => { EnsureMainThread(); return ((WaitSetAddSubscriptionType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_wait_set_add_subscription"),
        typeof(WaitSetAddSubscriptionType)))(ref wait_set, ref subscription, ref index); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int WaitSetAddClientType(ref rcl_wait_set_t wait_set, ref rcl_client_t client, ref UIntPtr index);
    internal static WaitSetAddClientType rcl_wait_set_add_client =
        (ref rcl_wait_set_t wait_set, ref rcl_client_t client, ref UIntPtr index) => { EnsureMainThread(); return ((WaitSetAddClientType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_wait_set_add_client"),
        typeof(WaitSetAddClientType)))(ref wait_set, ref client, ref index); };
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int WaitSetAddServiceType(ref rcl_wait_set_t wait_set, ref rcl_service_t service, ref UIntPtr index);
    internal static WaitSetAddServiceType rcl_wait_set_add_service =
        (ref rcl_wait_set_t wait_set, ref rcl_service_t service, ref UIntPtr index) => { EnsureMainThread(); return ((WaitSetAddServiceType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_wait_set_add_service"),
        typeof(WaitSetAddServiceType)))(ref wait_set, ref service, ref index); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int WaitType(ref rcl_wait_set_t wait_set, long timeout);
    internal static WaitType rcl_wait =
        (ref rcl_wait_set_t wait_set, long timeout) => { EnsureMainThread(); return ((WaitType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_wait"),
        typeof(WaitType)))(ref wait_set, timeout); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int RclClockGetNow(IntPtr ros_clock, ref long query_now);
    internal static RclClockGetNow rcl_clock_get_now =
        (IntPtr ros_clock, ref long query_now) => { EnsureMainThread(); return ((RclClockGetNow)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCL,
        "rcl_clock_get_now"),
        typeof(RclClockGetNow)))(ros_clock, ref query_now); };

    // rcutils

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate rcl_allocator_t RclGetDefaultAllocatorType();
    internal static RclGetDefaultAllocatorType rcutils_get_default_allocator =
        () => { EnsureMainThread(); return ((RclGetDefaultAllocatorType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCUtils,
        "rcutils_get_default_allocator"),
        typeof(RclGetDefaultAllocatorType)))(); };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void ResetErrorType();
    internal static ResetErrorType rcl_reset_error =
        () => { EnsureMainThread(); ((ResetErrorType)Marshal.GetDelegateForFunctionPointer(dllLoadUtils.GetProcAddress(
        nativeRCUtils,
        "rcutils_reset_error"),
        typeof(ResetErrorType)))(); };
  }
}
