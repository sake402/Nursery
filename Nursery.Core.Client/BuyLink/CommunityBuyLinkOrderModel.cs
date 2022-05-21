// using LivingThing.Core.Frameworks.Common.Entry;
// using LivingThing.Core.Frameworks.Common.Models;
// using System;
// using System.Collections.Generic;
// using System.Text;
// using LivingThing.Core.Community.Common.Models;
// using LivingThing.Core.Frameworks.Common.Data;
// using LivingThing.Core.Frameworks.Common.Attributes;
// using System.Linq;
// using LivingThing.Core.Shipping.Common.Models;
// using LivingThing.Core.Frameworks.Common.Data.Geographic;
// using System.Reflection;

// namespace LivingThing.Core.Community.BuyLink.Common.Models
// {
//     [Schema(ExcludeFilter = typeof(DataTableFilter))]
//     public class _CommunityBuyLinkOrderModel : CommunityPostModel, IShipmentPackageModel
//     {
//         public override CommunityPostTypes Type => CommunityPostTypes.BuyLinkOrder;
//         [Schema(Exclude = SchemaType.All)]
//         public DataId ShipmentId
//         {
//             get => GetPropertyOrDefault<DataId>(CommunityPostAttributeType.BuyLinkOrderShipmentId);
//             set => SetProperty(CommunityPostAttributeType.BuyLinkOrderShipmentId, value);
//         }
//         [Schema(Order = 3000, Exclude = SchemaType.All & ~SchemaType.DataTable)]
//         public DateTime? DatePaid
//         {
//             get => GetPropertyOrDefault<DateTime?>(CommunityPostAttributeType.BuyLinkOrderDatePaid);
//             set => SetProperty(CommunityPostAttributeType.BuyLinkOrderDatePaid, value);
//         }
//         [Schema(Order = 3001, Exclude = SchemaType.All & ~SchemaType.DataTable)]
//         public string PaymentReference
//         {
//             get => GetPropertyOrDefault<string>(CommunityPostAttributeType.BuyLinkOrderPaymentReference);
//             set => SetProperty(CommunityPostAttributeType.BuyLinkOrderPaymentReference, value);
//         }
//         [Schema(Order = 3002, Exclude = SchemaType.All & ~SchemaType.DataTable)]
//         public ShipmentDeliveryType Delivery
//         {
//             get => GetPropertyOrDefault<ShipmentDeliveryType>(CommunityPostAttributeType.BuyLinkOrderShipmentDeliveryType);
//             set => SetProperty(CommunityPostAttributeType.BuyLinkOrderShipmentDeliveryType, value);
//         }
//         [Schema(Order = 3003, Exclude = SchemaType.All & ~SchemaType.DataTable)]
//         public string DeliveryAddress
//         {
//             get => GetPropertyOrDefault<string>(CommunityPostAttributeType.BuyLinkOrderShipmentDeliveryAddress);
//             set => SetProperty(CommunityPostAttributeType.BuyLinkOrderShipmentDeliveryAddress, value);
//         }
//         [Schema(Order = 3004, Exclude = SchemaType.All & ~SchemaType.DataTable)]
//         public GeoLocation DeliveryLocation
//         {
//             get => GetPropertyOrDefault<GeoLocation>(CommunityPostAttributeType.BuyLinkOrderShipmentDeliveryLocation);
//             set => SetProperty(CommunityPostAttributeType.BuyLinkOrderShipmentDeliveryLocation, value);
//         }
//         [Schema(Order = 3005, Exclude = SchemaType.All & ~SchemaType.DataTable)]
//         public DateTime? DateDelivered
//         {
//             get => GetPropertyOrDefault<DateTime?>(CommunityPostAttributeType.BuyLinkOrderDateDelivered);
//             set => SetProperty(CommunityPostAttributeType.BuyLinkOrderDateDelivered, value);
//         }
//         [Schema(Order = 3006, Exclude = SchemaType.All & ~SchemaType.DataTable)]
//         public DateTime? DateConfirmed
//         {
//             get => GetPropertyOrDefault<DateTime?>(CommunityPostAttributeType.BuyLinkOrderDateConfirmed);
//             set => SetProperty(CommunityPostAttributeType.BuyLinkOrderDateConfirmed, value);
//         }
//         [Schema(Exclude = SchemaType.All)]
//         public IShipmentAccount Sender => null;
//         [Schema(Exclude = SchemaType.All)]
//         public IShipmentAccount Receiver => Account;
//         [Schema(Exclude = SchemaType.All)]
//         public IEnumerable<IShipmentProductModel> Products => Posts?.OfType<CommunityBuyLinkPostModel>();

//         public override bool RequiresPost() => false;
//         public override bool RequiresSummary() => false;
//         public override bool RequiresTags() => false;
//         public override IEnumerable<TDataDescriptor> UpdateDescriptors<TDataDescriptor>(IEnumerable<TDataDescriptor> descriptors, SchemaType schema)
//         {
//             descriptors = base.UpdateDescriptors(descriptors, schema);
//             if (schema == SchemaType.Form || schema == SchemaType.DataTable)
//             {
//                 var exclude = new[] { nameof(Tags), nameof(Icon), nameof(Summary) };
//                 descriptors = descriptors.Where(d => !exclude.Contains(d.Name));
//                 var name = descriptors.Single(d => d.Name == nameof(Name));
//                 name.Label = "Order Label";
//                 name.Description = "Your order is tagged with this label";
//                 var desc = descriptors.Single(d => d.Name == nameof(Post));
//                 desc.Label = "Order Description";
//                 desc.Description = "More information on this order";
//             }
//             return descriptors;
//         }
//     }

//     class DataTableFilter : ISchemaVisibilityProvider
//     {
//         public bool Visible(object target, PropertyInfo property, SchemaType schemaType)
//         {
//             return true;
//         }
//     }
// }
